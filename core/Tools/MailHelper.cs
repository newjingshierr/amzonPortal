using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Tools
{
    class MailHelper
    {
        void senderMail(string host,string user,string pass,string sender,string to,string subject,string body)
        {
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            client.Host = host;//使用163的SMTP服务器发送邮件
            client.UseDefaultCredentials = true;
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential(user, pass);//163的SMTP服务器需要用163邮箱的用户名和密码作认证，如果没有需要去163申请个,
                                                                                                    //这里假定你已经拥有了一个163邮箱的账户，用户名为abc，密码为*******
            System.Net.Mail.MailMessage Message = new System.Net.Mail.MailMessage();
            Message.From = new System.Net.Mail.MailAddress(sender);//这里需要注意，163似乎有规定发信人的邮箱地址必须是163的，而且发信人的邮箱用户名必须和上面SMTP服务器认证时的用户名相同
                                                                                 //因为上面用的用户名abc作SMTP服务器认证，所以这里发信人的邮箱地址也应该写为abc@163.com
            Message.To.Add(to);//将邮件发送给Gmail
            Message.Subject = subject;
            Message.Body = body;
            Message.SubjectEncoding = System.Text.Encoding.UTF8;
            Message.BodyEncoding = System.Text.Encoding.UTF8;
            Message.Priority = System.Net.Mail.MailPriority.High;
            Message.IsBodyHtml = true;
            client.Send(Message);
        }
    }
}
