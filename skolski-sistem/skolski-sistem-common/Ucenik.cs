using System;
using System.Runtime.Serialization;

namespace skolski_sistem_common
{
    [DataContract]
    public class Ucenik
    {
        public static class SqlQueries
        {
            public static string Create => @"IF NOT EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES
                WHERE TABLE_NAME = 'Ucenik')
                CREATE TABLE UCENIK(
                    id INT PRIMARY KEY NOT NULL IDENTITY(1,1),
                    ime NVARCHAR(30) NOT NULL,
                    prezime NVARCHAR(30) NOT NULL,
                    jmbg NVARCHAR(13) NOT NULL,
                    datumRodjenja DATE NOT NULL,
                    adresa NVARCHAR(255) NOT NULL,
                    mobilniTelefon NVARCHAR(15) NOT NULL,
                    idSmera INT FOREIGN KEY REFERENCES SMER(id),
                    idSkole INT FOREIGN KEY REFERENCES SKOLA(id)    
                );";
        }
        
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
        public int Id => id;
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
        public int IdSmera => idSmera;
        [DataMember]
        public int IdSkole => idSkole;

        public Ucenik()
        {
        }

        public Ucenik(string ime, string prezime, string jmbg, DateTime datumRodjenja, string adresa, string mobilniTelefon, int idSmera, int idSkole)
        {
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