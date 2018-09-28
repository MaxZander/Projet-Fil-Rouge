using DbToDll;
using MaterialDesignThemes.Wpf;
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
    /// Logique d'interaction pour Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        Login _login;
        Collabo collabo;
        private List<Users> users = new List<Users>();
        bool x = true;
        string role;
        HashSet<string> right = new HashSet<string>();
        int matricule;


        public Main(Login login, HashSet<Users> hashset)
        {
            InitializeComponent();
            _login = login;
            foreach (var item in hashset)
            {
                role = item.Role;
                matricule = item.ID;
            }
            if(role == "admin")
            {
                right.Add("admin");
                right.Add("RH");
                right.Add("user");
            }else if (role == "RH")
            {
                right.Add("user");
            }
            else
            {
                right.Add("nada");
            }
        }
        private void UpdateData()
        {
            MySqlCommand cmd = MySQL.GetConnexion().CreateCommand();
            cmd.CommandText = "Select D.id, D.name, D.lastname, D.school, D.dd, D.df, S.statut  from data D, statut S where D.idstatut = S.idstatut";
            //SELECT U.ID , U.username, U.password, S.statut FROM user U, statut S WHERE U.username="" and U.password ="" and S.idstatut = U.idstatut
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                try
                {
                    users.Add(new Users
                    {
                        ID = reader.GetInt32(0),
                        Nom = reader.GetString(2),
                        Prenom = reader.GetString(1),
                        Ecole = reader.GetString(3),
                        Role = reader.GetString(6),
                        DD = reader.GetString(4),
                        DF = reader.GetString(5)
                    });
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error ! \n" + e.Message);
                }

            }
            reader.Close();
        }

        private void Main1_Closed(object sender, EventArgs e)
        {
            if (x)
            {
                _login.Close();
            }
            else
            {
                _login.Show();
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            x = true;
        }

        private void Log_out_Click(object sender, RoutedEventArgs e)
        {
            x = false;
            this.Close();
        }

        private void List_Collab_Click(object sender, RoutedEventArgs e)
        {
            users.Clear();
            UpdateData();
            collabo = new Collabo(role, right);
            collabo.DataGrid.ItemsSource = users;
            collabo.Show();
        }

        private void Add_Users_Click(object sender, RoutedEventArgs e)
        {
            if(right.First() != "nada")
            {
                AddUsers addUsers = new AddUsers(right);
                addUsers.ShowDialog();
            }
        }
    }
}
