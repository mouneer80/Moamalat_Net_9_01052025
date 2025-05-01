using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
//using BulkSMS;
namespace Moamalat.Entities
{

    public class Utilities
    {
        public static string? publicKey { get; set; }
        public static string? SaltStr { get; set; }

        protected static byte[] _salt = new byte[16];
        protected static byte[] _Key = new byte[32];

        public void Create()
        {

        }
        public Utilities(string _KeyValue, string _SaltValue)
        {
            publicKey = _KeyValue;
            SaltStr = _SaltValue;

        }
        public static string Encrypt__(string value)
        {
            string EncryptionKey = publicKey;
            byte[] clearBytes = Encoding.UTF8.GetBytes(value);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.Mode = CipherMode.CBC;
                encryptor.IV = pdb.GetBytes(16);
                encryptor.Padding = PaddingMode.PKCS7;
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    value = Convert.ToBase64String(ms.ToArray());
                }
            }


            return value;

        }
        public static byte[] EncryptToBytes(string value)
        {
            // new byte[];

            string EncryptionKey = publicKey;
            byte[] clearBytes = Encoding.UTF8.GetBytes(value);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.Mode = CipherMode.CBC;
                encryptor.IV = pdb.GetBytes(16);
                encryptor.Padding = PaddingMode.PKCS7;
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    return ms.ToArray();
                    //value = Convert.ToBase64String(ms.ToArray());
                }
            }


            return null;

        }
        public static string DecryptFromBytes(byte[] cipherBytes)
        {

            string EncryptionKey = publicKey;

            //byte[] cipherBytes = Convert.FromBase64String(value);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.Mode = CipherMode.CBC;
                encryptor.IV = pdb.GetBytes(16);
                encryptor.Padding = PaddingMode.PKCS7;
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            // return "Bad Data";

        }
        public static string Decrypt__(string value)
        {

            string EncryptionKey = publicKey;

            byte[] cipherBytes = Convert.FromBase64String(value);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.Mode = CipherMode.CBC;
                encryptor.IV = pdb.GetBytes(16);
                encryptor.Padding = PaddingMode.PKCS7;
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    value = Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            return value;

        }

        public static string Encrypt(string plainText)
        {
            byte[] encryptedText;
            using (Aes aes = Aes.Create())
            {
                _Key = Encoding.UTF8.GetBytes(publicKey);
                _salt = Encoding.UTF8.GetBytes(SaltStr);
                // Derive the actual key using the salt and a password-based key derivation function
                Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(_Key, _salt, iterations: 10000);
                aes.Key = deriveBytes.GetBytes(aes.KeySize / 8);
                aes.IV = deriveBytes.GetBytes(aes.BlockSize / 8);

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        encryptedText = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(encryptedText);
        }

        public static string Decrypt(string encryptedText)
        {
            string decryptedText;
            try
            {
                encryptedText = encryptedText.Replace(" ", "+");
                byte[] encryptedBytes = Convert.FromBase64String(encryptedText);

                using (Aes aes = Aes.Create())
                {

                    _Key = Encoding.UTF8.GetBytes(publicKey);
                    _salt = Encoding.UTF8.GetBytes(SaltStr);
                    // Derive the actual key using the salt and a password-based key derivation function
                    Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(_Key, _salt, iterations: 10000);
                    aes.Key = deriveBytes.GetBytes(aes.KeySize / 8);
                    aes.IV = deriveBytes.GetBytes(aes.BlockSize / 8);

                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new MemoryStream(encryptedBytes))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader streamReader = new StreamReader(cryptoStream))
                            {
                                decryptedText = streamReader.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return decryptedText;
        }
        public static bool IsBase64String(string base64String)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(base64String);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }


        public static string Generate4Num()
        {
            double OTP = 0;
            Random random = new Random();
            OTP = (double)random.Next(1111, 9999);

            return OTP.ToString();
        }

        public static bool HasSpecialChar(string Value)
        {
            bool result = false;

            if (Value.Contains("'")
                || Value.Contains("--")
                || Value.Contains("%")
                || Value.Contains("/*")
                || Value.Contains("*/")
                || Value.Contains("...")
                || Value.Contains("+")
                || Value.Contains("=")
                || Value.Contains("+")
                || Value.Contains(">")
                || Value.Contains("<")
                || Value.Contains(">")
                || Value.Contains("or")
                || Value.Contains("!")
                || Value.Contains("#")
                || Value.Contains(";")
                || Value.Contains("|")
                )
                result = true;

            return result;
        }

        public static string EncodeEncryptedString(string Value)
        {
            string encrypted = Encrypt(Value);
            string encoded = Uri.EscapeDataString(encrypted);
            return encoded;
        }
        public static string DecodeEncryptedString(string Value)
        {

            string decrypted = Decrypt(Uri.UnescapeDataString(Value));

            return decrypted;
        }
        public static string EncodeEncryptedSmallString(string Value)
        {

            string encrypted = Encrypt(StringToGuid(Value).ToString());
            string encoded = Uri.EscapeDataString(encrypted);
            return encoded;
        }

        public static string DecodeEncryptedSmallString(string Value)
        {

            string decrypted = Decrypt(Uri.UnescapeDataString(Value));
            string decoded = GuidToString(new Guid(decrypted));
            return decoded;
        }
        protected static Guid StringToGuid(string _value)
        {
            byte[] _bytes = new byte[16];
            Encoding.UTF8.GetBytes(_value).CopyTo(_bytes, 0);
            try
            {
                Guid guid = new Guid(_bytes);
                return guid;
            }
            catch (Exception)
            {
                throw;
            }

        }
        protected static string GuidToString(Guid value)
        {

            byte[] b = value.ToByteArray();
            string _Result = Encoding.UTF8.GetString(b);//  BitConverter.ToString(b, 0);
            return _Result;

        }

    }
}
