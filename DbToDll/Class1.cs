using System;

namespace DbToDll
{
    public class Client
    {
        public string ID { get; set; }
        public string PSW { get; set; }
        public string Nom { get;  set; }
        public string Prenom { get;  set; }
        public string Ecole { get;  set; }
        public string Mat { get;  set; }
        public string DD { get;  set; }
        public string DF { get;  set; }
        public int EC { get;  set; } //Ecole bool
        public int AG { get;  set; } //Agence interim bool
        public int DL { get;  set; } //droit lecture
        public int DE { get;  set; } //droit ecriture
        public int RH { get;  set; }
        public int ADMIN { get;  set; }
    }

    public class Users
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Ecole { get; set; }
        public string Mat { get; set; }
        public string DD { get; set; }
        public string DF { get; set; }
        public int EC { get; set; }
        public int AG { get; set; }
        public int DL { get; set; }
        public int DE { get; set; }
        public int RH { get; set; }
        public int ADMIN { get; set; }
    }
}
