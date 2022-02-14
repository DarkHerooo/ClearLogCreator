using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ClearLogCreator.Classes
{
    public static class LogWorker
    {
        /// <summary>
        /// Формирует и возвращает массив LogFile
        /// </summary>
        /// <param name="files"></param>
        /// <returns>Массив LogFile</returns>
        public static LogFile[] GetLogFiles(string[] files)
        {
            List<LogFile> logFiles = new List<LogFile>();
            foreach (var file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                if (fileInfo.Extension == ".gz" || fileInfo.Extension == ".log")
                {
                    string name = fileInfo.Name;
                    string createDate = fileInfo.LastWriteTime.ToString("D");
                    string fileSource = file;
                    LogFile logFile = new LogFile(name, createDate, fileSource);
                    logFiles.Add(logFile);
                }
            }

            return logFiles.ToArray();
        }

        /// <summary>
        /// Возвращает полезные файлы в указанной папке.
        /// </summary>
        /// <param name="folderSource"></param>
        public static string[] GetUsefulFilesInFolder(string folderSource)
        {
            string[] fileSourcesInFolder = Directory.GetFiles(folderSource);

            List<string> usefulFiles = new List<string>();
            foreach (var fileSource in fileSourcesInFolder)
            {
                bool usefulFile = true;

                if (usefulFile)
                {
                    string text = File.ReadAllText(fileSource);
                    if (String.IsNullOrEmpty(text)) usefulFile = false;
                }

                if (usefulFile)
                {
                    string[] lines = CustomTextReader.GetText(fileSource, Encoding.Default);
                    lines = GetChatLines(lines);
                    if (lines.Length == 0) usefulFile = false;
                }

                if (usefulFile) usefulFiles.Add(fileSource);
            }

            return usefulFiles.ToArray();
        }

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
    }
}
