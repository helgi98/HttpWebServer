using HttpWebServer.http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer.application
{
    interface IResource
    {
        void Process(HttpRequest req, HttpResponse res);
    }
}
