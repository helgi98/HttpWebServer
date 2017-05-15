using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using HttpWebServer.application;

namespace HttpWebServer
{
    public class HttpServer
    {
        private Dictionary<String, Application> _applications;
        private static HttpServer _server;

        private HttpServer()
        {
            _applications = new Dictionary<string, Application>();
        }

        public static HttpServer GetServer()
        {
            if (_server == null) _server = new HttpServer();

            return _server;
        }

        private void RunApplication(Application app, int port)
        {
            HttpProcessor processor = new HttpProcessor(app);
            TcpListener listener = new TcpListener(IPAddress.Loopback, port);
            listener.Start();

            while (app.IsActive)
            {
                TcpClient client = listener.AcceptTcpClient();

                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;

                    processor.HandleClient(client);
                }).Start();

             }
            listener.Stop();
        }

        public void RunApplication(string name, int port)
        {
            Application app;
            _applications.TryGetValue(name, out app);
            if (app != null && !app.IsActive)
            {
                app.IsActive = true;
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;

                    RunApplication(app, port);
                }).Start();
            }
        }

        public void StopApplication(string name)
        {
            Application app;
            _applications.TryGetValue(name, out app);
            if (app != null) app.IsActive = false;
        }

        public void AddApplication(string name)
        {
            _applications.Add(name, new Application(name));
        }

        public void RemoveApplication(string name)
        {
            _applications.Remove(name);
        }

    }
}
