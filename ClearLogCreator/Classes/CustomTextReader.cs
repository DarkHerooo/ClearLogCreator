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
        public static string[] GetText(string fileSource, Encoding encoding)
        {
            FileInfo file = new FileInfo(fileSource);

            switch(file.Extension)
            {
                case ".gz": return GetGZFileText(fileSource, encoding);
            }

            return GetDefaultFileText(fileSource, encoding);
        }

        private static string[] GetGZFileText(string fileSource, Encoding encoding)
        {
            FileStream compressedFileStream = File.Open(fileSource, FileMode.Open);
            GZipStream gZipStream = new GZipStream(compressedFileStream, CompressionMode.Decompress);

            string tempFileName = "temp.txt";
            FileStream outputFileStream = File.Create(tempFileName);
            gZipStream.CopyTo(outputFileStream);

            compressedFileStream.Close();
            gZipStream.Close();
            outputFileStream.Close();

            string[] lines = File.ReadAllLines(tempFileName, encoding);
            File.Delete(tempFileName);

            return lines;
        }

        private static string[] GetDefaultFileText(string fileSource, Encoding encoding)
        {
            string[] lines = File.ReadAllLines(fileSource, encoding);
            return lines;
        }
    }
}
