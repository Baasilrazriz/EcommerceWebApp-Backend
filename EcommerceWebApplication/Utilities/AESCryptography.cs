using System.Security.Cryptography;
using System.IO;
using System;

namespace EcommerceWebApplication.Utilities
{
    public class AESCryptography
    {
        // ... [Other members like SavedKey, SavedIV, InitializeKeyAndIV]
        public static byte[] SavedKey { get; private set; }
        public static byte[] SavedIV { get; private set; }
        public static void InitializeKeyAndIV()
        {
            // Check if key and IV already exist in persistent storage (e.g., file system)
            // If they don't exist, generate new key and IV
            if (!File.Exists("aes_key.bin") || !File.Exists("aes_iv.bin"))
            {
                using (var aesAlg = Aes.Create())
                {
                    SavedKey = aesAlg.Key;
                    SavedIV = aesAlg.IV;

                    // Optionally save the key and IV to persistent storage for future use
                    File.WriteAllBytes("aes_key.bin", SavedKey);
                    File.WriteAllBytes("aes_iv.bin", SavedIV);
                }
            }
            else
            {
                // Load the key and IV from persistent storage
                SavedKey = File.ReadAllBytes("aes_key.bin");
                SavedIV = File.ReadAllBytes("aes_iv.bin");
            }
        }

        public static byte[] EncryptString(string plainText, byte[] key, byte[] iv)
        {
            // Check if the plainText, key, or iv is null
            if (plainText == null || key == null || iv == null)
                throw new ArgumentNullException("plainText, key, and iv must not be null.");

            byte[] encrypted;

            // Create a new AesManaged.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }
        public static string DecryptString(byte[] cipherText, byte[] key, byte[] iv)
        {
            // Check if the cipherText, key, or iv is null
            if (cipherText == null || key == null || iv == null)
                throw new ArgumentNullException("cipherText, key, and iv must not be null.");

            string plaintext = null;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
    }
}
