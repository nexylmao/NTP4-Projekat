using System;
using System.Runtime.Serialization;

namespace SkolskiSistemCommon
{
    [DataContract]
    public class Skola
    {
        private int id;
        private string naziv;
        private string adresa;
        private string telefon;
        private string email;

        [DataMember]
        public int Id
        {
            get => id;
            set => id = value;
        }
        [DataMember]
        public string Naziv
        {
            get => naziv;
            set => naziv = value;
        }
        [DataMember]
        public string Adresa
        {
            get => adresa;
            set => adresa = value;
        }
        [DataMember]
        public string Telefon
        {
            get => telefon;
            set => telefon = value;
        }
        [DataMember]
        public string Email
        {
            get => email;
            set => email = value;
        }

        public Skola()
        {
            id = int.MinValue;
        }

        public Skola(string naziv, string adresa, string telefon, string email)
        {
            id = Int32.MinValue;
            this.naziv = naziv;
            this.adresa = adresa;
            this.telefon = telefon;
            this.email = email;
        }

        public Skola(int id, string naziv, string adresa, string telefon, string email)
        {
            this.id = id;
            this.naziv = naziv;
            this.adresa = adresa;
            this.telefon = telefon;
            this.email = email;
        }
    }
}