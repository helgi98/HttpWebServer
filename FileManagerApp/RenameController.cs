using HttpWebServer.controllers;
using HttpWebServer.http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagerApp
{
    class RenameController : Controller
    {
        public override void DoPost(HttpRequest req, HttpResponse res)
        {
            var oldFileName = req.GetParameter("oldFileName");
            var newFileName = req.GetParameter("newFileName");
            var path = req.GetParameter("renamePath");

            FileManager.ChangeFileName(oldFileName, newFileName, path);
            res.StatusCode = "200";
            res.ReasonPhrase = "Ok";
        }
    }
}
