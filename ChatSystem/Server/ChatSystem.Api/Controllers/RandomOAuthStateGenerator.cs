namespace ChatSystem.Api.Controllers
{
    using System;
    using System.Security.Cryptography;
    using System.Web;

    public static class RandomOAuthStateGenerator
    {
        private const int BitsPerByte = 8;
        private static RandomNumberGenerator random = new RNGCryptoServiceProvider();

        public static string Generate(int strengthInBits)
        {
            if (strengthInBits % BitsPerByte != 0)
            {
                throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
            }

            var strengthInBytes = strengthInBits / BitsPerByte;

            var data = new byte[strengthInBytes];
            random.GetBytes(data);
            return HttpServerUtility.UrlTokenEncode(data);
        }
    }
}
