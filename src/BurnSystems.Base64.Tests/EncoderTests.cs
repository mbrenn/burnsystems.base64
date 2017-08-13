using System.Diagnostics;
using Xunit;

namespace BurnSystems.Base64.Tests
{
    public class EncoderTests
    {
        [Fact]
        public void TestEmpty()
        {
            Assert.Equal(string.Empty, new Base64Encoder().Encode(""));
        }


        [Fact]
        public void TestRFC4648()
        {
            Assert.Equal("", new Base64Encoder().Encode(""));
            Assert.Equal("Zg==", new Base64Encoder().Encode("f"));
            Assert.Equal("Zm8=", new Base64Encoder().Encode("fo"));
            Assert.Equal("Zm9v", new Base64Encoder().Encode("foo"));
            Assert.Equal("Zm9vYg==", new Base64Encoder().Encode("foob"));
            Assert.Equal("Zm9vYmE=", new Base64Encoder().Encode("fooba"));
            Assert.Equal("Zm9vYmFy", new Base64Encoder().Encode("foobar"));
        }


        [Fact]
        public void TestBytes()
        {
            Assert.Equal("", new Base64Encoder().Encode(new byte[] {}));
            Assert.Equal("QQ==", new Base64Encoder().Encode(new byte[] {65}));
        }

        [Fact]
        public void Test_Chapter_3_1_NoLineAddings()
        {
            var longText = "This is a very long text that might receive a line feed.";
            longText = longText + longText;
            longText = longText + longText;
            longText = longText + longText;

            var result = new Base64Encoder().Encode(longText);
            Assert.False(result.Contains("\r"));
            Assert.False(result.Contains("\n"));
        }

        [Fact]
        public void Test_Chapter_5_FilenameSafeAlphabet()
        {
            Assert.Equal("+A==", new Base64Encoder().Encode(new byte[] { 248 }));
            Assert.Equal("/A==", new Base64Encoder().Encode(new byte[] { 252 }));
            Assert.Equal("-A==", new Base64Encoder(Alphabets.Base64Url).Encode(new byte[] { 248 }));
            Assert.Equal("_A==", new Base64Encoder(Alphabets.Base64Url).Encode(new byte[] { 252 }));
        }
    }
}