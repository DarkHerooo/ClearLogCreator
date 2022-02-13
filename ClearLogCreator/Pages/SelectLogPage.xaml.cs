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
            PrepareLogFiles();

            InitializeComponent();
            ShowLogFiles();
        }

        /// <summary>
        /// Подготавливает лог-файлы к работе. 
        /// Копирует новые лог-файлы из папки с майнкрафтом в личную папку.
        /// Проверяет файлы в личной папке и предлагает удалить лишние.
        /// </summary>
        private void PrepareLogFiles()
        {
            if (User.UserSettings.DeleteUselessFilesInMinecraftFolder)
            {
                string[] files = LogWorker.GetUselessFilesInFolder(User.UserSettings.MinecraftLogsFolder);
                if (files.Length > 0)
                {
                    MessageBoxResult result =
                        MessageBox.Show("В папке лог-файлов майнкрафта найдено " + files.Length + " ненужных файлов\n" +
                        "Удалить их?", "Удаление ненужных файлов", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

                    if (result == MessageBoxResult.Yes)
                        foreach (var file in files) File.Delete(file);
                }
            }

            FileWorker.CopyNewFilesInFolder(User.UserSettings.MinecraftLogsFolder, User.UserSettings.MyLogsFolder);

            if (User.UserSettings.DeleteUselessFilesInMyFolder)
            {
                string[] files = LogWorker.GetUselessFilesInFolder(User.UserSettings.MyLogsFolder);
                if (files.Length > 0)
                {
                    MessageBoxResult result =
                        MessageBox.Show("В личной папке лог-файлов найдено " + files.Length + " ненужных файлов\n" +
                        "Удалить их?", "Удаление ненужных файлов", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

                    if (result == MessageBoxResult.Yes)
                        foreach (var file in files) File.Delete(file);
                }
            }
        }

        /// <summary>
        /// Показывает все существующие лог-файлы в указанной папке
        /// </summary>
        private void ShowLogFiles()
        {
            LogFile[] logFiles = GetLogFiles(User.UserSettings.MyLogsFolder);
            DgLogs.ItemsSource = logFiles;
        }

        /// <summary>
        /// Получает все лог-файлы из указанной папки.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private LogFile[] GetLogFiles(string path)
        {
            string[] logFileSources = Directory.GetFiles(path);
            List<LogFile> logFiles = new List<LogFile>();
            foreach (var logFileSource in logFileSources)
            {
                FileInfo file = new FileInfo(logFileSource);
                if (file.Extension == ".gz" || file.Extension == ".log")
                {
                    string name = file.Name;
                    string createDate = file.LastWriteTime.ToString("D");
                    string fileSource = logFileSource;
                    LogFile logFile = new LogFile(name, createDate, fileSource);
                    logFiles.Add(logFile);
                }
            }

            return logFiles.ToArray();
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
