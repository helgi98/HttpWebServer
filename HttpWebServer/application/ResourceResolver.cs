using HttpWebServer.http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer.application
{
    class ResourceResolver : IResourceResolver
    {
        public String Path { get; set; }

        public IResource Resolve(string resourceUrl)
        {
            Resource resource = new Resource(Path.Trim('/'), resourceUrl.Trim('/'));

            return resource;
        }
    }
}
