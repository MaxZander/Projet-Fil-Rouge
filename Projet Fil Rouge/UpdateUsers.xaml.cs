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
    /// Logique d'interaction pour UpdateUsers.xaml
    /// </summary>
    public partial class UpdateUsers : Window
    {
        HashSet<string> roles;
        public UpdateUsers(HashSet<string> vs, int id)
        {
            InitializeComponent();
            roles = vs;
            cbrole.ItemsSource = vs;
            CurrentUser(id);
            //select * from data D where D.id = 1;
        }
        private string Datechanger(string DateEnEU)
        {
            string[] dateok = DateEnEU.Split('/');
            var date = "";
            return date = dateok[2] + "-" + dateok[1] + "-" + dateok[0];
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }
        private void Update()
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
            if (cbrole.SelectedItem.ToString() == "admin")
            {
                idrole = 0;
            }
            else if (cbrole.SelectedItem.ToString() == "RH")
            {
                idrole = 1;
            }
            else
            {
                idrole = 2;
            }
            MySqlCommand cmd = MySQL.GetConnexion().CreateCommand();
            cmd.CommandText = "UPDATE data SET username =?Usr, password=?Psw,name=?Name,lastname=?LastName,school=?School,dd=?DD,df=?DF,bschool=?EC,bagence=?AG,idstatut=?IdStatut);";
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
        private void CurrentUser(int ID_IN)
        {
            MySqlCommand cmd = MySQL.GetConnexion().CreateCommand();
            cmd.CommandText = "select D.id, D.username, D.password, D.name, D.lastname, D.school, D.dd, D.df, D.bschool, D.bagence, S.statut from data D, statut S where D.idstatut = S.idstatut and D.id =" + ID_IN;
            //select D.id, D.username, D.password, D.name, D.lastname, D.school, D.dd, D.df, D.bschool, D.bagence, S.statut from data D, statut S where D.idstatut = S.idstatut and D.id = 1;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                try
                {

                    id.Text = reader.GetString(1).ToString();
                    name.Text = reader.GetString(3);
                    lastname.Text = reader.GetString(4);

                    if(reader.GetInt32(8) == 1)
                    {
                        school.Text = reader.GetString(5);
                        school_bool.IsChecked = true;
                    }
                    //cbrole.SelectedItem = reader.GetString(10);
                    DD.Text = reader.GetString(6);
                    DF.Text = reader.GetString(7);
                    if (reader.GetInt32(9) == 1)
                    {
                        agency_bool.IsChecked = true;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error ! \n" + e.Message);
                }

            }
            reader.Close();
        }
    }
}
