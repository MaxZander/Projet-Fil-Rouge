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
using System.Windows.Shapes;

namespace Projet_Fil_Rouge
{
    /// <summary>
    /// Logique d'interaction pour AddUsers.xaml
    /// </summary>
    public partial class AddUsers : Window
    {
        HashSet<Client> h;
        public AddUsers(HashSet<Client> clients)
        {
            InitializeComponent();
            h = clients; 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            h.Add(new Client { ID = id.Text, PSW = psw.Text , Nom = lastname.Text, Prenom = name.Text, Ecole = school.Text, Mat = "1023", DD = "01/01/2000", DF = "01/03/2000", EC = 1, AG = 0, DL = 1, DE = 0, RH = 0, ADMIN = 1 });
        }
    }
}
