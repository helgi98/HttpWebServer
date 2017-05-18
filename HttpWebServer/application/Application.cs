using HttpWebServer.http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer.application
{
    abstract public class Application
    {
        private ControllerContatiner _dispatcher;

        public bool IsActive { get; set; }
        public String Name { get; set; }

        public void Process(ref HttpRequest req, ref HttpResponse res)
        {
            _dispatcher.Process(ref req, ref res);
        }

        public String GetVariable(String key)
        {
            return _dispatcher.GetVariable(key);
        }

        public Application(string name)
        {
            IsActive = false;
            Name = name;

            _dispatcher = new ControllerContatiner(name + "-config.xml");
        }

    }
}
