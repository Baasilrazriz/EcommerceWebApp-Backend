using System.Security.Cryptography;
using System.Text;

namespace EcommerceWebApplication.Utilities
{
    public class Decryption
    {
        public string decryption(string y)
        {
           
            string decrypt;
            //Convert a string to byte array
            byte[] data = Convert.FromBase64String(y);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(y));//Get hash key
                //Decrypt data by hash key
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    decrypt = UTF8Encoding.UTF8.GetString(results);
                }
            }
            return decrypt;
        }
    }
}
