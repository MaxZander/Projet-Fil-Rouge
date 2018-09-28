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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projet_Fil_Rouge
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        HashSet<Users> h = new HashSet<Users>();
        int a = 2;
        HashSet<string> roles = new HashSet<string>();
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        Main main;
        public Login()
        {
            InitializeComponent();
            pgbar.Value = 0;
            timer.Tick += new EventHandler(dispatcherTimer_Tick);
            timer.Interval = TimeSpan.FromMilliseconds(1);
        }

        private void log_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void Log_In()
        {
            MySqlCommand cmd = MySQL.GetConnexion().CreateCommand();
            cmd.CommandText = "SELECT D.ID , D.username, D.password, S.statut FROM data D, statut S WHERE D.username=" + '"' + ID.Text + '"' + " and D.password =" +
            '"' + PSW.Password + '"' + " and S.idstatut = D.idstatut";
            //SELECT D.ID , D.username, D.password, S.statut FROM 'data' D, 'statut' S WHERE D.username="" and D.password ="" and S.idstatut = U.idstatut
            //select 1 from data where exists (select * from data)
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                try
                {
                    if (reader.GetString(3) == "admin")
                    {
                        h.Add(new Users { Role = reader.GetString(3), ID = reader.GetInt32(0) });
                        timer.Start();
                    }
                    else if (reader.GetString(3) == "RH")
                    {
                        h.Add(new Users { Role = reader.GetString(3), ID = reader.GetInt32(0) });
                        timer.Start();
                    }
                        
                    else
                    {
                        h.Add(new Users { Role = reader.GetString(3), ID = reader.GetInt32(0) });
                        timer.Start();
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error ! \n" + e.Message);
                }

            }
            reader.Close();
        }

        private void FirstLaunch()
        {
            MySqlCommand cmd = MySQL.GetConnexion().CreateCommand();
            cmd.CommandText = "select * from data";
            //select 1 from data where exists (select * from data)
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows) { }
            else
            {
                a = 0;
            }

            
            reader.Close();
            
        }

        private void AddFirstUser ()
        {
            AddUsers add = new AddUsers(roles);
            add.ShowDialog();
            if (add.DialogResult.HasValue && add.DialogResult.Value)
                MessageBox.Show("User clicked OK");
            else
                MessageBox.Show("Vous n'avez pas enregistrer d'admin");
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (pgbar.Value == 100)
            {
                timer.Stop();
                main = new Main(this, h);
                main.Show();
                this.Hide();
            }
                
            else
                pgbar.Value += 5;

        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            Log_In();
        }

        private void ID_GotFocus(object sender, RoutedEventArgs e)
        {
            
        }

        private void pgbar_Initialized(object sender, EventArgs e)
        {
            FirstLaunch();
            if (a == 0)
            {
                roles.Add("admin");
                AddFirstUser();
            }
        }

        private void SnackbarMessage_ActionClick(object sender, RoutedEventArgs e)
        {
            SnackBar.IsActive = false;
        }
    }
}
