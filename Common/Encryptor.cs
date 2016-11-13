using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Common
{
    public class Encryptor
    {
        public static string PublicKey { get; set; }
        public static string PrivateKey { get; set; }

        public static void GenerateRSAKeys()
        {
            RSA rsaProvider = new RSACryptoServiceProvider();
            PublicKey = Convert.ToBase64String(Encoding.Unicode.GetBytes(rsaProvider.ToXmlString(false)));
            PrivateKey = Convert.ToBase64String(Encoding.Unicode.GetBytes(rsaProvider.ToXmlString(true)));
        }
        public static byte[] EncryptAsymmetrycal(byte[] input, string publicRSA)
        {
            RSA rsaProvider = new RSACryptoServiceProvider();
            rsaProvider.FromXmlString(Encoding.Unicode.GetString(Convert.FromBase64String(publicRSA)));
            RSAEncryptionPadding padding = RSAEncryptionPadding.Pkcs1;
            byte[] output = rsaProvider.Encrypt(input, padding);
            rsaProvider.Clear();
            return output;
        }

        public static byte[] DecryptAsymmetrical(byte[] input, string privateRSA)
        {
            RSA rsaProvider = new RSACryptoServiceProvider();
            rsaProvider.FromXmlString(Encoding.Unicode.GetString(Convert.FromBase64String(privateRSA)));
            RSAEncryptionPadding padding = RSAEncryptionPadding.Pkcs1;
            byte[] output = rsaProvider.Decrypt(input, padding);
            rsaProvider.Clear();
            return output;
        }

        public static byte[] GenerateSymmetricalKey()
        {
            TripleDES cryptor = new TripleDESCryptoServiceProvider();
            cryptor.GenerateKey();
            return cryptor.Key;
        }

        public static string EncryptSymmetrical(string inputString, string symmKey)
        {
            TripleDES cryptor = new TripleDESCryptoServiceProvider();
            cryptor.Key = Convert.FromBase64String(symmKey);
            cryptor.Mode = CipherMode.ECB;
            cryptor.Padding = PaddingMode.PKCS7;

            ICryptoTransform transformer = cryptor.CreateEncryptor();
            byte[] input = Encoding.Unicode.GetBytes(inputString);
            byte[] output = transformer.TransformFinalBlock(input, 0, input.Length);

            string outputString = Convert.ToBase64String(output);
            return outputString;
        }

        public static string DecryptSymmetrical(string inputString, string symmKey)
        {
            TripleDES cryptor = new TripleDESCryptoServiceProvider();
            cryptor.Key = Convert.FromBase64String(symmKey);
            cryptor.Mode = CipherMode.ECB;
            cryptor.Padding = PaddingMode.PKCS7;

            byte[] input = Convert.FromBase64String(inputString);
            ICryptoTransform transformer = cryptor.CreateDecryptor();
            byte[] output = transformer.TransformFinalBlock(input, 0, input.Length);
            cryptor.Clear();
            string outputString = Encoding.Unicode.GetString(output);
            return outputString;
        }
    }
}

//byte[] initialKey = Encryptor.GenerateSymmetricalKey();
//byte[] keyEncrypted = Encryptor.EncryptAsymmetrycal(initialKey, ConfigurationManager.AppSettings["rsaPublic"]);
//byte[] keyDecrypted = Encryptor.DecryptAsymmetrical(keyEncrypted, ConfigurationManager.AppSettings["rsaPrivate"]);
//string stringKey = Convert.ToBase64String(keyDecrypted);
//string toSendObject = Encryptor.EncryptSymmetrical(tbQueue.Text, stringKey);
//byte[] serialized = Serializer.SerializeObject(toSendObject);
//string deserialized = (string)Serializer.DeserializeObject(serialized);
//string resultObject = Encryptor.DecryptSymmetrical(deserialized, stringKey);