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
    class FileController :Controller
    {
        public override void DoPut(HttpRequest req, HttpResponse res)
        {
            var oldFile = req.Content;
            var newFileName = req.GetHeader("NewFileName");

            FileManager.ChangeFileName(oldFile, newFileName, "1234");
            res.StatusCode = "200";
            res.ReasonPhrase = "Ok";
        }
    }
}
