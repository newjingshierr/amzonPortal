using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace core.Rest
{
    class Rest
    {
        public static void HttpPost()
        {
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://app.yungalaxy.com/YeeOfficeDocument_Net/_API/Ver(3.0)//api/admin/library");
            request.Method = "POST";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/json";
            request.Headers.Add("AkmiiSecret", "ContextFactory.GetAkmiiSecret()");
            string postData = "APPID=" + "48";
            postData += ("&CategoryID=" + "890476991822827520");
            postData += ("&Description=" + "");
            postData += ("&Ext=" + "");
            postData += ("&IconUrl=" + "");
            postData += ("&Localization=" + "");
            postData += ("&IsItemPerm=" + false);
            postData += ("&ShowFileIcon=" + true);
            byte[] buffer = encoding.GetBytes(postData);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string result;
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            string ii = result;
        }
    }
}
