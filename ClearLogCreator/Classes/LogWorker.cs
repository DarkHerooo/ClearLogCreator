using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearLogCreator.Classes
{
    public static class LogWorker
    {
        /// <summary>
        /// Возвращает только строки чата в лог-файле
        /// </summary>
        /// <param name="lines"></param>
        /// <returns>Массив строк чата</returns>
        public static string[] GetChatLines(string[] lines)
        {
            List<string> chatLines = new List<string>();

            chatLines = lines.Where(line => line.Contains("[CHAT] <")).ToList();

            for (int i = 0; i < chatLines.Count; i++)
            {
                int lastIndex = chatLines[i].IndexOf('<');
                chatLines[i] = chatLines[i].Remove(0, lastIndex);
                chatLines[i] += "\n";
            }

            return chatLines.ToArray();
        }

        /// <summary>
        /// Возвращает ненужные файлы в указанной папке.
        /// </summary>
        /// <param name="folderSource"></param>
        public static string[] GetUselessFilesInFolder(string folderSource)
        {
            string[] fileSourcesInFolder = Directory.GetFiles(folderSource);

            List<string> uselessFiles = new List<string>();
            foreach (var fileSource in fileSourcesInFolder)
            {
                bool uselessFile = false;

                if (!uselessFile)
                {
                    string text = File.ReadAllText(fileSource);
                    if (String.IsNullOrEmpty(text)) uselessFile = true;
                }

                if (!uselessFile)
                {
                    string[] lines = CustomTextReader.GetText(fileSource);
                    lines = GetChatLines(lines);
                    if (lines.Length == 0) uselessFile = true;
                }

                if (uselessFile) uselessFiles.Add(fileSource);
            }

            return uselessFiles.ToArray();
        }
    }
}
