using System.Runtime.Serialization;

namespace skolski_sistem_common
{
    [DataContract]
    public class Smer
    {
        public static class SqlQueries
        {
            public static string Create => @"IF NOT EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES
                WHERE TABLE_NAME = 'Smer')
                CREATE TABLE SMER(
                    id INT PRIMARY KEY NOT NULL IDENTITY(1,1),
                    naziv NVARCHAR(255) NOT NULL
                );";
        }
        
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