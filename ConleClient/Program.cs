using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.IO;

namespace ConleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            dooPost();
        }
        static async void dooPost()
        {
            var url = "";
                //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                Encoding encoding = Encoding.UTF8;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://192.168.0.210:3636/api/products/Thumbnail/1");
                request.Method = "GET";
                request.Accept = "text/html, application/xhtml+xml, */*";
                request.ContentType = "application/json";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    var result =  reader.ReadToEnd();
                }
            

            //using (WebClient client = new WebClient())
            //{
            //    client.Headers["Type"] = "GET";
            //    client.Headers["Accept"] = "application/json";
            //    client.Encoding = Encoding.UTF8;
            //    client.DownloadStringCompleted += (senderobj, es) =>
            //    {
            //        var obj = es.Result;
            //    };
            //    client.DownloadStringAsync(new Uri("http://192.168.0.210:3636/api/products/Thumbnail/1"));
            //}

            //try
            //{
            //    string url = "http://192.168.0.210:3636/api/products/Thumbnail/1";
            //    var userId = "1";
            //    //设置HttpClientHandler的AutomaticDecompression
            //    var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };
            //    //创建HttpClient（注意传入HttpClientHandler）
            //    using (var http = new HttpClient(handler))
            //    {
            //        //使用FormUrlEncodedContent做HttpContent
            //        var content = new FormUrlEncodedContent(new Dictionary<string, string>()
            //    {
            //      {"", userId}//键名必须为空
            //     });

            //        //await异步等待回应

            //        var response = await http.GetAsync(url);
            //        //确保HTTP成功状态值
            //        response.EnsureSuccessStatusCode();
            //        //await异步读取最后的JSON（注意此时gzip已经被自动解压缩了，因为上面的AutomaticDecompression = DecompressionMethods.GZip）
            //        Console.WriteLine(await response.Content.ReadAsStringAsync());
            //    }
            //}
            //catch(Exception ex)
            //{

            //}

        }
    }
}
