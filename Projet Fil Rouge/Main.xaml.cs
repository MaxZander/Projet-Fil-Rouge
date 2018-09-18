using DbToDll;
using MaterialDesignThemes.Wpf;
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
        int c;
        Login _login;
        Collabo collabo;
        private HashSet<Users> users = new HashSet<Users>();
        HashSet<Client> clients;
        string[] nom;
        string[] prenom;
        bool x = true;

        public Main(Login login, HashSet<Client> hashset)
        {
            InitializeComponent();
            _login = login;
            c = hashset.Count;
            clients = hashset;
            nom = new string[c];
            prenom = new string[c];
            foreach (var item in hashset)
            {
                users.Add(new Users { Nom = item.Nom, Prenom = item.Prenom , ADMIN = item.ADMIN, AG = item.AG, DD = item.DD, DE = item.DE, DF = item.DF, DL = item.DL, EC = item.EC, Ecole = item.Ecole, Mat = item.Mat, RH = item.RH });
            }
            //mydatagridview.ItemsSource = users;
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
            collabo = new Collabo(users);
            collabo.Show();
        }

        private void Add_Users_Click(object sender, RoutedEventArgs e)
        {
            AddUsers addUsers = new AddUsers(clients);
            addUsers.Show();
        }
    }
}
