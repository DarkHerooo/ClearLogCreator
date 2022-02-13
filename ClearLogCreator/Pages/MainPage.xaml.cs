using ClearLogCreator.Classes.Global;
using ClearLogCreator.Classes.User;
using System;
using System.Collections.Generic;
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
            if (String.IsNullOrEmpty(User.UserSettings.MinecraftLogsFolder) || 
                String.IsNullOrEmpty(User.UserSettings.MyLogsFolder))
            {
                MessageBox.Show("Настройки пользователя не установлены. Заполните все пустые поля в меню настроек.",
                    "Настройки не установлены", MessageBoxButton.OK, MessageBoxImage.Warning);
                NavigationService.Navigate(new SettingsPage());
            }
            else
                NavigationService.Navigate(new SelectLogPage());
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
