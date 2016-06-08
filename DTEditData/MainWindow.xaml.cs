using DataTrack.IO;
using DataTrack.IO.Structs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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
        internal List<DataTrackFile> _rwdList;

        public MainWindow()
        {
            InitializeComponent();
            GetArgs();
            GetBadges();
            GetFiles();
            GetOptions(); //MainWindow.Options.cs
            PopulateControls(); //MainWindow.GridLogic.cs
        }


        private void GetArgs()
        {
            _currentDirectory = Environment.CurrentDirectory; //get path by args?
            var arguments = Environment.GetCommandLineArgs();
            if (arguments.Length > 1)
            {
                if (!Directory.Exists(arguments[1]))
                {
                    MessageBox.Show($"Could not locate directory: {arguments[1]}");
                    return;
                }
                _currentDirectory = arguments[1];
            }
        }

        private void GetBadges()
        {
            try
            {
                DefReader br = new DefReader(_currentDirectory);
                _badgeList = (List<Badge>)br.GetFileContents(BADGEFILE);
            }
            catch (Exception ex) { ExceptionHandler.Handle(ex, "Error getting list of badges"); }
        }

        private void GetFiles()
        {
            try
            {
                RwdReader rr = new RwdReader(_currentDirectory);
                _rwdList = rr.GetFolderContents().ToList();
            }
            catch (Exception ex) { ExceptionHandler.Handle(ex, "Error getting list of RWD files"); }
        }

        
    }
}
