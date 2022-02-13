using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClearLogCreator.Classes
{
    public static class FileWorker
    {
        /// <summary>
        /// Копирует новые файлы из одной папки в другую.
        /// </summary>
        /// <param name="oldFolderSource"></param>
        /// <param name="newFolderSource"></param>
        public static void CopyNewFilesInFolder(string oldFolderSource, string newFolderSource)
        {
            string[] fileSourcesInOldFolder = Directory.GetFiles(oldFolderSource);

            List<FileInfo> filesInOldFolder = new List<FileInfo>();
            foreach (var fileInOldFolder in fileSourcesInOldFolder)
                filesInOldFolder.Add(new FileInfo(fileInOldFolder));

            string[] fileSourcesInNewFolder = Directory.GetFiles(newFolderSource);

            List<FileInfo> filesInNewFolder = new List<FileInfo>();
            foreach (var fileSourceInNewFolder in fileSourcesInNewFolder)
                filesInNewFolder.Add(new FileInfo(fileSourceInNewFolder));

            List<FileInfo> newFiles = new List<FileInfo>();
            foreach (var fileInOldFolder in filesInOldFolder)
            {
                bool fileExists = false;
                foreach (var fileInNewFolder in filesInNewFolder)
                {
                    if (fileInOldFolder.Name == fileInNewFolder.Name)
                    {
                        fileExists = true;
                        break;
                    }
                }

                if (!fileExists) newFiles.Add(fileInOldFolder);
            }

            foreach (var newFile in newFiles)
                newFile.CopyTo(newFolderSource + @"\" + newFile.Name);
        }
    }
}
