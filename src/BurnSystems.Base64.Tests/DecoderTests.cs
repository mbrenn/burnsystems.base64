using System.Diagnostics;
using Xunit;

namespace BurnSystems.Base64.Tests
{
    public class DecoderTests
    {
        [Fact]
        public void TestEmpty()
        {
            Assert.Equal(string.Empty, new Base64Decoder().DecodeToText(""));
            Assert.True(new Base64Decoder().Decode("").Length == 0);
        }


        [Fact]
        public void TestRFC4648()
        {
            Assert.Equal("", new Base64Decoder().DecodeToText(""));
            Assert.Equal("f", new Base64Decoder().DecodeToText("Zg=="));
            Assert.Equal("fo", new Base64Decoder().DecodeToText("Zm8="));
            Assert.Equal("foo", new Base64Decoder().DecodeToText("Zm9v"));
            Assert.Equal("foob", new Base64Decoder().DecodeToText("Zm9vYg=="));
            Assert.Equal("fooba", new Base64Decoder().DecodeToText("Zm9vYmE="));
            Assert.Equal("foobar", new Base64Decoder().DecodeToText("Zm9vYmFy"));
        }

        [Fact]
        public void Test_Chapter_10_Ignore_InvalidCharacters()
        {
            Assert.Throws<EncodingException>(() => new Base64Decoder().Decode("\0"));
            Assert.Throws<EncodingException>(() => new Base64Decoder().Decode("!"));
            Assert.Throws<EncodingException>(() => new Base64Decoder().Decode("§"));
            Assert.Throws<EncodingException>(() => new Base64Decoder().Decode("Z"));
            Assert.Throws<EncodingException>(() => new Base64Decoder().Decode("§m9v"));
            Assert.Throws<EncodingException>(() => new Base64Decoder().Decode("Z§9v"));
            Assert.Throws<EncodingException>(() => new Base64Decoder().Decode("Zm§v"));
            Assert.Throws<EncodingException>(() => new Base64Decoder().Decode("Zm9§"));
            Assert.Throws<EncodingException>(() => new Base64Decoder().Decode("TG==="));
            Assert.Throws<EncodingException>(() => new Base64Decoder().Decode("TG==="));
            Assert.Throws<EncodingException>(() => new Base64Decoder().Decode("Zm9v\r\nZm9v"));
        }

        [Fact]
        public void Test_Chapter_5_FilenameSafeAlphabet()
        {
            Assert.True(new Base64Decoder().Decode("+A==")[0] == 62);
            Assert.True(new Base64Decoder().Decode("/A==")[0] == 63);
            Assert.True(new Base64Decoder(Alphabets.Base64Url).Decode("-A==")[0] == 62);
            Assert.True(new Base64Decoder(Alphabets.Base64Url).Decode("_A==")[0] == 63);


            Assert.Throws<EncodingException>(() => new Base64Decoder(Alphabets.Base64Url).Decode("+A=="));
            Assert.Throws<EncodingException>(() => new Base64Decoder(Alphabets.Base64Url).Decode("/A=="));
            Assert.Throws<EncodingException>(() => new Base64Decoder().Decode("-A=="));
            Assert.Throws<EncodingException>(() => new Base64Decoder().Decode("_A=="));
        }
    }
}