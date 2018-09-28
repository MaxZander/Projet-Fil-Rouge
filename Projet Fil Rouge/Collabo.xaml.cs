using DbToDll;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Projet_Fil_Rouge
{
    /// <summary>
    /// Logique d'interaction pour Collabo.xaml
    /// </summary>
    public partial class Collabo : Window
    {
        string roles;
        string ID;
        int id;
        HashSet<string> vs = new HashSet<string>();
        public Collabo(string role, HashSet<string> vs)
        {
            InitializeComponent();
            roles = role;
            this.vs = vs;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (roles == "user" || roles == "RH")
            {
                Snackbar.Message.Content = "Vous n'avez pas le permission de faire ceci";
                Snackbar.IsActive = true;
            }
            else
            {
                UpdateUsers update = new UpdateUsers(vs, id);
                update.Show();
            }
        }

        private void mydatagridview_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if(roles == "user" || roles == "RH")
            {
                e.Cancel = true;
            }
        }
        
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentID();
        }
        private void CurrentID()
        {
            object item = DataGrid.SelectedItem;
            ID = (DataGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            id = Int32.Parse(ID);
        }

        private void SnackbarMessage_ActionClick(object sender, RoutedEventArgs e)
        {
            Snackbar.IsActive = false;
        }
    }
}
