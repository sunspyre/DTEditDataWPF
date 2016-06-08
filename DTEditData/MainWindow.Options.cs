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
        private Visibility CheckVisibility(bool checkBoxValue)
        {
            if (checkBoxValue)
                return Visibility.Visible;
            else
                return Visibility.Hidden;
        }
        private void CheckColumns(object sender, RoutedEventArgs e)
        {
            gridMain.Columns[gridMain.Columns.Single(c => c.Header.ToString().Equals(DATECOLUMN)).DisplayIndex].Visibility = CheckVisibility(chkShowDate.IsChecked.Value);
            gridMain.Columns[gridMain.Columns.Single(c => c.Header.ToString().Equals(SPECIALCOLUMN)).DisplayIndex].Visibility = CheckVisibility(chkShowSpecial.IsChecked.Value);
            gridMain.Columns[gridMain.Columns.Single(c => c.Header.ToString().Equals(BUTTONCOLUMN)).DisplayIndex].Visibility = CheckVisibility(chkShowButtonId.IsChecked.Value);

        }
    }
}
