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
            _ConnectionString = "Server=localhost; port=3308; database=fil_rouge; password=; UID=root; sslmode=none;";

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

        private string RequestReader(string CmdReader)
        {
            MySqlCommand cmd = GetConnexion().CreateCommand();
            cmd.CommandText = CmdReader;
            MySqlDataReader reader = cmd.ExecuteReader();
            string[] resultT = new string[50];
            int i = 0;
            while (reader.Read())
            {
                try
                {
                    resultT[i] = reader.GetString(i);
                    i++;
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
