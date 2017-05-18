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
        private static FileManagerApp _app;

        private FileManagerApp() : base("fm")
        {
            FileManager.RootPath = GetVariable("RootFillesPath");
        }

        public static Application GetApp()
        {
            if (_app == null) _app = new FileManagerApp();

            return _app;
        }
    }
}
