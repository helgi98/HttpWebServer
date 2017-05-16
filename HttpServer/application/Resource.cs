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
        private string _root;

        public Resource(string root, string path)
        {
            _path = path;
            _root = root;
        }
        
        public void Process(HttpRequest req, HttpResponse res)
        {
            res.Headers.Add("Location", _path);
            res.Headers.Add("Content-Type", MIMETypes.GetContentType(_path));

            Stream content;
            try
            {
                content = new StreamReader((_root + "/" + _path)).BaseStream;
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
