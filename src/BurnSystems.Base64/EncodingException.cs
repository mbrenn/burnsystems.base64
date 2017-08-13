using System;

namespace BurnSystems.Base64
{
    /// <summary>
    /// This exception is thrown in case an error occured during the encoding
    /// </summary>
    public class EncodingException : Exception
    {
        public EncodingException()
        {
        }

        public EncodingException(string message) : base(message)
        {
        }

        public EncodingException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}