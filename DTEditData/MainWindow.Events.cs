using DataTrack.IO;
using System;
using System.Windows;
using System.Windows.Input;

namespace DTEditData
{
    public partial class MainWindow : Window
    {
        private void gridDates_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                foreach (DataTrackFile item in gridDates.SelectedItems)
                {
                    ActivityLog.Log($"Opening RWD {item.Path}");
                    //MessageBox.Show(item.Path); //for testing
                    BuildGrid(item);
                    break; //No need to load more than one RWD at a time
                }
            }
            catch(Exception ex) { ExceptionHandler.Handle(ex, "Error on date grid click event"); }
        }
    }
}
