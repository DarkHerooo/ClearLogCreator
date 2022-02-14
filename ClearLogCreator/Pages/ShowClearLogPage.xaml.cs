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
        private string _fileSource;
        public ShowClearLogPage(string fileSource)
        {
            _fileSource = fileSource;

            InitializeComponent();
            CreateEncodingList();

            SetLinesFromFile();
            CreatePlayerList();
            ShowLogFileContent();
        }

        private void SetLinesFromFile()
        {
            Encoding encoding = Encoding.Default;

            switch(CbEncoding.SelectedIndex)
            {
                case 0: encoding = Encoding.Default; break;
                case 1: encoding = Encoding.UTF8; break;
                case 2: encoding = Encoding.ASCII; break;
            }

            _lines = CustomTextReader.GetText(_fileSource, encoding);
            _lines = LogWorker.GetChatLines(_lines);
        }

        private void CreateEncodingList()
        {
            CbEncoding.Items.Clear();

            CbEncoding.Items.Add("Default");
            CbEncoding.Items.Add("UTF-8");
            CbEncoding.Items.Add("ASCII");

            CbEncoding.SelectedIndex = 0;
        }

        private void CreatePlayerList()
        {
            CbPlayers.Items.Clear();

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

        private void CbEncoding_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetLinesFromFile();
            ShowLogFileContent();
        }
    }
}
