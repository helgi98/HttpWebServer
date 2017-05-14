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
        public string Method { get; set; }
        public Uri Url { get; set; }
        public string Content { get; set; }
        public Dictionary<string, string> Headers { get; set; }

        public Dictionary<String, String> GetQueryPrameters()
        {
            Dictionary<string, string> queryParameters = new Dictionary<string, string>();

            try
            {
                var query = new Uri(new Uri("http://localhost/"), Url).Query;
                var queryData = WebUtility.UrlDecode(query).Substring(1).Split(';').Where(str => str != "");
                foreach (var pair in queryData)
                {
                    string[] parts = pair.Split('=');
                    queryParameters.Add(parts[0], parts[1]);
                }
            }
            catch { }

            return queryParameters;
        }

        public String GetHeader(String header)
        {
            Headers.TryGetValue(header, out String value);
            return value;
        }

        public HttpRequest()
        {
            Headers = new Dictionary<string, string>();
        }

        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(Content))
                if (!Headers.ContainsKey("Content-Length"))
                    Headers.Add("Content-Length", Content.Length.ToString());

            return $"{Method} {Url} HTTP/1.0\r\n{string.Join("\r\n", Headers.Select(h => $"{h.Key}: {h.Value}"))}" +
                $"\r\n\r\n{Content}";
        }
    }
}
