﻿using HttpWebServer.controllers;
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
        private Dictionary<String, String> _variables;

        private void ConfigureDispatcher(String configPath)
        {
            try
            {
                _controllers = new Dictionary<string, Controller>();
                _variables = new Dictionary<string, string>();

                XDocument doc = XDocument.Load(configPath);
                XElement root = doc.Root;

                //
                var variablesElement = root.Element("Variables");
                if (variablesElement != null)
                {
                    foreach (var variableElement in variablesElement.Elements("Variable"))
                    {
                        _variables.Add(variableElement.Element("Name").Value, variableElement.Element("Value").Value);
                    }
                }

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

        private Controller GetController(String ctrlClass)
        {
            Controller controller;
            _controllers.TryGetValue(ctrlClass, out  controller);
            if (controller == null)
            {

                controller = Utilities.CreateClassExample<Controller>(ctrlClass);
                controller.Container = this;
            }
            return controller;
        }

        public ControllerContatiner(String configPath)
        {
            ConfigureDispatcher(configPath);
        }

        public String GetVariable(String key)
        {
            String value;
            _variables.TryGetValue(key, out value);

            return value;
        }

        public void Forward(HttpRequest req, HttpResponse res, String where)
        {
            res.StatusCode = "307";
            _resolver.Resolve(where).Process(req, res);
        }

        public void Process(ref HttpRequest req, ref HttpResponse res)
        {
            if (RequestsResource(req))
            {
                IResource resource = _resolver.Resolve(req.Url.ToString());
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
