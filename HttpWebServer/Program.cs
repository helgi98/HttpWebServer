using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HttpWebServer
{
    class Program
    {
        public static void Main(String[] args)
        {
            //HttpServer.GetServer().AddApplication("user");
            //HttpServer.GetServer().RunApplication("user", 8080);

            var url = new Uri("http://localhost/");

            var query = WebUtility.UrlDecode(url.Query).Split(';').Where(str => str != "").Select(str =>
            {
                var parts = str.Split('=');
                return new { key = parts[0], value = parts[1] };
            });
            Console.WriteLine(query.Count());
            /*UrlPattern pattern = new UrlPattern("/dar/*//*");
            string url1 = "/dar/daeq/da/dsad/dad";
            string url2 = "/dar/daeq/da1/dd";

            Console.WriteLine(pattern.Matches(url1));
            Console.WriteLine(pattern.Matches(url2));*/

            Console.ReadLine();
        }
    }
}
