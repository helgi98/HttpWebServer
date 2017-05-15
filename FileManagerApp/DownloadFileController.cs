using HttpWebServer.controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpWebServer.http;
using System.IO;

namespace FileManagerApp
{
    class DownloadFileController: Controller
    {
        public override void DoGet(HttpRequest req, HttpResponse res)
        {
            if (req.GetQueryParameter("path") != null)
            {
                try
                {
                    String path = req.GetQueryParameter("path");
                    Stream stream = FileManager.DownloadFile(path);

                    res.Headers.Add("Content-Type", "application/octet-stream");
                    res.Headers.Add("Content-Length", stream.Length.ToString());
                    res.Content = stream;
                }
                catch
                {
                    res.StatusCode = "400";
                    res.ReasonPhrase = "Bad Request";
                }
            }
            else
            {
                res.StatusCode = "404";
                res.ReasonPhrase = "File Not Found";
            }
        }
    }
}
