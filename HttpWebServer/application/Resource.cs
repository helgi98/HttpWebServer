using HttpWebServer.http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer.application
{
    class Resource : IResource
    {
        private string _path;

        public Resource(string path)
        {
            _path = path;
        }

        public string GetContentType()
        {
            String fileExtension =
                new String(_path.Reverse().TakeWhile(el => el != '.').Reverse().ToArray());
            
            switch (fileExtension)
            {
                case "html": return "text/html";
                case "css": return "text/css";
                case "js": return "application/javascript";
                case "png": return "image/png";
                case "jpg": return "image/jpg";
                case "jpeg": return "image/jpeg";
                case "gif": return "image/gif";
                default: return "application/octet-stream";
            }
        }

        public void Process(HttpRequest req, HttpResponse res)
        {
            res.Headers.Add("Content-Type", GetContentType());
            Stream content;
            try
            {
                content = new StreamReader(_path.Trim('/')).BaseStream;
            }
            catch
            {
                content = new MemoryStream();
            }
            res.Headers.Add("Content-Length", content.Length.ToString());
            res.Content = content;
        }
    }
}
