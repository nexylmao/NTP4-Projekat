using System;
using System.Runtime.Serialization;

namespace SkolskiSistemCommon
{
    [DataContract]
    public class Ucenik
    {
        private int id;
        private string ime;
        private string prezime;
        private string jmbg;
        private DateTime datumRodjenja;
        private string adresa;
        private string mobilniTelefon;
        private int idSmera;
        private int idSkole;

        [DataMember]
        public int Id
        {
            get => id;
            set => id = value;
        }
        [DataMember]
        public string Ime
        {
            get => ime;
            set => ime = value;
        }
        [DataMember]
        public string Prezime
        {
            get => prezime;
            set => prezime = value;
        }
        [DataMember]
        public string Jmbg
        {
            get => jmbg;
            set => jmbg = value;
        }
        [DataMember]
        public DateTime DatumRodjenja
        {
            get => datumRodjenja;
            set => datumRodjenja = value;
        }
        [DataMember]
        public string Adresa
        {
            get => adresa;
            set => adresa = value;
        }
        [DataMember]
        public string MobilniTelefon
        {
            get => mobilniTelefon;
            set => mobilniTelefon = value;
        }
        [DataMember]
        public int IdSkole
        {
            get => idSkole;
            set => idSkole = value;
        }
        [DataMember]
        public int IdSmera
        {
            get => idSmera;
            set => idSmera = value;
        }

        public Ucenik()
        {
            id = int.MinValue;
        }

        public Ucenik(string ime, string prezime, string jmbg, DateTime datumRodjenja, string adresa, string mobilniTelefon, int idSmera, int idSkole)
        {
            id = int.MinValue;
            this.ime = ime;
            this.prezime = prezime;
            this.jmbg = jmbg;
            this.datumRodjenja = datumRodjenja;
            this.adresa = adresa;
            this.mobilniTelefon = mobilniTelefon;
            this.idSmera = idSmera;
            this.idSkole = idSkole;
        }

        public Ucenik(int id, string ime, string prezime, string jmbg, DateTime datumRodjenja, string adresa, string mobilniTelefon, int idSmera, int idSkole)
        {
            this.id = id;
            this.ime = ime;
            this.prezime = prezime;
            this.jmbg = jmbg;
            this.datumRodjenja = datumRodjenja;
            this.adresa = adresa;
            this.mobilniTelefon = mobilniTelefon;
            this.idSmera = idSmera;
            this.idSkole = idSkole;
        }
    }
}