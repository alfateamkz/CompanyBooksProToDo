using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CompanyBooksProECom.Utils
{
    public static class Aes
    {
        public static string Encrypt(string value, byte[] passwordBytes)
        {
            var originalValueBytes = Encoding.UTF8.GetBytes(value);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            var saltSize = GetSaltSize(passwordBytes);
            var saltBytes = GetRandomBytes(saltSize);
            var bytesToBeEncrypted = new byte[saltBytes.Length + originalValueBytes.Length];

            for (var i = 0; i < saltBytes.Length; i++)
            {
                bytesToBeEncrypted[i] = saltBytes[i];
            }

            for (var i = 0; i < originalValueBytes.Length; i++)
            {
                bytesToBeEncrypted[i + saltBytes.Length] = originalValueBytes[i];
            }

            var encryptedBytes = AES_Encrypt(bytesToBeEncrypted, passwordBytes);

            return Convert.ToBase64String(encryptedBytes);
        }

        public static string Decrypt(string value, byte[] passwordBytes)
        {
            var bytesToBeDecrypted = Convert.FromBase64String(value);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            var decryptedBytes = AES_Decrypt(bytesToBeDecrypted, passwordBytes);
            var saltSize = GetSaltSize(passwordBytes);
            var originalBytes = new byte[decryptedBytes.Length - saltSize];

            for (var i = saltSize; i < decryptedBytes.Length; i++)
            {
                originalBytes[i - saltSize] = decryptedBytes[i];
            }

            return Encoding.UTF8.GetString(originalBytes);
        }

        public static string Encrypt(string value, string password)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);

            return Encrypt(value, passwordBytes);
        }

        public static string Decrypt(string value, string password)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);

            return Decrypt(value, passwordBytes);
        }

        private static byte[] GetRandomBytes(int length)
        {
            var bytes = new byte[length];
            RandomNumberGenerator.Create().GetBytes(bytes);

            return bytes;
        }

        private static int GetSaltSize(byte[] passwordBytes)
        {
            var key = new Rfc2898DeriveBytes(passwordBytes, passwordBytes, 1000);
            var bytes = key.GetBytes(2);
            var sb = new StringBuilder();

            for (var i = 0; i < bytes.Length; i++)
            {
                sb.Append(Convert.ToInt32(i).ToString());
            }

            var stringBytes = sb.ToString();

            return stringBytes.Sum(charByte => Convert.ToInt32(charByte.ToString()));
        }

        private static byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            using var ms = new MemoryStream();
            using var aes = new RijndaelManaged
            {
                KeySize = 256, 
                BlockSize = 128
            };

            var key = new Rfc2898DeriveBytes(passwordBytes, passwordBytes, 1000);
            aes.Key = key.GetBytes(aes.KeySize / 8);
            aes.IV = key.GetBytes(aes.BlockSize / 8);

            aes.Mode = CipherMode.CBC;

            using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                cs.Close();
            }
            var encryptedBytes = ms.ToArray();
            return encryptedBytes;
        }

        private static byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            using var ms = new MemoryStream();
            using var aes = new RijndaelManaged
            {
                KeySize = 256, 
                BlockSize = 128
            };

            var key = new Rfc2898DeriveBytes(passwordBytes, passwordBytes, 1000);
            aes.Key = key.GetBytes(aes.KeySize / 8);
            aes.IV = key.GetBytes(aes.BlockSize / 8);

            aes.Mode = CipherMode.CBC;

            using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
            {
                cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                cs.Close();
            }
            var decryptedBytes = ms.ToArray();
            return decryptedBytes;
        }
    }
}
