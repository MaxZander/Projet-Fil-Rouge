using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace DbToDll
{
    public class MySQL
    {
        string result;
        private static MySqlConnection cnx = null;

        private static string _ConnectionString;

        public static MySqlConnection GetConnexion()
        {
            _ConnectionString = "Server=localhost; database=dbtest; password=; UID=root; sslmode=none;";

            if (cnx == null)
            {
                cnx = new MySqlConnection();
                cnx.ConnectionString = _ConnectionString;

                cnx.Open();
            }
            return cnx;
        }

        public static void CloseConnexion()
        {
            cnx.Close();
            cnx = null;
        }

        private string RequestReader()
        {
            MySqlCommand cmd = GetConnexion().CreateCommand();
            cmd.CommandText = "SELECT * FROM `marque` WHERE 1";
            //SELECT Mo.modele as 'MODELE FORD DISPONIBLE' FROM modele Mo, marque Ma WHERE Mo.IDMarque=Ma.IDMarque and Ma.Marque='Ford'
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                try
                {
                    result = reader.GetString(0);
                }
                catch (Exception e)
                {
                    result = e.ToString();
                    //MessageBox.Show("Erreur DB \n" + e.Message);
                }

            }
            
            reader.Close();
            return result;
        }
    }
}
