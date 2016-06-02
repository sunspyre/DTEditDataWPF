using DataTrack.IO;
using DataTrack.IO.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DTEditData
{
    public partial class MainWindow : Window
    {
        private void gridDates_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                foreach (DataTrackFile item in gridDates.SelectedItems)
                {
                    ActivityLog.Log($"Opening RWD {item.Path}");
                    MessageBox.Show(item.Path);
                    BuildGrid(item);
                    break; //No need to load more than one RWD
                }
            }
            catch(Exception ex) { ExceptionHandler.Handle(ex, "Error on date grid click event"); }
        }
    }
}
