using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpWebServer.http;
using HttpWebServer.application;

namespace HttpWebServer.controllers
{
    public abstract class Controller
    {
        public ControllerContatiner Container { get; set; }

        //Add response error
        public virtual void DoGet(HttpRequest req, HttpResponse res)
        {
            throw new NotImplementedException();
        }

        //Add response error
        public virtual void DoPost(HttpRequest req, HttpResponse res)
        {
            throw new NotImplementedException();
        }

        //Add response error
        public virtual void DoDelete(HttpRequest req, HttpResponse res)
        {
            throw new NotImplementedException();
        }

        //Add response error
        public virtual void DoPut(HttpRequest req, HttpResponse res)
        {
            throw new NotImplementedException();
        }
    }
}
