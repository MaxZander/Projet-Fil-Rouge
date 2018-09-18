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
    /// Logique d'interaction pour Collabo.xaml
    /// </summary>
    public partial class Collabo : Window
    {
        private HashSet<Users> users;
        public Collabo(HashSet<Users> hashset)
        {
            InitializeComponent();
            users = hashset;
            mydatagridview.ItemsSource = users;
        }
    }
}
