using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer.http
{
    enum HttpStatusCode
    {
        Continue = 100,
        Ok = 200,
        Created = 201,
        Accepted = 202,
        BadRequest = 400,
        NotFound = 404,
        InternalServerError = 500
    }

    public class HttpResponse
    {
        public string StatusCode { get; set; }
        public string ReasonPhrase { get; set; }
        public Stream Content { get; set; }

        public Dictionary<string, string> Headers { get; set; }

        public HttpResponse()
        {
            Headers = new Dictionary<string, string>();
        }
    }
}
