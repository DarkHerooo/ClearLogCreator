using ClearLogCreator.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
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
    /// Логика взаимодействия для ShowClearLogPage.xaml
    /// </summary>
    public partial class ShowClearLogPage : Page
    {
        private string[] _lines;
        public ShowClearLogPage(string fileSource)
        {
            InitializeComponent();
            SetLinesFromFile(fileSource);
            CreatePlayerList();

            ShowLogFileContent();
        }

        private void SetLinesFromFile(string fileSource)
        {
            _lines = CustomTextReader.GetText(fileSource);
            _lines = LogWorker.GetChatLines(_lines);
        }

        private void CreatePlayerList()
        {
            string[] linesCopy = _lines;
            List<string> players = new List<string>();

            for (int i = 0; i < linesCopy.Length; i++)
            {
                int startIndex = linesCopy[i].IndexOf('<');
                int endIndex = linesCopy[i].IndexOf('>');
                players.Add(linesCopy[i].Substring(startIndex + 1, endIndex - 1));
            }

            players = players.Distinct().ToList();

            CbPlayers.Items.Add("ВСЕ ИГРОКИ");
            foreach (var player in players)
                CbPlayers.Items.Add(player);

            CbPlayers.SelectedIndex = 0;
        }

        private void ShowLogFileContent()
        {
            List<string> linesCopy = _lines.ToList();

            if (CbPlayers.SelectedIndex > 0)
                linesCopy = linesCopy.Where(line => 
                line.Contains("<" + CbPlayers.SelectedItem.ToString() + ">")).ToList();

            TbLogFileContent.Clear();
            foreach (var line in linesCopy)
                TbLogFileContent.Text += line;
        }

        private void CbPlayers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowLogFileContent();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
