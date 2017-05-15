using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer.http
{
    public static class MIMETypes
    {
        public static String GetContentType(String filePath)
        {
            String fileExtension = Utilities.GetFileExtension(filePath);

            switch (fileExtension)
            {
                case "html": return "text/html";
                case "css": return "text/css";
                case "js": return "application/javascript";
                case "png": return "image/png";
                case "jpg": return "image/jpg";
                case "jpeg": return "image/jpeg";
                case "gif": return "image/gif";
                case "zip": return "application/x-compressed";
                case "mp3": return "audio/mpeg3";
                default: return "application/octet-stream";
            }
        }
    }
}
