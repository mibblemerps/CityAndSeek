using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CityAndSeek.Server.Helpers
{
    public static class SecureToken
    {
        /// <summary>
        /// Generates a cryptographically secure token safe for authentication purposes.
        /// </summary>
        /// <param name="bytes">Number of random bytes to use</param>
        /// <returns>Cryptographically Secure Token</returns>
        public static string Generate(int bytes = 32)
        {
            using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
            {
                byte[] tokenData = new byte[bytes];
                rng.GetBytes(tokenData);

                return Convert.ToBase64String(tokenData);
            }
        }
    }
}
