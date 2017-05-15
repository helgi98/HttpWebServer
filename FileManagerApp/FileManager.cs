using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagerApp
{
    static class FileManager
    {
        public static String RootPath { get; set; }

        static FileManager()
        {
            RootPath = @"C:/";
        }

        public static Stream DownloadFile(String path)
        {
            Stream stream = new FileStream(RootPath + path, FileMode.Open);

            return stream;
        }

        public static Dictionary<String, List<String>> GetDirectoryContent(String path)
        {
            Dictionary<String, List<String>> directoryContent = new Dictionary<string, List<string>>();
            directoryContent.Add("directories", new List<string>());
            directoryContent.Add("files", new List<string>());

            DirectoryInfo currDirectory = new DirectoryInfo(RootPath + path);

            foreach (var file in currDirectory.EnumerateFiles())
                directoryContent["files"].Add(file.Name);

            foreach (var directory in currDirectory.EnumerateDirectories())
                directoryContent["directories"].Add(directory.Name);

            return directoryContent;
        }

        public static void ChangeFileName(string oldName, string newName, string directoryPath)
        {
            File.Move(RootPath + directoryPath + oldName, RootPath + directoryPath + newName);
        }
    }
}
