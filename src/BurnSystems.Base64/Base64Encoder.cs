using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BurnSystems.Base64
{
    public class Base64Encoder : Base64
    {
        private StringBuilder _builder;
        public Base64Encoder()
        {
        }

        public Base64Encoder(char[] alphabet) : base(alphabet)
        {
        }

        public Base64Encoder(char[] alphabet, char paddingSymbol) : base(alphabet, paddingSymbol)
        {
        }

        public string Encode(string original)
        {
            return Encode(original, Encoding.UTF8);
        }

        public string Encode(string original, Encoding encoding)
        {
            return Encode(encoding.GetBytes(original));
        }

        public string Encode(IEnumerable<byte> original)
        {
            return Encode(original.ToArray());
        }

        /// <summary>
        /// Encodes the given bytes to the string
        /// </summary>
        /// <param name="original">Bytes to be converted</param>
        /// <returns>Converted string</returns>
        public string Encode(byte[] original)
        {
            _builder = new StringBuilder();
            var i = 0;
            while ( i < original.Length)
            {
                var left = original.Length - i;
                if (left > 3)
                {
                    left = 3;
                }

                Encode(original, i, left);

                i += left;

            }

            return _builder.ToString();
        }

        private void Encode(byte[] original, int index, int length)
        {
            if (length == 1)
            {
                var value = original[index];
                var first = (value & 0xFC) >> 2;
                var second = (value & 0x03) << 4;
                _builder.Append(Alphabet[first]);
                _builder.Append(Alphabet[second]);
                _builder.Append(PaddingSymbol);
                _builder.Append(PaddingSymbol);

            }

            if (length == 2)
            {
                var value1 = original[index];
                var value2 = original[index + 1];

                var first = (value1 & 0xFC) >> 2;
                var second = (value1 & 0x03) << 4 | (value2 & 0xF0) >> 4;
                var third = (value2 & 0x0F) << 2;

                _builder.Append(Alphabet[first]);
                _builder.Append(Alphabet[second]);
                _builder.Append(Alphabet[third]);
                _builder.Append(PaddingSymbol);
            }

            if (length == 3)
            {
                var value1 = original[index];
                var value2 = original[index + 1];
                var value3 = original[index + 2];

                var first = (value1 & 0xFC) >> 2;
                var second = (value1 & 0x03) << 4 | (value2 & 0xF0) >> 4;
                var third = (value2 & 0x0F) << 2 | (value3 & 0xC0) >> 6;
                var fourth = (value3 & 0x3F);


                _builder.Append(Alphabet[first]);
                _builder.Append(Alphabet[second]);
                _builder.Append(Alphabet[third]);
                _builder.Append(Alphabet[fourth]);
            }
        }
    }
}
