using HttpWebServer.controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HttpWebServer.application
{
    class RouteHandler
    {
        private Dictionary<UrlPattern, String> _urlPatterns;
        

        private RouteHandler()
        {
            _urlPatterns = new Dictionary<UrlPattern, string>();
        }

        public static RouteHandler CreateRouteHandler(XElement controllersEl)
        {
            RouteHandler router = new RouteHandler();

            if (controllersEl == null) return router;

            foreach(var controllerEl in controllersEl.Elements())
            {
                if (controllerEl.Name != "Controller")
                    throw new Exception("Invalid config file");
                
                foreach(var urlPattern in controllerEl.Elements("Url-pattern"))
                {
                    router._urlPatterns.Add(new UrlPattern(controllerEl.Element("Url-pattern").Value),
                        controllerEl.Element("Controller-class").Value);
                }
            }

            return router;
        }        

        //Returns controller class
        public String RouteRequest(String url)
        {
            var matchingUrl = _urlPatterns.Keys
                .Where(pattern => pattern.Matches(url))
                .OrderBy(el => el).First();

            return _urlPatterns[matchingUrl];
        }

    }
}
