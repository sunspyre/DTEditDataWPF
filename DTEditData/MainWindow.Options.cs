using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DTEditData
{
    public partial class MainWindow : Window
    {
        private void CheckColumns(object sender, RoutedEventArgs e)
        {
            if (chkShowButtonId.IsChecked.Value)
                gridMain.Columns[0].Visibility = Visibility.Collapsed;
            else
                gridMain.Columns[0].Visibility = Visibility.Visible;



        }
    }
}
