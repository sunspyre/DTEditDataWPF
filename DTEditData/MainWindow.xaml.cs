using DataTrack.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DTEditData
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string _currentDirectory;

        private const string DEFFOLDER = "def";
        private const string DATAFOLDER = "data";
        private const string BADGEFILE = "badge.def";

        internal List<Badge> _badgeList;

        public MainWindow()
        {
            InitializeComponent();
            GetBadges();
            GetFiles();
        }

        private void GetFiles()
        {
            DirectoryInfo di = new DirectoryInfo($@"{_currentDirectory}\{DATAFOLDER}");
        }

        private void GetBadges()
        {
            _currentDirectory = BADGEFILE; //get path by args?
            BadgeReader br = new BadgeReader($@"{_currentDirectory}\{DEFFOLDER}\{BADGEFILE}");
            _badgeList = br.GetList();
        }

        #region Events

        #endregion
    }
}
