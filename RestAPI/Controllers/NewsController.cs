
using System.Linq;
using System.Web.Http;
using RestAPI.Models;
using System.Collections.Generic;
using System;
using Newtonsoft.Json.Linq;
using System.Web;
using System.Net.Http;
using System.Net;

namespace RestAPI.Controllers
{

    public class addNewsRequest
    {
        public string coverImagePath { get; set; }
        public string title { get; set; }
        public string body { get; set; }
    }

    public class Result
    {
        public List<am_news> rows { get; set; }
        public int total { get; set; }
    }

    public class deleteRequestAll
    {
        public List<deleteRequest> ids;
    }
    public class deleteRequest
    {
        public string id;
    }


    [RoutePrefix("api/news")]
    public class NewsController : ApiController
    {
        /// <summary>
        /// 获取所有新闻信息
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Items")]
        public Result Items(int index = 0, int pageSize = 5)
        {
            using (AmAPIContent content = new AmAPIContent())
            {

                var result = content.Am_News.OrderByDescending(o => o.PublishDate).Take(pageSize * (index + 1)).Skip(pageSize * (index)).ToList();

                Result r = new Result();
                r.rows = result;
                r.total = content.Am_News.Count();

                return r;

            }

        }
        /// <summary>
        /// 获取单个新闻信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Item")]
        public am_news Item(string ID)
        {
            using (AmAPIContent content = new AmAPIContent())
            {

                var result = content.Am_News.FirstOrDefault(o => o.ID.ToString() == ID);

                return result;

            }

        }
        /// <summary>
        /// 新增修改
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Item/Modify")]
        public bool ItemModify([FromBody] JObject request)
        {
            var ID = Convert.ToInt64(request["ID"]);
            using (AmAPIContent content = new AmAPIContent())
            {

                var result = content.Am_News.FirstOrDefault(o => o.ID == ID);
                result.Title = request["title"].ToString();
                result.Content = request["body"].ToString();
                result.imagePath = request["coverImagePath"].ToString();
                return content.SaveChanges() > 0;
            }
            return false;
        }

        /// <summary>
        /// 新闻新增
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Item")]
        public bool Item([FromBody] JObject request)
        {
            var dbresult = false;

            using (AmAPIContent content = new AmAPIContent())
            {

                am_news model = new am_news();
                Random random = new Random();
                model.imagePath = request["coverImagePath"].ToString();
                model.Title = request["title"].ToString();
                model.Content = request["body"].ToString();
                model.Type = "2";
                model.PublishDate = System.DateTime.Now;
                model.VisitCount = random.Next(1000, 5000);
                model.CreatedBy = "亚马逊探险乐园";
                model.Created = System.DateTime.Now;
                model.ModifiedBy = "亚马逊探险乐园";
                model.Modified = System.DateTime.Now;
                var result = content.Am_News.Add(model);
                return content.SaveChanges() > 0;
            }

            return dbresult;
        }
        /// <summary>
        /// 图片上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Item/upload")]
        public string postfile()
        {
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            var filePath = "";
            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    filePath = HttpContext.Current.Server.MapPath("~/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);

                    docfiles.Add(filePath);
                }

            }

            return filePath;

        }
        /// <summary>
        /// 单个新闻删除
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Item/delete")]
        public bool delete([FromBody] JObject request)
        {
            var ID = Convert.ToInt32(request["ID"]);
            using (AmAPIContent content = new AmAPIContent())
            {

                var result = content.Am_News.FirstOrDefault(o => o.ID == ID);
                content.Am_News.Remove(result);
                return content.SaveChanges() > 0;
            }

            return false;
        }
        /// <summary>
        /// 批量删除新闻
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Items/delete")]
        public bool deleteAll([FromBody] JObject request)
        {
            using (AmAPIContent content = new AmAPIContent())
            {
                foreach (var id in request["IDList"])
                {
                    var result = content.Am_News.FirstOrDefault(o => o.ID == Convert.ToInt32(id));
                    content.Am_News.Remove(result);
                }

                return content.SaveChanges() > 0;
            }
            return false;
        }


    }
}
