using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearLogCreator.Classes
{
    public static class CustomTextReader
    {
        public static string[] GetText(string fileSource)
        {
            FileInfo file = new FileInfo(fileSource);

            switch(file.Extension)
            {
                case ".gz": return GetGZFileText(fileSource);
            }

            return GetDefaultFileText(fileSource);
        }

        private static string[] GetGZFileText(string fileSource)
        {
            FileStream compressedFileStream = File.Open(fileSource, FileMode.Open);
            GZipStream gZipStream = new GZipStream(compressedFileStream, CompressionMode.Decompress);

            string tempFileName = "temp.txt";
            FileStream outputFileStream = File.Create(tempFileName);
            gZipStream.CopyTo(outputFileStream);

            compressedFileStream.Close();
            gZipStream.Close();
            outputFileStream.Close();

            string[] lines = File.ReadAllLines(tempFileName, Encoding.Default);
            File.Delete(tempFileName);

            return lines;
        }

        private static string[] GetDefaultFileText(string fileSource)
        {
            string[] lines = File.ReadAllLines(fileSource, Encoding.UTF8);
            return lines;
        }
    }
}
