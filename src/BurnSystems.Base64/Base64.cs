namespace BurnSystems.Base64
{
    public class Base64
    {
        protected readonly char[] _alphabet;
        protected readonly char _paddingSymbol;

        public Base64() : this(Alphabets.Base64, '=')
        {

        }
        public Base64(char[] alphabet) : this(alphabet, '=')
        {

        }
        public Base64(char[] alphabet, char paddingSymbol)
        {
            _alphabet = alphabet;
            _paddingSymbol = paddingSymbol;
        }
    }
}