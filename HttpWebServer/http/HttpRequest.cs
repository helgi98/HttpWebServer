using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer.http
{
    public class HttpRequest
    {
        private Dictionary<String, String> _queryParameters;
        private Dictionary<string, string> _headers;
        private List<Cookie> _cookies;
        public string Method { get; set; }
        public Uri Url { get; set; }
        public string Content { get; set; }

        public void AddCookie(Cookie cookie)
        {
            _cookies.Add(cookie);
        }

        public Cookie GetCookie(String name)
        {
            var c = _cookies.Where(cookie => cookie.Name == name);

            if (c.Count() == 1) return c.First();
            else return null;
        }

        public void AddQueryParameter(String key, String value)
        {
            _queryParameters.Add(key, value);
        }

        public void AddHeader(String key, String value)
        {
            _headers.Add(key, value);
        }

        public String GetQueryParameter(String key)
        {
            String value;
            _queryParameters.TryGetValue(key, out value);
            return value;
        }

        public String GetHeader(String header)
        {
            String value;
            _headers.TryGetValue(header, out value);
            return value;
        }

        public HttpRequest()
        {
            _headers = new Dictionary<string, string>();
            _queryParameters = new Dictionary<string, string>();
            _cookies = new List<Cookie>();
        }

        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(Content))
                if (!_headers.ContainsKey("Content-Length"))
                    _headers.Add("Content-Length", Content.Length.ToString());

            return $"{Method} {Url} HTTP/1.0\r\n{string.Join("\r\n", _headers.Select(h => $"{h.Key}: {h.Value}"))}" +
                $"\r\n\r\n{Content}";
        }
    }
}