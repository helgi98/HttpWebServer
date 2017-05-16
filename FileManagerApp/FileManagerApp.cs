using HttpWebServer.application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagerApp
{
    public class FileManagerApp : Application
    {
        private static Application _app;

        private FileManagerApp() : base("fm")
        {
        }

        public static Application GetApp()
        {
            if (_app == null) _app = new FileManagerApp();

            return _app;
        }
    }
}
