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
            id = int.MinValue;
        }

        public Smer(string naziv)
        {
            id = int.MinValue;
            this.naziv = naziv;
        }

        public Smer(int id, string naziv)
        {
            this.id = id;
            this.naziv = naziv;
        }
    }
}