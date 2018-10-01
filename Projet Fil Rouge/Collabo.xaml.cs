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
        Main main;
        HashSet<string> vs = new HashSet<string>();
        public Collabo(string role, HashSet<string> vs, Main main)
        {
            InitializeComponent();
            roles = role;
            this.main = main;
            this.vs = vs;
            DataGrid.ItemsSource = this.main.UpdateList(this);
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
                update.ShowDialog();
                if (update.DialogResult.HasValue && update.DialogResult.Value)
                {//OK
                    DataGrid.ItemsSource = "";
                    DataGrid.ItemsSource = main.UpdateList(this);
                }
                else
                {//Cancel
                    Snackbar.Message.Content = "Les modifs ne sont pas enregistrer.";
                    Snackbar.IsActive = true;
                }
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
            try
            {
                ID = (DataGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                id = Int32.Parse(ID);
            }
            catch
            {

            }

        }

        private void SnackbarMessage_ActionClick(object sender, RoutedEventArgs e)
        {
            Snackbar.IsActive = false;
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            //
            main.DeleteUser("delete from data where id ="+ id);
        }
    }
}
