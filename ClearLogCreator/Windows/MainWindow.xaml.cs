using ClearLogCreator.Classes;
using ClearLogCreator.Classes.Global;
using ClearLogCreator.Classes.User;
using ClearLogCreator.Pages;
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

namespace ClearLogCreator.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            User.SetUser();

            InitializeComponent();

            MainWindowApp.window = this;
            FrmMain.Navigate(new MainPage());
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            User.SaveUser();
        }
    }
}
