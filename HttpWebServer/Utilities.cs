﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer
{
    public static class Utilities
    {
        public static String GetFileName(String path)
        {
            return new string(path.Reverse().TakeWhile(ch => ch != '/' || ch != '\\').Reverse().ToArray());
        }

        public static String GetFileExtension(String path)
        {
            return new String(path.Reverse().TakeWhile(ch => ch != '.').Reverse().ToArray());
        }

        public static T CreateClassExample<T>(string className)
        {
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            foreach (var asm in loadedAssemblies)
            {
                var classes = asm.GetTypes(); 
                foreach (var c in classes)
                {
                    if (c.FullName == className)
                        return (T)Activator.CreateInstance(c);
                }
            }
            throw new Exception("No such class");
        }

        public static Stream ToStream(this string str)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(str);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
        public static string ReplaceFirst(this string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }
    }
}
