using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace UtilityTools.Encryption
{
    /// <summary>
    /// DES加密(UTF8)
    /// </summary>
    public static class DesAssist
    {
        /// <summary>  
        /// 加密
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Encrypt(string plainText, string key)
        {
            return Encrypt(plainText, Encoding.UTF8.GetBytes(key));
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Encrypt(string plainText, byte[] key)
        {
            var buffer = Encoding.UTF8.GetBytes(plainText);
            var cipher = Encrypt(buffer, key);
            return Convert.ToBase64String(cipher);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] buffer, byte[] key)
        {
            using (var crypto = CreateSymmetricAlgorithm(key))
            {
                using (var ms = new MemoryStream())
                {
                    var binaryWriter = new BinaryWriter(ms);
                    binaryWriter.Write((byte)1);
                    binaryWriter.Write(crypto.IV);
                    binaryWriter.Flush();

                    var cryptoStream = new CryptoStream(ms, crypto.CreateEncryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(buffer, 0, buffer.Length);
                    cryptoStream.FlushFinalBlock();

                    return ms.ToArray();
                }
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Decrypt(string cipherText, string key)
        {
            return Decrypt(cipherText, Encoding.UTF8.GetBytes(key));
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Decrypt(string cipherText, byte[] key)
        {
            var cipher = Convert.FromBase64String(cipherText);
            var plainText = Decrypt(cipher, key);
            return Encoding.UTF8.GetString(plainText);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] buffer, byte[] key)
        {
            using (var crypto = CreateSymmetricAlgorithm(key))
            {
                using (var ms = new MemoryStream(buffer))
                {
                    var binaryReader = new BinaryReader(ms);
                    // ReSharper disable once UnusedVariable
                    int algorithmVersion = binaryReader.ReadByte();
                    crypto.IV = binaryReader.ReadBytes(crypto.IV.Length);

                    var decryptedBuffer = new byte[buffer.Length];
                    int actualDecryptedLength;

                    using (var cryptoStream = new CryptoStream(ms, crypto.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        actualDecryptedLength = cryptoStream.Read(decryptedBuffer, 0, decryptedBuffer.Length);
                    }

                    var finalDecryptedBuffer = new byte[actualDecryptedLength];
                    Array.Copy(decryptedBuffer, finalDecryptedBuffer, actualDecryptedLength);
                    return finalDecryptedBuffer;
                }
            }
        }

        /// <summary>
        /// 创建SymmetricAlgorithm
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static SymmetricAlgorithm CreateSymmetricAlgorithm(byte[] key)
        {
            SymmetricAlgorithm? result = null;

            try
            {
                result = SymmetricAlgorithm.Create("7A5F3R9A");

                if (result == null)
                {
                    throw new Exception("SymmetricAlgorithm create instance is null");
                }

                result.Mode = CipherMode.CBC;
                result.Key = key;
                result.IV = new byte[]
                {
                    101, 22, 34, 47, 54, 67, 75, 89, 99, 104, 118, 124, 133, 146, 158, 167
                };

                return result;
            }
            catch
            {
                if (result == null)
                {
                    throw;
                }

                IDisposable disposableResult = result;
                disposableResult.Dispose();

                throw;
            }
        }
    }
}