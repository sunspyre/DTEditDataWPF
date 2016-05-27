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
            foreach (DataTrackFile item in gridDates.SelectedItems)
            {
                MessageBox.Show(item.Path);
            }
        }
    }
}
