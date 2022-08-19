using System.Security.Cryptography;
using System.Text;

namespace Bank.Common.Security
{
    internal class Encryptor
    {
        private static readonly string _publicKey = "Wr52.c#v";
        private static readonly string _secretKey = "Er5FKg*=";

        public static string Encrypt(string textToEncrypt)
        {
            if (textToEncrypt == null)
                throw new ArgumentNullException("The text to encrypt can´t be null");

            if (textToEncrypt == string.Empty)
                throw new ArgumentException("The text to encrypt can´t be empty");

            byte[] _secretKeyByte = Encoding.UTF8.GetBytes(_secretKey);
            byte[] _publicKeybyte = Encoding.UTF8.GetBytes(_publicKey);
            byte[] inputbyteArray = Encoding.UTF8.GetBytes(textToEncrypt);
            using var des = DES.Create();
            MemoryStream ms = new();
            CryptoStream cs = new(ms, des.CreateEncryptor(_publicKeybyte, _secretKeyByte), CryptoStreamMode.Write);
            cs.Write(inputbyteArray, 0, inputbyteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }
    }
}
