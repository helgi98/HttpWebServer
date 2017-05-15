using HttpWebServer.controllers;
using HttpWebServer.http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HttpWebServer.application
{
    public class ControllerContatiner
    {
        private RouteHandler _router;
        private IResourceResolver _resolver;
        private Dictionary<String, Controller> _controllers;

        private void ConfigureDispatcher(String configPath)
        {
            try
            {
                _controllers = new Dictionary<string, Controller>();

                XDocument doc = XDocument.Load(configPath);
                XElement root = doc.Root;

                //Configures route handler
                var controllersElement = root.Element("Controllers");
                _router = RouteHandler.CreateRouteHandler(controllersElement);

                //Configures resolver
                var resolverElement = root.Element("ResourceResolver");
                _resolver = 
                    Utilities.CreateClassExample<IResourceResolver>(resolverElement.Element("Resolver-class").Value);
                _resolver.Path = resolverElement.Element("Path") == null ? "resources/" : resolverElement.Element("Path").Value;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                _resolver = new ResourceResolver() { Path = "/" };
            }
        }

        private bool RequestsResource(HttpRequest req)
        {
            return req.Method.ToUpper() == "GET" && new Regex(".*\\.\\w+").IsMatch(req.Url.ToString());
        }

        private Uri GetResourceUrl(HttpRequest req)
        {
            Uri url = req.Url;

            if (req.GetHeader("Referer") != null)
            {
                Uri referer;
                Uri.TryCreate(req.GetHeader("Referer"), UriKind.Absolute, out referer);
                Uri.TryCreate(new Uri(referer.GetLeftPart(UriPartial.Authority)), url, out url);

                return referer.MakeRelativeUri(url);
            }

            return url;
        }

        private Controller GetController(String ctrlClass)
        {
            Controller controller;
            _controllers.TryGetValue(ctrlClass, out  controller);
            if (controller == null)
            {
                controller = Utilities.CreateClassExample<Controller>(ctrlClass);
                controller.Dispatcher = this;
            }
            return controller;
        }

        public ControllerContatiner(String configPath)
        {
            ConfigureDispatcher(configPath);
        }

        public void Forward(HttpRequest req, HttpResponse res, String where)
        {
            _resolver.Resolve(where).Process(req, res);
        }

        public void Process(ref HttpRequest req, ref HttpResponse res)
        {
            if (RequestsResource(req))
            {
                Uri url = GetResourceUrl(req);
                IResource resource = _resolver.Resolve(url.ToString());
                resource.Process(req, res);
            }
            else
            {

                Controller controller = GetController(_router.RouteRequest(req.Url.ToString()));
                
                if (controller != null)
                {
                    try
                    {
                        switch (req.Method)
                        {
                            case "GET": controller.DoGet(req, res); break;
                            case "POST": controller.DoPost(req, res); break;
                            case "DELETE": controller.DoDelete(req, res); break;
                            case "PUT": controller.DoPut(req, res); break;
                        }
                    }
                    catch
                    {
                        res = ResponseDefaultBuilder.InternalServerError();
                    }
                }
                else res = ResponseDefaultBuilder.NotFound();
            }
        }
    }
}
