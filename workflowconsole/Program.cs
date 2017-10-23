using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Threading;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.SqlServer;

namespace workflowconsole
{
    [DataContract]
    class TaskResult
    {
        [DataMember]
        public List<Data> Data;
        [DataMember]
        public string Status;
        [DataMember]
        public string Message;
        [DataMember]
        public string TotalCount;
    }

    [DataContract]
    public class Data
    {
        [DataMember]
        public long TaskID;
        [DataMember]
        public long ProcInstID;
        [DataMember]
        public long ProcDefID;
        [DataMember]
        public string ProcDefName;
        [DataMember]
        public string FlowNo;
        [DataMember]
        public string CategoryID;
        [DataMember]
        public string CategoryName;
        [DataMember]
        public long ExecutionID;
        [DataMember]
        public long ActivityID;
        [DataMember]
        public string Name;
        [DataMember]
        public string ParentTaskID;
        [DataMember]
        public string Description;
        [DataMember]
        public long OwnerID;
        [DataMember]
        public long AssigneeID;
        [DataMember]
        public string AssigneeName;
        [DataMember]
        public string AssigneePhoto;
        [DataMember]
        public string DelegateID;
        [DataMember]
        public string DelegateName;
        [DataMember]
        public string DelegatePhoto;
        [DataMember]
        public string Priority;
        [DataMember]
        public string StartTimeStr;
        [DataMember]
        public string ClaimTimeStr;
        [DataMember]
        public string EndTimeStr;
        [DataMember]
        public string Duration;
        [DataMember]
        public string DueDateStr;
        [DataMember]
        public string DeleteReason;
        [DataMember]
        public string Outcome;
        [DataMember]
        public string IsAllowSign;
        [DataMember]
        public string Comment;
        [DataMember]
        public string TaskURL;
        [DataMember]
        public string IsSaveFormData;
        [DataMember]
        public string FormDataID;
        [DataMember]
        public string CallProcInstID;
        [DataMember]
        public string Secret;

    }
    [DataContract]
    public class SecretReuslt
    {
        [DataMember]
        public Data Data;

    }

    public class putRequest
    {
        public string TaskID;
        public string Outcome;
    }
    class Program
    {
        public string key = "";
        public static string readTasksUrl = "https://flowcraft.yungalaxy.com/_API/Ver(3.0)//api/tasks?type=1&pageIndex=1&pageSize=20&1502696784227";
        public static string loginUrl = "https://loginserver.yungalaxy.com/_api/ver(2.0)/login";
        public static string approveUrl = "https://flowcraft.yungalaxy.com/_API/Ver(3.0)//api/tasks/handle";

        static void Main(string[] args)
        {
            var autoEvent = new AutoResetEvent(false);
            Timer t = new Timer(p => excute_handler(), autoEvent, 0, 1000 * 60 * 30);// 第一个参数是：回调方法，表示要定时执行的方法，第二个参数是：回调方法要使用的信息的对象，或者为空引用，第三个参数是：调用 callback 之前延迟的时间量（以毫秒为单位），指定 Timeout.Infinite 以防止计时器开始计时。指定零 (0) 以立即启动计时器。第四个参数是：定时的时间时隔，以毫秒为单位
            autoEvent.WaitOne();
        }




        static void excute_handler()
        {
            Program p = new Program();
            var screct = p.Login();
            /*读取任务*/
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("AkmiiSecret", screct);
            var response = httpClient.GetAsync(new Uri(readTasksUrl)).Result;
            var result = response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<TaskResult>(result.Result);
            Console.WriteLine("共计" + model.TotalCount + "条任务待审批");

            int taskCount = int.Parse(model.TotalCount);

            for (int i = 0; i < taskCount; i++)
            {
                 p.Approve(model.Data[i].TaskID.ToString(), screct);
                 p.WriteLog(model.Data[i].OwnerID.ToString(), "", model.Data[i].ProcDefName, model.Data[i].FlowNo, System.DateTime.Now);
            }

            /*十点开始到十二点之间开始检查，并且周六，周日不检查*/
            if (System.DateTime.Now.Hour > 22 && System.DateTime.Now.Hour < 24 && System.DateTime.Now.DayOfWeek.ToString() !="0"&& System.DateTime.Now.DayOfWeek.ToString() !="6")
            {
                Console.WriteLine("开始检查任务:"+System.DateTime.Now);
                p.CheckLog();
                Console.WriteLine("检查任务结束:"+System.DateTime.Now);
            }

            System.Threading.Thread.Sleep(3000);
            /*审批任务*/
        }

        /// <summary>
        /// 审批
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="key"></param>
        void Approve(string taskID, string key)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("AkmiiSecret", key);
            putRequest putReq = new putRequest();
            putReq.TaskID = taskID;
            putReq.Outcome = "Approved";
            HttpContent content = new StringContent(JsonConvert.SerializeObject(putReq), Encoding.UTF8, "application/json");
            content.Headers.Add("TaskID", taskID);

            var response = httpClient.PutAsync(new Uri(approveUrl), content).Result;
            var result = response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
            Console.WriteLine("审批通过:" + taskID);
        }


        /*写入日志*/
        void WriteLog(string assinger, string assignerName, string category, string flowNo, DateTime submitTime)
        {

            using (WorkflowContent content = new WorkflowContent())
            {
                WorkflowEntity entity = new WorkflowEntity();
                entity.assigner = assinger;
                entity.submitDate = submitTime;
                entity.assignerName = assignerName;
                entity.modified = System.DateTime.Now;
                entity.Key = Guid.NewGuid();
                entity.category = category;
                content.WorkflowEntitySet.Add(entity);
                content.SaveChanges();

            }
        }

        /*检查日报*/
        void CheckLog()
        {
            Dictionary<string, string> usersDictionary = new Dictionary<string, string>();
            usersDictionary.Add("797006985907277824", "Evan");
            usersDictionary.Add("816908639905386496", "Holly");
            usersDictionary.Add("785374225648193536", "phil");
            usersDictionary.Add("768323699781799940", "hand");



            using (WorkflowContent content = new WorkflowContent())
            {

                var todayTasks = content.WorkflowEntitySet.Where(item => item.submitDate.Day == DateTime.Now.Day&& item.category == "工作日报").ToList();
                /*首先检查当天任务是不是4条，如果不是则要比较数据，检查谁没有提交*/
                if (content.WorkflowEntitySet.Select(item => item.submitDate.Day == DateTime.Now.Day&& item.category == "工作日报").Count() != 4)
                {

                    foreach (var item in usersDictionary)
                    {
                        bool isExit = false;
                        foreach (var currentItem in todayTasks)
                        {
                            if (item.Key == currentItem.assigner)
                            {
                                isExit = true;
                            }
                        }
                        if (!isExit)
                        {
                            SendMail(item.Key);
                        }


                    }
                }
            }

        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="assingTo"></param>
        void SendMail(string assingTo)
        {
            Dictionary<string, string> mailList = new Dictionary<string, string>();
            mailList.Add("797006985907277824", "Evan.Xu@akmii.com");
            mailList.Add("785374225648193536", "phil.xu@akmii.com");
            mailList.Add("768323699781799940", "handy.zhang@akmii.com");
            mailList.Add("816908639905386496", "holly.zhang@akmii.com");

            var res = mailList.Where(item => item.Key == assingTo).Select(item => item.Value).SingleOrDefault();

            foreach (var item in mailList)
            {
                if (item.Key == assingTo)
                {
                    System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                    client.Host = "smtp.163.com";//使用163的SMTP服务器发送邮件
                    client.UseDefaultCredentials = true;
                    client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    client.Credentials = new System.Net.NetworkCredential("jingshierr@163.com", "sj789456");//163的SMTP服务器需要用163邮箱的用户名和密码作认证，如果没有需要去163申请个,
                                                                                                            //这里假定你已经拥有了一个163邮箱的账户，用户名为abc，密码为*******
                    System.Net.Mail.MailMessage Message = new System.Net.Mail.MailMessage();
                    Message.From = new System.Net.Mail.MailAddress("jingshierr@163.com");//这里需要注意，163似乎有规定发信人的邮箱地址必须是163的，而且发信人的邮箱用户名必须和上面SMTP服务器认证时的用户名相同
                                                                                         //因为上面用的用户名abc作SMTP服务器认证，所以这里发信人的邮箱地址也应该写为abc@163.com
                    Message.To.Add(item.Value);//将邮件发送给Gmail
                    Message.Subject = "狗日地，你今天的日报没有提交";
                    Message.Body = "我20点到24点之间开始检查，每隔半小时提醒一封；直到你奔溃！！！！！！";
                    Message.SubjectEncoding = System.Text.Encoding.UTF8;
                    Message.BodyEncoding = System.Text.Encoding.UTF8;
                    Message.Priority = System.Net.Mail.MailPriority.High;
                    Message.IsBodyHtml = true;
                    client.Send(Message);

                    Console.WriteLine(item.Value+"没有提交日报，提醒邮件发送成功。当前时间是"+System.DateTime.Now);
                }
            }


        }

        /// <summary>
        /// 返回akmiiSercret
        /// </summary>
        /// <returns></returns>
        string Login()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var stringContent = new StringContent("{\"LoginName\":\"i:0#.f|membership|enzo.shi@akmii.com\",\"MerchatToken\":\"bd1d7c7e-e799-474e-812d-28672789993a\"}", Encoding.UTF8, "application/json");
            var content = new FormUrlEncodedContent(new Dictionary<string, string>() { { "LoginName", "i:0#.f|membership|enzo.shi@akmii.com" }, { "MerchatToken", "bd1d7c7e-e799-474e-812d-28672789993a" } });
            var response = httpClient.PostAsync(new Uri(loginUrl), stringContent).Result;
            if (response.Content != null)
            {
                var responseContent = response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<SecretReuslt>(responseContent.Result);
                Console.Write("当前时间" + System.DateTime.Now + "获取的secret" + model.Data.Secret);
                key = model.Data.Secret;
            }

            return key;
        }





    }

}
