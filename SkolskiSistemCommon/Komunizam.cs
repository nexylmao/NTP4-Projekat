using System.Runtime.Serialization;

namespace SkolskiSistemCommon
{
    [DataContract]
    public class Komunizam
    {
        private string propaganda;

        [DataMember]
        public string Propaganda
        {
            get => propaganda;
            set => propaganda = value;
        }

        public Komunizam(string propaganda)
        {
            this.propaganda = propaganda;
        }

        public string HraniMePropagandom()
        {
            return string.Format("Totalno ne-opresivni sistem kaze : {0}. Ijoj, program ti ide u gulag.", propaganda);
        }
    }
}