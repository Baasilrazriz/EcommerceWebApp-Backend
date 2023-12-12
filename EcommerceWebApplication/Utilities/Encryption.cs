using System.Security.Cryptography;
using System.Text;

namespace EcommerceWebApplication.Utilities
{
    public class Encryption
    {
        string hash = "";
        public string encryption(string x)
        {
            string encrypt;
            byte[] data = UTF8Encoding.UTF8.GetBytes(x);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));//Get hash key
                //Encrypt data by hash key
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    encrypt = Convert.ToBase64String(results, 0, results.Length);
                }
            }
            return encrypt;
        }
    }
}
