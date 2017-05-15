using HttpWebServer.application;
using HttpWebServer.http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

        private HttpRequest ReadRequest(Stream inputStream)
        {
            HttpRequest req = new HttpRequest();
            //Read Request Line
            string request = ReadLine(inputStream);

            string[] tokens = request.Split(' ');
            if (tokens.Length != 3)
            {
                throw new Exception("Invalid http request");
            }

            req.Method = tokens[0].ToUpper();

            if (tokens[1].IndexOf('?') == -1)
                req.Url = new Uri(tokens[1], UriKind.Relative);
            else req.Url = new Uri(new string(tokens[1].TakeWhile(ch => ch != '?').ToArray()), UriKind.Relative);

            try
            {
                String query = new Uri(new Uri("http://loaclhost/"), tokens[1]).Query.Substring(1);
                if (query != "")
                {
                    var queryData = WebUtility.UrlDecode(query).Split(';').Where(str => str != "");
                    foreach (var pair in queryData)
                    {
                        string[] parts = pair.Split('=');
                        req.AddQueryParameter(parts[0], parts[1]);
                    }
                }
            }
            catch { }

            //Read Headers
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

                if (name == "Cookie")
                {
                    String[] cookies = value.Split(';').Where(c => c != "").ToArray();
                    foreach(var cookie in cookies)
                    {
                        String[] pair = cookie.Trim().Split('=');
                        req.AddCookie(new http.Cookie { Name = pair[0], Value = pair[1] });
                    }
                }

                req.AddHeader(name, value);
            }

            string content = null;

            //Read content
            if (req.GetHeader("Content-Length") != null)
            {
                int totalBytes = Convert.ToInt32(req.GetHeader("Content-Length"));
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

            req.Content = content;


            return req;
        }

        #endregion


    }
}
