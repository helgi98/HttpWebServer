using HttpWebServer.application;
using HttpWebServer.http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HttpWebServer
{
    class HttpProcessor
    {
        private Application _app;

        public HttpProcessor(Application app)
        {
            _app = app;
        }

        #region Public Methods
        public void HandleClient(TcpClient tcpClient)
        {
            Stream inputStream = tcpClient.GetStream();
            Stream outputStream = tcpClient.GetStream();

            HttpRequest request = ReadRequest(inputStream);
            HttpResponse response = ResponseDefaultBuilder.Ok();

            try
            {
                _app.Process(ref request, ref response);
            }
            catch
            {
                response = ResponseDefaultBuilder.InternalServerError();
            }

            // route and handle the request...
            /*HttpResponse response = RouteRequest(inputStream, outputStream, request);

            Console.WriteLine("{0} {1}", response.StatusCode, request.Url);
            // build a default response for errors
            if (response.Content == null)
            {
                if (response.StatusCode != "200")
                {
                    response.ContentAsUTF8 = string.Format("{0} {1} <p> {2}", response.StatusCode, request.Url, response.ReasonPhrase);
                }
            }*/

            try
            {
                WriteResponse(outputStream, response);
            }
            catch { }

            outputStream.Flush();
            outputStream.Close();
            outputStream = null;

            inputStream.Close();
            inputStream = null;
        }


        #endregion

        #region Private Methods
        private static void WriteResponse(Stream stream, HttpResponse response)
        {
            if (!response.Headers.ContainsKey("Content-Type"))
            {
                response.Headers["Content-Type"] = "text/html";
            }
            if (response.Content == null) response.Content = new MemoryStream();

            response.Headers["Content-Length"] = response.Content.Length.ToString();

            WriteText(stream, string.Format("HTTP/1.0 {0} {1}\r\n", response.StatusCode, response.ReasonPhrase));
            WriteText(stream, string.Join("\r\n", response.Headers.Select(x => string.Format("{0}: {1}", x.Key, x.Value))));
            WriteText(stream, "\r\n\r\n");

            WriteBytes(stream, response.Content);
        }

        private static string ReadLine(Stream stream)
        {
            int next_char;
            string data = "";
            while (true)
            {
                next_char = stream.ReadByte();

                if (next_char == '\n') break;
                else if (next_char == '\r') continue;
                else if (next_char == -1)
                {
                    Thread.Sleep(1); continue;
                };

                data += Convert.ToChar(next_char);
            }
            return data;
        }

        private static void WriteText(Stream stream, string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);

            stream.Write(bytes, 0, bytes.Length);
        }

        private static void WriteBytes(Stream stream, Stream data)
        {
            int bufferSize = 1024;
            byte[] buffer = new byte[bufferSize];

            int bytesRead = 0;
            while ((bytesRead = data.Read(buffer, 0, bufferSize)) > 0)
            {
                stream.Write(buffer, 0, bytesRead);
            }
        }

        /*protected virtual HttpResponse RouteRequest(Stream inputStream, Stream outputStream, HttpRequest request)
        {

            List<Route> routes = this.Routes.Where(x => Regex.Match(request.Url, x.UrlRegex).Success).ToList();

            if (!routes.Any())
                return HttpBuilder.NotFound();

            Route route = routes.SingleOrDefault(x => x.Method == request.Method);

            if (route == null)
                return new HttpResponse()
                {
                    ReasonPhrase = "Method Not Allowed",
                    StatusCode = "405",

                };

            // extract the path if there is one
            var match = Regex.Match(request.Url, route.UrlRegex);
            if (match.Groups.Count > 1)
            {
                request.Path = match.Groups[1].Value;
            }
            else
            {
                request.Path = request.Url;
            }

            // trigger the route handler...
            request.Route = route;
            try
            {
                return route.Callable(request);
            }
            catch (Exception ex)
            {
                return HttpBuilder.InternalServerError();
            }

        }*/

        private HttpRequest ReadRequest(Stream inputStream)
        {
            //Read Request Line
            string request = ReadLine(inputStream);

            string[] tokens = request.Split(' ');
            if (tokens.Length != 3)
            {
                throw new Exception("Invalid http request");
            }

            string method = tokens[0].ToUpper();
            Uri url = new Uri(tokens[1], UriKind.Relative);
            string protocolVersion = tokens[2];

            //Read Headers
            Dictionary<string, string> headers = new Dictionary<string, string>();
            string line;
            while ((line = ReadLine(inputStream)) != null)
            {
                if (line.Equals("")) break;

                int separator = line.IndexOf(':');
                if (separator == -1)
                    throw new Exception("invalid http header line: " + line);

                string name = line.Substring(0, separator);
                int pos = separator + 1;
                string value = new string(line.Substring(pos).SkipWhile(ch => char.IsWhiteSpace(ch)).ToArray());

                headers.Add(name, value);
            }

            string content = null;

            //Read content
            if (headers.ContainsKey("Content-Length"))
            {
                int totalBytes = Convert.ToInt32(headers["Content-Length"]);
                int bytesLeft = totalBytes;
                byte[] bytes = new byte[totalBytes];

                while (bytesLeft > 0)
                {
                    byte[] buffer = new byte[bytesLeft > 1024 ? 1024 : bytesLeft];

                    int n = inputStream.Read(buffer, 0, buffer.Length);
                    buffer.CopyTo(bytes, totalBytes - bytesLeft);

                    bytesLeft -= n;
                }

                content = Encoding.ASCII.GetString(bytes);
            }


            return new HttpRequest()
            {
                Method = method,
                Url = url,
                Headers = headers,
                Content = content
            };
        }

        #endregion


    }
}
