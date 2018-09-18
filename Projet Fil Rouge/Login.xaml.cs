using DbToDll;
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
        HashSet<Client> h = new HashSet<Client>();
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        Main main;

        bool open = false;
        string name;
        string lname;
        string school;
        string mat;
        int rh;
        int admin;
        public Login()
        {
            InitializeComponent();
            h.Add(new Client { ID = "root", PSW = "toor" , Nom = "DURAND", Prenom = "Martin", Ecole = "ECOLE ALSACIENNE", Mat = "1023", DD = "01/01/2000", DF = "01/03/2000", EC = 1, AG = 0, DL = 1, DE = 0, RH = 0, ADMIN = 1 });
            h.Add(new Client { ID = "Lucien", PSW = "abc123", Nom = "MARTIN", Prenom = "Paul", Ecole = "AGENCE ADECCO", Mat = "0023", DD = "01/01/1990", DF = "01/03/2020", EC = 0, AG = 1, DL = 1, DE = 1, RH = 1, ADMIN = 0 });
            pgbar.Value = 0;
            timer.Tick += new EventHandler(dispatcherTimer_Tick);
            timer.Interval = TimeSpan.FromMilliseconds(1);
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
            //timer.Start();
            foreach (var item in h)
            {
                if (item.ID == ID.Text)
                {
                    if (item.PSW == PSW.Password)
                    {
                        name = item.Prenom;
                        lname = item.Nom;
                        school = item.Ecole;
                        mat = item.Mat;
                        rh = item.RH;
                        admin = item.ADMIN;
                        timer.Start();
                        open = true;
                    }

                }
                if (!open)
                {
                    SnackbarOne.IsActive = true;
                }
            }
        }

        private void ID_GotFocus(object sender, RoutedEventArgs e)
        {
            SnackbarOne.IsActive = false;
        }
    }
}
