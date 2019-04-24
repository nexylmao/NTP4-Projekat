using System.Runtime.Serialization;

namespace SkolskiSistemCommon
{
    [DataContract]
    public class Smer
    {
        private int id;
        private string naziv;

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

        public Smer()
        {
        }

        public Smer(string naziv)
        {
            this.naziv = naziv;
        }

        public Smer(int id, string naziv)
        {
            this.id = id;
            this.naziv = naziv;
        }
    }
}