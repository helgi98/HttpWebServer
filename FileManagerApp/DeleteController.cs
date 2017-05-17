using HttpWebServer.controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpWebServer.http;

namespace FileManagerApp
{
    class DeleteController: Controller
    {
        public override void DoDelete(HttpRequest req, HttpResponse res)
        {
            var path = req.GetQueryParameter("path");
            FileManager.DeleteFile(path);
        }
    }
}
