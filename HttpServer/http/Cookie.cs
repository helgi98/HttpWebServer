using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer.http
{
    public class Cookie
    {
        public String Name { get; set; }
        public String Value { get; set; }
        public String Path { get; set; }
        public int MaxAge { get; set; }

        public override string ToString()
        {
            StringBuilder builder =  new StringBuilder($"{Name}={Value}; ");
            if (Path != null) builder.Append($"Path={Path} ");
            if (MaxAge != 0) builder.Append($"Max-Age={MaxAge} ");

            return builder.ToString();
        }
    }
}
