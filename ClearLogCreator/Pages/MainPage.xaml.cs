using ClearLogCreator.Classes.Global;
using ClearLogCreator.Classes.User;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClearLogCreator.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void BtnMyLogFiles_Click(object sender, RoutedEventArgs e)
        {
            bool trueSettings = true;

            if (String.IsNullOrEmpty(User.UserSettings.MinecraftLogsFolder) || 
                String.IsNullOrEmpty(User.UserSettings.MyLogsFolder))
            {
                MessageBox.Show("Настройки пользователя не установлены. Заполните все пустые поля в меню настроек.",
                    "Настройки не установлены", MessageBoxButton.OK, MessageBoxImage.Warning);
                trueSettings = false;
            }

            if (trueSettings)
            try
            {
                string[] files = Directory.GetFiles(User.UserSettings.MinecraftLogsFolder);
            }
            catch
            {
                MessageBox.Show("Не удалось обратиться к папке майнкрафта. Вероятнее всего, " +
                    "указанной папки не существует. Укажите другую папку.", "Ошибка лог-папки майнкрафта",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                User.UserSettings.MinecraftLogsFolder = null;
                trueSettings = false;
            }

            if (trueSettings)
            try
            {
                string[] files = Directory.GetFiles(User.UserSettings.MyLogsFolder);
            }
            catch
            {
                MessageBox.Show("Не удалось обратиться к вашей личной папке. Вероятнее всего, " +
                    "указанной папки не существует. Укажите другую папку.", "Ошибка личной лог-папки",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                User.UserSettings.MyLogsFolder = null;
                trueSettings = false;
            }

            if (trueSettings) NavigationService.Navigate(new SelectLogPage());
            else NavigationService.Navigate(new SettingsPage());
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SettingsPage());
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            MainWindowApp.window.Close();
        }
    }
}
