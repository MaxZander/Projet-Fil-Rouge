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
    /// Logique d'interaction pour AddUsers.xaml
    /// </summary>
    public partial class AddUsers : Window
    {

        public AddUsers(HashSet<string> vs)
        {
            InitializeComponent();
            cbrole.ItemsSource = vs;
        }
        private string Datechanger(string DateEnEU)
        {
            string[] dateok = DateEnEU.Split('/');
            var date = "";
            return date = dateok[2] + "-"+ dateok[1] + "-" + dateok[0];
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Add();
            DialogResult = true;
                /*Mat = 0,
                Nom = lastname.Text,
                Prenom = name.Text,
                Ecole = school.Text,
                Role = role.SelectedValue.ToString(),
                DD = datechanger(DD.Text),
                DF = datechanger(DF.Text),
                EC = 1,
                AG = 0*/
                // RESTE A FAIRE :
                // Ajouter une requete SQL INSERT pour ajouter le nouvel utilisateur
            
        }

        private void school_bool_Checked(object sender, RoutedEventArgs e)
        {
            agency_bool.IsChecked = false;
            school.IsEnabled = true;
            agency.IsEnabled = false;
        }

        private void school_bool_Unchecked(object sender, RoutedEventArgs e)
        {
            agency_bool.IsChecked = true;
            agency.IsEnabled = true;
            school.IsEnabled = false;
        }

        private void agency_bool_Unchecked(object sender, RoutedEventArgs e)
        {
            agency.IsEnabled = false;
            school.IsEnabled = true;
            school_bool.IsChecked = true;
        }

        private void agency_bool_Checked(object sender, RoutedEventArgs e)
        {
            agency.IsEnabled = true;
            school.IsEnabled = false;
            school_bool.IsChecked = false;
        }

        private void Add()
        {
            var dd = Datechanger(DD.Text);
            var df = Datechanger(DF.Text);
            int ec = 0;
            int ag = 0;
            int idrole = 0;
            if (school_bool.IsChecked == true)
                ec = 1;
            if (agency_bool.IsChecked == true)
                ag = 1;
            if(cbrole.SelectedItem.ToString() == "admin")
            {
                idrole = 0;
            }else if(cbrole.SelectedItem.ToString() == "RH")
            {
                idrole = 1;
            }
            else
            {
                idrole = 2;
            }
            MySqlCommand cmd = MySQL.GetConnexion().CreateCommand();
            cmd.CommandText = "INSERT INTO data VALUES(0,?Usr, ?Psw,?Name,?LastName,?School,?DD,?DF, ?EC, ?AG, ?IdStatut);";
            //"INSERT INTO data VALUES(0, ?Usr, ?Psw,?Name,?LastName,?School,?DD,?DF, 0, 0, 0);"
            cmd.Parameters.Add("?Usr", MySqlDbType.VarChar).Value = id.Text;
            cmd.Parameters.Add("?Psw", MySqlDbType.VarChar).Value = psw.Password;
            cmd.Parameters.Add("?Name", MySqlDbType.VarChar).Value = name.Text;
            cmd.Parameters.Add("?LastName", MySqlDbType.VarChar).Value = lastname.Text;
            cmd.Parameters.Add("?School", MySqlDbType.VarChar).Value = school.Text;

            cmd.Parameters.Add("?DD", MySqlDbType.Date).Value = dd;
            cmd.Parameters.Add("?DF", MySqlDbType.Date).Value = df;
            cmd.Parameters.Add("?EC", MySqlDbType.Int32).Value = ec;
            cmd.Parameters.Add("?AG", MySqlDbType.Int32).Value = ag;
            cmd.Parameters.Add("?IdStatut", MySqlDbType.Int32).Value = idrole;
            cmd.ExecuteNonQuery();

        }
    }
}
