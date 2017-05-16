using HttpWebServer.controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpWebServer.http;

namespace FileManagerApp
{
    class LoginController: Controller
    {
        public override void DoGet(HttpRequest req, HttpResponse res)
        {

            if (req.GetCookie("admin") != null) Container.Forward(req, res, "pages/dirview/index.html");
            else Container.Forward(req, res, "pages/login/index.html");
        }

        public override void DoPost(HttpRequest req, HttpResponse res)
        {
            String login = req.GetParameter("login");
            String password = req.GetParameter("password");

            if (login == "admin" && password == "password")
            {
                res.Cookie = new Cookie { Name = "admin", Value = "true"};
                Container.Forward(req, res, "pages/dirview/index.html");
            }
            else
            {
                Container.Forward(req, res, "pages/login/index.html");
            }
        }


    }
}
