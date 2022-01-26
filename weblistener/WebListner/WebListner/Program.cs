using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebListner1
{
    class Program
    {

        private static string htmlFormat =
            "<>";
        static  void Main(string[] args)
        {
            StartServerAsync().Wait();
        }

        static void ShowUsage()
        {
            Console.WriteLine("usage httpServer Prefix [Prefix2] [Prefix3] [Prefix4]");
        }

        public static async Task StartServerAsync(params string[] prefixes)
        {
            try
            {
                var settings = new WebListenerSettings();
                settings.UrlPrefixes.Add("http://localhost:8080");
                WebListener listener = new WebListener(settings);
                listener.Start();
                do
                {
                    using (RequestContext context = await listener.AcceptAsync())
                    {
                        var result1 = context.User;
                       
                       // result1.Identity
                        var requestBody = context.Request.Body;
                        var length = requestBody.Length;
                        context.Response.Headers.Add("content-type",new string[] { "text/json"});
                        context.Response.StatusCode = (int)HttpStatusCode.OK;

                        JsonResult jsonResult = new JsonResult(new  { respone = "Hello World: " + DateTime.Now });

                        byte[] bytes = Encoding.ASCII.GetBytes(jsonResult.Value.ToString());
                        context.Response.ContentLength = bytes.Length;
                        context.Response.ContentType = "text/json";

                        await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                        context.Dispose();

                    }

                } while (true);
            }
            catch (Exception ex)
            { 
            
            }
        }



    }
}
