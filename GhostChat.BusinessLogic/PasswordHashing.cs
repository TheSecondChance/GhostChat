using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace GhostChat.BusinessLogic
{
    public static class PasswordHashing
    {
        public static string GenerateSalt()
        {
            var salt = new byte[16];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }
            return ByteArrayToHex(salt);
        }

        public static string PasswordHash(string password)
        {
            HashAlgorithm algorithm = new SHA256Managed();
            string salt = GenerateSalt();
            byte[] passwordBytes = Encoding.ASCII.GetBytes(password);
            byte[] saltBytes = HexToByteArray(salt);       
            byte[] passwordSaltBytes = passwordBytes.Concat(saltBytes).ToArray();

            byte[] passwordHash = algorithm.ComputeHash(passwordSaltBytes);
            string hash = salt + ByteArrayToHex(passwordHash);
            return hash;
        }

        public static bool PasswordVerify(string password, string passwordHash)
        {
            string salt = passwordHash.Substring(0, 32);
            string hash = passwordHash.Substring(32, passwordHash.Length - 32);
            HashAlgorithm algorithm = new SHA256Managed();

            byte[] passwordBytes = Encoding.ASCII.GetBytes(password);
            byte[] saltBytes = HexToByteArray(salt);
            byte[] passwordSaltBytes = passwordBytes.Concat(saltBytes).ToArray();

            string hashToVerify = ByteArrayToHex(algorithm.ComputeHash(passwordSaltBytes));

            if (hashToVerify == hash)
                return true;
            else
                return false;
        }

        public static byte[] HexToByteArray(string str)
        {
            byte[] bytes = new byte[str.Length / 2];
            for (int i = 0; i < str.Length; i += 2)
                bytes[i / 2] = Convert.ToByte(str.Substring(i, 2), 16);
            return bytes;
        }

        public static string ByteArrayToHex(byte[] bytes)
        {
            string hex = "";
            foreach (byte b in bytes)
                hex += b.ToString("x2");
            return hex;
        }
    }
}
