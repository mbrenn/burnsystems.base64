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

        public string DecodeToText(string original)
        {
            return DecodeToText(original, Encoding.UTF8);
        }

        private string DecodeToText(string original, Encoding encoding)
        {
            return encoding.GetString(Decode(original));
        }

        public byte[] Decode(string original)
        {
            var bytes = new List<byte>();
            if (original.Length % 4 != 0)
            {
                throw new EncodingException("Length is not dividable by 4");
            }
            var i = 0;
            while (i < original.Length)
            {
                Decode(bytes, original, i);

                i += 4;
            }

            return bytes.ToArray();
        }

        private void Decode(List<byte> bytes, string original, int i)
        {
            var char1 = original[i];
            var char2 = original[i+1];
            var char3 = original[i+2];
            var char4 = original[i+3];

            var value1 = GetByteForCharacter(char1);
            var value2 = GetByteForCharacter(char2);

            var c1 = (byte) ((value1 << 2) | ((value2 & 0x30) >> 4));
            bytes.Add(c1);


            if (char3 != PaddingSymbol)
            {
                var value3 = GetByteForCharacter(char3);
                var c2 = (byte) (((value2 & 0x0F) << 4) | (value3 & 0x3C) >> 2);
                bytes.Add(c2);


                if (char4 != PaddingSymbol)
                {
                    var value4 = GetByteForCharacter(char4);
                    var c3 = (byte) (((value3 & 0x03) << 6) | value4);
                    bytes.Add(c3);
                }
                else
                {
                    if ((value3 & 0x03) != 0x00)
                    {
                        throw new EncodingException("No zeroes in padding are allowed");
                    }
                }
            }
            else
            {
                if ((value2 & 0x0F) != 0x00)
                {
                    throw new EncodingException("No zeroes in padding are allowed");
                }
            }

            if (char3 == PaddingSymbol && char4 != PaddingSymbol)
            {
                throw new EncodingException("Error in padding... ??=? is not supported.");
            }
        }
    }
}