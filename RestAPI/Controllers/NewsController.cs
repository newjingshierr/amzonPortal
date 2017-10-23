
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
        public string coverImagePath;
        public string title;
        public string body;
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
        [HttpPut]
        [Route("Item")]
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

        [HttpPost]
        [Route("Item")]
        public bool Item([FromBody] JObject request)
        {
            var dbresult = true;

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
                content.SaveChanges();
            }

            return dbresult;
        }

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

        [HttpDelete]
        [Route("Item/delete")]
        public bool delete([FromBody] JObject request)
        {
            var ID = Convert.ToInt64(request["ID"]);
            using (AmAPIContent content = new AmAPIContent())
            {

                var result = content.Am_News.FirstOrDefault(o => o.ID == ID);
                content.Am_News.Remove(result);
                return content.SaveChanges() > 0;
            }

            return false;
        }

        [HttpDelete]
        [Route("Items/delete")]
        public bool deleteAll([FromBody] deleteRequestAll request)
        {
            using (AmAPIContent content = new AmAPIContent())
            {
                foreach (var id in request.ids)
                {
                    var result = content.Am_News.FirstOrDefault(o => o.ID == Convert.ToInt64(id));
                    content.Am_News.Remove(result);
                }

                return content.SaveChanges() > 0;
            }
            return false;
        }


    }
}
