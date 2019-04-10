using System.Runtime.Serialization;

namespace skolski_sistem_common
{
    [DataContract]
    public class Zmaj
    {
        private string _catchPhrase;

        [DataMember]
        public string CatchPhrase
        {
            get => _catchPhrase;
            set => _catchPhrase = value;
        }

        public string GetCatchPhrase()
        {
            return $"Zmaj kaze: {_catchPhrase} i bljune te vatrom. Ugasi ti se program.";
        }

        public Zmaj(string catchPhrase)
        {
            _catchPhrase = catchPhrase;
        }
    }
}