using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BurnSystems.Base64
{
    public class Base64Decoder : Base64
    {
        public Base64Decoder()
        {
        }

        public Base64Decoder(char[] alphabet) : base(alphabet)
        {
        }

        public Base64Decoder(char[] alphabet, char paddingSymbol) : base(alphabet, paddingSymbol)
        {
        }

        public byte[] Decode(string original)
        {
            return new byte[] { };
        }

        public string DecodeToText(string original)
        {
            return DecodeToText(original, Encoding.UTF8);
        }

        private string DecodeToText(string original, Encoding encoding)
        {
            return encoding.GetString(Decode(original));
        }
    }
}