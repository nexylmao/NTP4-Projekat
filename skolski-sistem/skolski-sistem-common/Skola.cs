using System.Runtime.Serialization;

namespace skolski_sistem_common
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
        public int Id => id;
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
        }

        public Skola(string naziv, string adresa, string telefon, string email)
        {
            this.naziv = naziv;
            this.adresa = adresa;
            this.telefon = telefon;
            this.email = email;
        }
    }
}