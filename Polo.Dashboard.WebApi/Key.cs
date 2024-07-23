using System.Security.Cryptography;

namespace Polo.Dashboard.WebApi
{
    public class Key
    {
        public static string Secret { get; } = GenerateRandomKey();

        private static string GenerateRandomKey()
        {
            var keyBytes = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(keyBytes);
            }
            return Convert.ToBase64String(keyBytes);
        }
    }
}