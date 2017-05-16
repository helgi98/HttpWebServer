using HttpWebServer.controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpWebServer.http;
using System.IO;
using HttpWebServer;

namespace FileManagerApp
{
    class DownloadFileController: Controller
    {
        public override void DoGet(HttpRequest req, HttpResponse res)
        {
            String path = req.GetQueryParameter("path");
            String fileName = Utilities.GetFileName(path);
            Stream stream = FileManager.DownloadFile(path);

            res.Headers.Add("Content-Type", MIMETypes.GetContentType(path));
            res.Headers.Add("Content-Disposition", $"inline; filename = \"{fileName}\"");
            res.Headers.Add("Content-Length", stream.Length.ToString());
            res.Content = stream;
        }
    }
}
