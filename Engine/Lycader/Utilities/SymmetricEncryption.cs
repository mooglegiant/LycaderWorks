namespace KTree.Encryption
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public static class SymmetricEncryption
    {
        #region Settings

        private static int iterations = 2;
        private static int keySize = 256;

        private static string hash = "SHA1";
        private static string salt = "8fNYym79cBJOUPKw"; // Random
        private static string vector = "uLpIKbVvYuaudG60"; // Random
        private static string passPhrase = "kPllpXK9RvoclYKm";

        #endregion

        public static string Encrypt(string value)
        {
            return Encrypt<AesManaged>(value);
        }

        public static string Encrypt<T>(string value)
                where T : SymmetricAlgorithm, new()
        {
            byte[] vectorBytes = ASCIIEncoding.ASCII.GetBytes(vector);
            byte[] saltBytes = ASCIIEncoding.ASCII.GetBytes(salt);
            byte[] valueBytes = UTF8Encoding.UTF8.GetBytes(value);

            byte[] encrypted;
            using (T cipher = new T())
            {
                PasswordDeriveBytes passwordBytes = new PasswordDeriveBytes(passPhrase, saltBytes, hash, iterations);
                byte[] keyBytes = passwordBytes.GetBytes(keySize / 8);

                cipher.Mode = CipherMode.CBC;

                using (ICryptoTransform encryptor = cipher.CreateEncryptor(keyBytes, vectorBytes))
                {
                    using (MemoryStream to = new MemoryStream())
                    {
                        using (CryptoStream writer = new CryptoStream(to, encryptor, CryptoStreamMode.Write))
                        {
                            writer.Write(valueBytes, 0, valueBytes.Length);
                            writer.FlushFinalBlock();
                            encrypted = to.ToArray();
                        }
                    }
                }
                cipher.Clear();
            }
            return Convert.ToBase64String(encrypted);
        }

        public static string Decrypt(string value)
        {
            return Decrypt<AesManaged>(value);
        }

        public static string Decrypt<T>(string value) where T : SymmetricAlgorithm, new()
        {
            byte[] vectorBytes = ASCIIEncoding.ASCII.GetBytes(vector);
            byte[] saltBytes = ASCIIEncoding.ASCII.GetBytes(salt);
            byte[] valueBytes = Convert.FromBase64String(value);

            byte[] decrypted;
            int decryptedByteCount = 0;

            using (T cipher = new T())
            {
                PasswordDeriveBytes passwordBytes = new PasswordDeriveBytes(passPhrase, saltBytes, hash, iterations);
                byte[] keyBytes = passwordBytes.GetBytes(keySize / 8);

                cipher.Mode = CipherMode.CBC;

                try
                {
                    using (ICryptoTransform decryptor = cipher.CreateDecryptor(keyBytes, vectorBytes))
                    {
                        using (MemoryStream from = new MemoryStream(valueBytes))
                        {
                            using (CryptoStream reader = new CryptoStream(from, decryptor, CryptoStreamMode.Read))
                            {
                                decrypted = new byte[valueBytes.Length];
                                decryptedByteCount = reader.Read(decrypted, 0, decrypted.Length);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return String.Empty;
                }

                cipher.Clear();
            }
            return Encoding.UTF8.GetString(decrypted, 0, decryptedByteCount);
        }
    }
}
