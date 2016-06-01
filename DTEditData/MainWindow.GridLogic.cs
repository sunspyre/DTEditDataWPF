using DataTrack.IO;
using DataTrack.IO.Structs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DTEditData
{
    public partial class MainWindow : Window
    {
        private BackgroundWorker _backgroundWorker;
        private DataTrackFile _currentFile;

        private void PopulateControls()
        {
            #region RWD Grid
            gridDates.Columns.Add(new DataGridTextColumn
            {
                Header = "Date",
                Binding = new Binding("FileName")
            });
            gridDates.ItemsSource = _rwdList;
            #endregion

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

            gridBadges.ItemsSource = _badgeList;

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



        }

        private void BuildGrid(DataTrackFile fileToLoad)
        {
            _currentFile = fileToLoad;
            SetUpBackgroundWorker(_currentFile);

        }

        private void SetUpBackgroundWorker(DataTrackFile file)
        {
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.WorkerReportsProgress = false;
            _backgroundWorker.WorkerSupportsCancellation = false;
            _backgroundWorker.DoWork += _backgroundWorker_DoWork;
            _backgroundWorker.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted;

            _backgroundWorker.RunWorkerAsync(file.Path);
        }

        private void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            RwdReader reader = new RwdReader(e.Argument.ToString());
            IEnumerable<Record> list = reader.GetFileContents(e.Argument.ToString());
            e.Result = list;
        }

        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IEnumerable<Record> list = (IEnumerable<Record>)e.Result;

            
        }

        private void Isolate()
        {

        }

    }
}
