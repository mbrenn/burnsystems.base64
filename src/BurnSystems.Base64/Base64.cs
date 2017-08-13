using System.Collections.Generic;

namespace BurnSystems.Base64
{
    public class Base64
    {
        protected readonly char[] Alphabet;
        protected readonly char PaddingSymbol;

        private Dictionary<char, byte> _reverseAlphabet;

        public Base64() : this(Alphabets.Base64, '=')
        {

        }
        public Base64(char[] alphabet) : this(alphabet, '=')
        {

        }
        public Base64(char[] alphabet, char paddingSymbol)
        {
            Alphabet = alphabet;
            PaddingSymbol = paddingSymbol;
        }

        /// <summary>
        /// Gets the reversed alphabet
        /// </summary>
        public Dictionary<char, byte> ReverseAlphabet
        {
            get
            {
                if (_reverseAlphabet == null)
                {
                    _reverseAlphabet = new Dictionary<char, byte>();
                    if (Alphabet.Length > 0xFF)
                    {
                        throw new EncodingException("The given alphabet is too long (> 255 characters)");
                    }

                    for (byte n = 0; n < Alphabet.Length; n++)
                    {
                        _reverseAlphabet[Alphabet[n]] = n;
                    }
                }

                return _reverseAlphabet;
            }
        }

        public byte GetByteForCharacter(char character)
        {
            if (ReverseAlphabet.TryGetValue(character, out byte result))
            {
                return result;
            }

            throw new EncodingException($"Character {character} is unknown in alphabet");
        }
    }
}