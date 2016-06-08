using DataTrack.IO;
using DataTrack.IO.Structs;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DTEditData
{
    public partial class MainWindow : Window
    {
        private BackgroundWorker _backgroundWorker;
        private DataTrackFile _currentFile;

        private const string DATECOLUMN = "Date";
        private const string TIMECOLUMN = "Time";
        private const string PROBECOLUMN = "ProbeID";
        private const string BADGECOLUMN = "BadgeID";
        private const string BUTTONCOLUMN = "ButtonID";
        private const string TYPECOLUMN = "Type";
        private const string DESCCOLUMN = "Desc";
        private const string MISCCOLUMN = "Misc";
        private const string SPECIALCOLUMN = "Sp";
        private const string FLAGCOLUMN = "Flag";


        private void PopulateControls()
        {
            #region gridDates
            gridDates.Columns.Add(new DataGridTextColumn
            {
                Header = "Date",
                Binding = new Binding("FileName")
            });
            gridDates.ItemsSource = _rwdList;
            #endregion

            #region gridBadges

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

            #region gridAbstract

            gridAbstract.Columns.Add(new DataGridTextColumn
            {
                Header = "Time",
                Binding = new Binding("Time"),
                IsReadOnly = true
            });
            gridAbstract.Columns.Add(new DataGridTextColumn
            {
                Header = "Device",
                Binding = new Binding("Device"),
                IsReadOnly = true
            });
            gridAbstract.Columns.Add(new DataGridTextColumn
            {
                Header = "Probe",
                Binding = new Binding("Probe"),
                IsReadOnly = false
            });
            gridAbstract.Columns.Add(new DataGridTextColumn
            {
                Header = "Button",
                Binding = new Binding("Button"),
                IsReadOnly = true
            });

            /*
                //Hide a column:
            gridMain.Columns[0].Visibility = Visibility.Collapsed;
            */

            #endregion

            #region gridMain

            gridMain.Columns.Add(new DataGridTextColumn
            {
                Header = DATECOLUMN,
                Binding = new Binding("Date"),
                IsReadOnly = true
            });
            gridMain.Columns.Add(new DataGridTextColumn
            {
                Header = TIMECOLUMN,
                Binding = new Binding("Time"),
                IsReadOnly = true
            });
            gridMain.Columns.Add(new DataGridTextColumn
            {
                Header = FLAGCOLUMN,
                Binding = new Binding("Flag"),
                IsReadOnly = true
            });
            gridMain.Columns.Add(new DataGridTextColumn
            {
                Header = PROBECOLUMN,
                Binding = new Binding("ProbeId"),
                IsReadOnly = true
            });
            gridMain.Columns.Add(new DataGridTextColumn
            {
                Header = BADGECOLUMN,
                Binding = new Binding("BadgeId"),
                IsReadOnly = true
            });
            gridMain.Columns.Add(new DataGridTextColumn
            {
                Header = SPECIALCOLUMN,
                Binding = new Binding("Special"),
                IsReadOnly = true
            });
            gridMain.Columns.Add(new DataGridTextColumn
            {
                Header = TYPECOLUMN,
                Binding = new Binding("Type"),
                IsReadOnly = true
            });
            gridMain.Columns.Add(new DataGridTextColumn
            {
                Header = DESCCOLUMN,
                Binding = new Binding("Desc"),
                IsReadOnly = true
            });
            gridMain.Columns.Add(new DataGridTextColumn
            {
                Header = MISCCOLUMN,
                Binding = new Binding("Misc"),
                IsReadOnly = true
            });
            gridMain.Columns.Add(new DataGridTextColumn
            {
                Header = BUTTONCOLUMN,
                Binding = new Binding("ButtonId"),
                IsReadOnly = true
            });


            #endregion
        }

        private void BuildGrid(DataTrackFile fileToLoad) => SetUpBackgroundWorker(fileToLoad);

        private void PopulateGrid(IEnumerable<Record> list)
        {
            gridAbstract.ItemsSource = list;
        }

        private void PopulateGrid(List<RwdRecord> list)
        {
            gridMain.ItemsSource = list;
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
            IEnumerable<Record> scanList = reader.GetFileContents(e.Argument.ToString());
            List<RwdRecord> filteredRecordList = new List<RwdRecord>();

            foreach (Scan item in scanList)
            {
                bool exists = _badgeList.Exists(x => x.ButtonId.Equals(item.Button));
                if (exists)
                {
                    Badge tempBadge = _badgeList.Find(x => x.ButtonId.Equals(item.Button));

                    filteredRecordList.Add(new RwdRecord {
                        Date = item.Time.ToShortDateString(),
                        Time = item.Time.ToShortTimeString(),
                        Flag = "",
                        ProbeId = item.Probe,
                        BadgeId = tempBadge.BadgeId,
                        Special = tempBadge.Special,
                        Type1 = tempBadge.Type1,
                        Type2 = tempBadge.Type2,
                        Desc = tempBadge.Desc,
                        Misc = tempBadge.Misc,
                        ButtonId = tempBadge.ButtonId,
                        ExtraDetails = tempBadge.ExtraDetails.Split(';')
                    });
                }
                else
                {
                    filteredRecordList.Add(new RwdRecord
                    {
                        Date = item.Time.ToShortDateString(),
                        Time = item.Time.ToShortTimeString(),
                        Flag = "",
                        ProbeId = item.Probe
                    });
                }
            }

            e.Result = filteredRecordList;
        }

        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) => PopulateGrid((List<RwdRecord>)e.Result);


        private void Isolate()
        {

        }

    }
}
