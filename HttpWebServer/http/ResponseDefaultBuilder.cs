using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer.http
{
    public static class ResponseDefaultBuilder
    {
            public static HttpResponse Ok()
            {
                return new HttpResponse()
                {
                    ReasonPhrase = "OK",
                    StatusCode = "200",
                };
            }

            public static HttpResponse InternalServerError()
            {
                string content = File.ReadAllText("resources/pages/errors/500.html");

                return new HttpResponse()
                {
                    ReasonPhrase = "InternalServerError",
                    StatusCode = "500",
                    Content = content.ToStream()
                };
            }

            public static HttpResponse NotFound()
            {
                string content = File.ReadAllText("resources/pages/errors/404.html");

                return new HttpResponse()
                {
                    ReasonPhrase = "NotFound",
                    StatusCode = "404",
                    Content = content.ToStream()
                };
            }
    }
}
