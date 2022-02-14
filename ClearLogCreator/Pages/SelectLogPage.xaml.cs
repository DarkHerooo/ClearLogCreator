using ClearLogCreator.Classes;
using ClearLogCreator.Classes.User;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace ClearLogCreator.Pages
{
    /// <summary>
    /// Логика взаимодействия для SelectLogPage.xaml
    /// </summary>
    public partial class SelectLogPage : Page
    {
        public SelectLogPage()
        {
            InitializeComponent();

            if (User.UserSettings.CopyFilesFromMinecraftFolder)
                CopyNewFilesInFolder(User.UserSettings.MinecraftLogsFolder, User.UserSettings.MyLogsFolder);

            ShowLogFiles();
        }

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

        /// <summary>
        /// Показывает все существующие лог-файлы в указанной папке
        /// </summary>
        private void ShowLogFiles()
        {
            string[] files;
            if (User.UserSettings.HideUselessFiles)
                files = LogWorker.GetUsefulFilesInFolder(User.UserSettings.MyLogsFolder);
            else
                files = Directory.GetFiles(User.UserSettings.MyLogsFolder);

            LogFile[] logFiles = LogWorker.GetLogFiles(files);
            DgLogs.ItemsSource = logFiles;
        }

        /// <summary>
        /// Извлекает выбранный лог-файл и открывает его
        /// </summary>
        private void OpenLogFile()
        {
            if (DgLogs.SelectedItem != null)
            {
                LogFile selectLogFile = DgLogs.SelectedItem as LogFile;
                NavigationService.Navigate(new ShowClearLogPage(selectLogFile.FileSource));
            }
        }

        /// <summary>
        /// Извлекает выбранный лог-файл и открывает его
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgLogs_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenLogFile();
        }

        private void BtnBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtnOpen_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OpenLogFile();
        }

        private void DgLogs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BtnOpen.IsEnabled = DgLogs.SelectedItems.Count > 0;
        }
    }
}
