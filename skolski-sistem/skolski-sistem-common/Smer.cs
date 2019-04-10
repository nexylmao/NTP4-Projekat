using System.Runtime.Serialization;

namespace skolski_sistem_common
{
    [DataContract]
    public class Smer
    {
        private int id;
        private string naziv;

        [DataMember]
        public int Id => id;
        [DataMember]
        public string Naziv
        {
            get => naziv;
            set => naziv = value;
        }

        public Smer()
        {
        }

        public Smer(int id, string naziv)
        {
            this.id = id;
            this.naziv = naziv;
        }
    }
}