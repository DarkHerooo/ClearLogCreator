using Microsoft.Win32;
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
using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;
using ClearLogCreator.Classes;
using ClearLogCreator.Classes.User;

namespace ClearLogCreator.Pages
{
    /// <summary>
    /// Логика взаимодействия для SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();

            TbMinecraftLogFolder.Text = User.UserSettings.MinecraftLogsFolder;
            TbMyLogFolder.Text = User.UserSettings.MyLogsFolder;
            ChbDeleteUselessFilesInMinecraftFolder.IsChecked = User.UserSettings.DeleteUselessFilesInMinecraftFolder;
            ChbDeleteUselessFilesInMyFolder.IsChecked = User.UserSettings.DeleteUselessFilesInMyFolder;
        }

        private void BtnMinecraftLogFolder_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = 
                MessageBox.Show("Изменение адреса лог-папки майнкрафта задаётся системой автоматически, " +
                "поэтому вам не нужно её искать самому. Однако, бывают редкие случаи, когда эта папка " +
                "не находится там, где предполагает система. Только в этом случае меняйте адрес. " +
                "Если вы хотите снова автоматически определить папку, нажмите \"Отмена\", если изменить -" +
                "\"Ок\"", "Смена лог-папки майнкрафта", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

            if (result == MessageBoxResult.OK)
            {
                CommonOpenFileDialog dialog = new CommonOpenFileDialog();
                dialog.IsFolderPicker = true;
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                    TbMinecraftLogFolder.Text = dialog.FileName;
            }
            else
            {
                TbMinecraftLogFolder.Text =
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.minecraft\logs";
            }
        }

        private void BtnMyLogsFolder_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                TbMyLogFolder.Text = dialog.FileName;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            User.UserSettings.MinecraftLogsFolder = TbMinecraftLogFolder.Text;
            User.UserSettings.MyLogsFolder = TbMyLogFolder.Text;
            User.UserSettings.DeleteUselessFilesInMinecraftFolder = 
                (bool)ChbDeleteUselessFilesInMinecraftFolder.IsChecked;
            User.UserSettings.DeleteUselessFilesInMyFolder = (bool)ChbDeleteUselessFilesInMyFolder.IsChecked;
            User.SaveUser();
            NavigationService.GoBack();
        }
    }
}
