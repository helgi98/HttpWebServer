using HttpWebServer.controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpWebServer.http;
using HttpWebServer;

namespace FileManagerApp
{
    class AdminController: Controller
    {
        public override void DoGet(HttpRequest req, HttpResponse res)
        {
            if (req.GetCookie("admin") == null)
                return;

            if (req.GetQueryParameter("action") == "status")
            {
                res.Headers.Add("Content-Type", "application/json");
                var answer = $"{{\"public_access\": \"{FileManager.PublicAccessed }\"}}";
                res.Content = answer.ToStream();
            }
            else if (req.GetQueryParameter("action") == null)
            {
                Container.Forward(req, res, "pages/adminview/index.html");
            }
        }

        public override void DoPost(HttpRequest req, HttpResponse res)
        {
            if (req.GetCookie("admin") == null)
                return;

            var status = req.GetParameter("public_access");
            FileManager.PublicAccessed = status == "true";
        }
    }
}
