using DataTrack.IO;
using DataTrack.IO.Structs;
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
            _currentDirectory = Environment.CurrentDirectory; //get path by args?
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
            #region Badge Columns

            gridBadges.Columns.Add(new DataGridTextColumn
            {
                Header = "Badge ID",
                Binding = new Binding("BadgeId")
            });
            gridBadges.Columns.Add(new DataGridTextColumn
            {
                Header = "Button ID",
                Binding = new Binding("ButtonId")
            });
            gridBadges.Columns.Add(new DataGridTextColumn
            {
                Header = "Desc.",
                Binding = new Binding("Desc")
            });
            gridBadges.Columns.Add(new DataGridTextColumn
            {
                Header = "Type",
                Binding = new Binding("Type1")
            });
            gridBadges.Columns.Add(new DataGridTextColumn
            {
                Header = "Type 2",
                Binding = new Binding("Type2")
            });
            gridBadges.Columns.Add(new DataGridTextColumn
            {
                Header = "Sp.",
                Binding = new Binding("Special")
            });
            gridBadges.Columns.Add(new DataGridTextColumn
            {
                Header = "Misc",
                Binding = new Binding("Misc")
            });
            gridBadges.Columns.Add(new DataGridTextColumn
            {
                Header = "Extra",
                Binding = new Binding("ExtraDetails")
            });

            #endregion

            //TODO: implement this
            #region DataTrack Columns

            gridMain.Columns.Add(new DataGridTextColumn
            {
                Header = "Date",
                Binding = new Binding("Date")
            });

            /*
                //Hide a column:
            gridMain.Columns[0].Visibility = Visibility.Collapsed;
            */

            #endregion


            gridBadges.ItemsSource = _badgeList;
        }

        #endregion

        #region Events

        #endregion
    }
}
