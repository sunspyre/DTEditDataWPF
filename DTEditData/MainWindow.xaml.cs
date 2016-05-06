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

        internal List<Record> _badgeList;
        internal List<DataTrackFile> _rwdList;

        public MainWindow()
        {
            InitializeComponent();
            _currentDirectory = BADGEFILE; //get path by args?
            GetBadges();
            GetFiles();
            PopulateControls();
        }

        #region Startup

        private void GetBadges()
        {
            try
            {
                DefReader br = new DefReader(_currentDirectory);
                _badgeList = br.GetFileContents(BADGEFILE).ToList();
            }
            catch (Exception ex) {ExceptionHandler.Handle(ex);}
        }

        private void GetFiles()
        {
            try
            {
                RwdReader rr = new RwdReader(_currentDirectory);
                _rwdList = rr.GetFolderContents().ToList();
            }
            catch (Exception ex) { ExceptionHandler.Handle(ex); }
        }

        private void PopulateControls()
        {
            gridMain.Columns.Add(new DataGridTextColumn {
                Header = "Badge ID",
                Binding = new Binding("Badge ID")
            });

            gridMain.Columns.Add(new DataGridTextColumn
            {
                Header = "Button ID",
                Binding = new Binding("Button ID")
            });

            gridMain.Columns.Add(new DataGridTextColumn
            {
                Header = "Probe",
                Binding = new Binding("Probe")
            });

            gridMain.Columns.Add(new DataGridTextColumn
            {
                Header = "Date",
                Binding = new Binding("Date")
            });

        }

        #endregion

        #region Events

        #endregion
    }
}
