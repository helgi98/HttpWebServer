using System;
using System.Collections.Generic;
using System.Linq;
using HttpWebServer.http;
using HttpWebServer.controllers;
using Newtonsoft.Json;
using HttpWebServer;
using System.IO;

namespace FileManagerApp
{
    class TreeViewController: Controller
    {

        public override void DoGet(HttpRequest req, HttpResponse res)
        {
            if (req.GetQueryParameter("action") == "dircontent"
                    && (FileManager.PublicAccessed || req.GetCookie("admin") != null))
            {
                String path = req.GetQueryParameter("path");
                Dictionary<String, List<String>> data = FileManager.GetDirectoryContent(path);
                Stream jsonData = JsonConvert.SerializeObject(data).ToStream();

                res.Headers.Add("Content-Type", "application/json");
                res.Headers.Add("Content-Length", jsonData.Length.ToString());
                res.Content = jsonData;
            }
            else Container.Forward(req, res, "pages/dirview/index.html");
        }
    }
}
