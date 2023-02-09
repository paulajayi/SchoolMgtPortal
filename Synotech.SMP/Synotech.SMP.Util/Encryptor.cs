using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Synotech.SMP.Util
{
    public class Encryptor
    {
        private static string GenerateHashString(HashAlgorithm algo, string text)
        {
            // Compute hash from text parameter
            algo.ComputeHash(Encoding.UTF8.GetBytes(text));

            // Get has value in array of bytes
            var result = algo.Hash;

            // Return as hexadecimal string
            return string.Join(
                string.Empty,
                result.Select(x => x.ToString("x2")));
        }

        public static String GenerateHMACSHA256(string text, string key)
        {
            // change according to your needs, an UTF8Encoding
            // could be more suitable in certain situations
            ASCIIEncoding encoding = new ASCIIEncoding();

            Byte[] textBytes = encoding.GetBytes(text);
            Byte[] keyBytes = encoding.GetBytes(key);

            Byte[] hashBytes;

            using (HMACSHA256 hash = new HMACSHA256(keyBytes))
                hashBytes = hash.ComputeHash(textBytes);

            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }

        public static String GenerateHMACSHA512(string text, string key)
        {
            // change according to your needs, an UTF8Encoding
            // could be more suitable in certain situations
            ASCIIEncoding encoding = new ASCIIEncoding();

            Byte[] textBytes = encoding.GetBytes(text);
            Byte[] keyBytes = encoding.GetBytes(key);

            Byte[] hashBytes;

            using (HMACSHA512 hash = new HMACSHA512(keyBytes))
                hashBytes = hash.ComputeHash(textBytes);

            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }

        public static string Md5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(Encoding.ASCII.GetBytes(text));

            //get hash result after compute it
            var result = md5.Hash;

            var strBuilder = new StringBuilder();
            foreach (var t in result)
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(t.ToString("x2"));

            return strBuilder.ToString();
        }

        public static string SHA1(string text)
        {
            var result = default(string);

            using (var algo = new SHA1Managed())
            {
                result = GenerateHashString(algo, text);
            }

            return result;
        }

        public static string SHA256(string text)
        {
            var result = default(string);

            using (var algo = new SHA256Managed())
            {
                result = GenerateHashString(algo, text);
            }

            return result;
        }

        public static string SHA384(string text)
        {
            var result = default(string);

            using (var algo = new SHA384Managed())
            {
                result = GenerateHashString(algo, text);
            }

            return result;
        }

        public static string SHA512(string text)
        {
            var result = default(string);

            using (var algo = new SHA512Managed())
            {
                result = GenerateHashString(algo, text);
            }

            return result;
        }

        private static RSAParameters LoadRsaPublicKey(byte[] pubkey)
        {
            RSAParameters RSAKeyInfo = new RSAParameters();

            byte[] SeqOID = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
            byte[] seq = new byte[15];

            // ---------  Set up stream to read the asn.1 encoded SubjectPublicKeyInfo blob  ------
            System.IO.MemoryStream mem = new System.IO.MemoryStream(pubkey);
            System.IO.BinaryReader binr = new System.IO.BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading
            byte bt = 0;
            ushort twobytes = 0;

            try
            {
                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte();    //advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16();   //advance 2 bytes
                else
                    return RSAKeyInfo;

                seq = binr.ReadBytes(15);       //read the Sequence OID
                if (!CompareBytearrays(seq, SeqOID))    //make sure Sequence for OID is correct
                    return RSAKeyInfo;

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8103) //data read as little endian order (actual data order for Bit String is 03 81)
                    binr.ReadByte();    //advance 1 byte
                else if (twobytes == 0x8203)
                    binr.ReadInt16();   //advance 2 bytes
                else
                    return RSAKeyInfo;

                bt = binr.ReadByte();
                if (bt != 0x00)     //expect null byte next
                    return RSAKeyInfo;

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte();    //advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16();   //advance 2 bytes
                else
                    return RSAKeyInfo;

                twobytes = binr.ReadUInt16();
                byte lowbyte = 0x00;
                byte highbyte = 0x00;

                if (twobytes == 0x8102) //data read as little endian order (actual data order for Integer is 02 81)
                    lowbyte = binr.ReadByte();  // read next bytes which is bytes in modulus
                else if (twobytes == 0x8202)
                {
                    highbyte = binr.ReadByte(); //advance 2 bytes
                    lowbyte = binr.ReadByte();
                }
                else
                    return RSAKeyInfo;
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };   //reverse byte order since asn.1 key uses big endian order
                int modsize = BitConverter.ToInt32(modint, 0);

                byte firstbyte = binr.ReadByte();
                binr.BaseStream.Seek(-1, System.IO.SeekOrigin.Current);

                if (firstbyte == 0x00)
                {   //if first byte (highest order) of modulus is zero, don't include it
                    binr.ReadByte();    //skip this null byte
                    modsize -= 1;   //reduce modulus buffer size by 1
                }

                byte[] modulus = binr.ReadBytes(modsize);   //read the modulus bytes

                if (binr.ReadByte() != 0x02)            //expect an Integer for the exponent data
                    return RSAKeyInfo;
                int expbytes = (int)binr.ReadByte();        // should only need one byte for actual exponent data (for all useful values)
                byte[] exponent = binr.ReadBytes(expbytes);

                RSAKeyInfo.Modulus = modulus;
                RSAKeyInfo.Exponent = exponent;

                return RSAKeyInfo;
            }
            catch (System.Exception)
            {
                return RSAKeyInfo;
            }
            finally { binr.Close(); }
        }

        private static bool CompareBytearrays(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;
            int i = 0;
            foreach (byte c in a)
            {
                if (c != b[i])
                    return false;
                i++;
            }
            return true;
        }


        /// <summary>
        /// Use public key encryption, segment encryption
        /// </summary>
        ///<param name="publicKey"> Public key (string) </param>
        /// <param name="strEncryptString">To encryt the data</param>
        /// <returns></returns>
        public static string RSAEncrytByPublic(string publicKey, string strEncryptString)
        {
            var keyBytes = Convert.FromBase64String(publicKey);
            RSAParameters rsaParam = LoadRsaPublicKey(keyBytes);

            RSACryptoServiceProvider publicRsa = new RSACryptoServiceProvider();
            publicRsa.ImportParameters(rsaParam);
            byte[] originalData = Encoding.UTF8.GetBytes(strEncryptString);
            if (originalData == null || originalData.Length <= 0)
            {
                throw new NotSupportedException();
            }
            if (publicRsa == null)
            {
                throw new ArgumentNullException();
            }
            byte[] encryContent = null;
            #region Segment encryption
            int bufferSize = (publicRsa.KeySize / 8) - 11;
            byte[] buffer = new byte[bufferSize];
            //Segment encryption
            using (MemoryStream input = new MemoryStream(originalData))
            using (MemoryStream ouput = new MemoryStream())
            {
                while (true)
                {
                    int readLine = input.Read(buffer, 0, bufferSize);
                    if (readLine <= 0)
                    {
                        break;
                    }
                    byte[] temp = new byte[readLine];
                    Array.Copy(buffer, 0, temp, 0, readLine);
                    byte[] encrypt = publicRsa.Encrypt(temp, false);
                    ouput.Write(encrypt, 0, encrypt.Length);
                }
                encryContent = ouput.ToArray();
            }
            #endregion
            return Convert.ToBase64String(encryContent);
        }

        ///<summary>
        ///Use the public key to RSA decrypt the data 
        ///</summary>
        ///<param name="publicKey"> Public key (string) </param>
        ///<param name="strDecryptString"> To decrypt the data</param>
        ///<returns> Decrypted data</returns>
        public static string RSAPublicKeyDecrypt(string publicKey, string strDecryptString)
        {
            var keyBytes = Convert.FromBase64String(publicKey);
            RSAParameters rsaParam = LoadRsaPublicKey(keyBytes);

            //Load the public key
            RSACryptoServiceProvider publicRsa = new RSACryptoServiceProvider();
            publicRsa.ImportParameters(rsaParam);
            RSAParameters rp = publicRsa.ExportParameters(false);

            //Conversion key
            AsymmetricKeyParameter pbk = DotNetUtilities.GetRsaPublicKey(rp);

            IBufferedCipher c = CipherUtilities.GetCipher("RSA/ECB/PKCS1Padding");
            //The first parameter is true for encryption, false for decryption; the second parameter is the key
            c.Init(false, pbk);
            byte[] outBytes = null;
            byte[] dataToDecrypt = Convert.FromBase64String(strDecryptString);
            #region Segment decryption
            int keySize = publicRsa.KeySize / 8;
            byte[] buffer = new byte[keySize];

            using (MemoryStream input = new MemoryStream(dataToDecrypt))
            using (MemoryStream output = new MemoryStream())
            {
                while (true)
                {
                    int readLine = input.Read(buffer, 0, keySize);
                    if (readLine <= 0)
                    {
                        break;
                    }
                    byte[] temp = new byte[readLine];
                    Array.Copy(buffer, 0, temp, 0, readLine);
                    byte[] decrypt = c.DoFinal(temp);
                    output.Write(decrypt, 0, decrypt.Length);
                }
                outBytes = output.ToArray();
            }
            #endregion

            string strDec = Encoding.UTF8.GetString(outBytes);
            return strDec;
        }

        ///<summary>
        ///Use private key to encrypt data with RSA
        ///</summary>
        ///<param name="xmlPrivateKey"> Private key (XML format string)</param>
        ///<param name="strEncryptString">the data to be encrypted</param>
        ///<returns> Encrypted data</returns>
        public static string RSAPrivateKeyEncrypt(string xmlPrivateKey, string strEncryptString)
        {
            //Load private key
            RSACryptoServiceProvider privateRsa = new RSACryptoServiceProvider();
            privateRsa.FromXmlString(xmlPrivateKey);

            //Conversion key
            AsymmetricCipherKeyPair keyPair = DotNetUtilities.GetKeyPair(privateRsa);
            IBufferedCipher c = CipherUtilities.GetCipher("RSA/ECB/PKCS1Padding");//Use RSA/ECB/PKCS1Padding format
                                                                                  //The first parameter is true for encryption, false for decryption; the second parameter is the key

            c.Init(true, keyPair.Private);
            byte[] dataToEncrypt = Encoding.UTF8.GetBytes(strEncryptString);
            #region Segment encryption
            int bufferSize = (privateRsa.KeySize / 8) - 11;
            byte[] buffer = new byte[bufferSize];
            byte[] outBytes = null;
            //Segment encryption
            using (MemoryStream input = new MemoryStream(dataToEncrypt))
            using (MemoryStream ouput = new MemoryStream())
            {
                while (true)
                {
                    int readLine = input.Read(buffer, 0, bufferSize);
                    if (readLine <= 0)
                    {
                        break;
                    }
                    byte[] temp = new byte[readLine];
                    Array.Copy(buffer, 0, temp, 0, readLine);
                    byte[] encrypt = c.DoFinal(temp);
                    ouput.Write(encrypt, 0, encrypt.Length);
                }
                outBytes = ouput.ToArray();
            }
            #endregion           
            string strBase64 = Convert.ToBase64String(outBytes);

            return strBase64;
        }

        ///<summary>
        ///Decrypt by private key, decrypt in segments
        ///</summary>
        ///<param name="xmlPrivateKey"> Private key (XML format string)</param>
        ///<param name="strDecryptString">the data to be decrypted</param>
        ///<returns></returns>
        public static string RSADecryptByPrivate(string xmlPrivateKey, string strDecryptString)
        {
            RSACryptoServiceProvider privateRsa = new RSACryptoServiceProvider();
            privateRsa.FromXmlString(xmlPrivateKey);
            byte[] encryptData = Convert.FromBase64String(strDecryptString);

            byte[] dencryContent = null;
            #region Segment decryption
            if (encryptData == null || encryptData.Length <= 0)
            {
                throw new NotSupportedException();
            }

            int keySize = privateRsa.KeySize / 8;
            byte[] buffer = new byte[keySize];

            using (MemoryStream input = new MemoryStream(encryptData))
            using (MemoryStream output = new MemoryStream())
            {
                while (true)
                {
                    int readLine = input.Read(buffer, 0, keySize);
                    if (readLine <= 0)
                    {
                        break;
                    }
                    byte[] temp = new byte[readLine];
                    Array.Copy(buffer, 0, temp, 0, readLine);
                    byte[] decrypt = privateRsa.Decrypt(temp, false);
                    output.Write(decrypt, 0, decrypt.Length);
                }
                dencryContent = output.ToArray();
            }
            #endregion
            return Encoding.UTF8.GetString(dencryContent);
        }

        public static string DecryptRSA(string data, string privateKey)
        {
            UnicodeEncoding encoder = new UnicodeEncoding();
            var rsa = new RSACryptoServiceProvider();
            var dataArray = data.Split(new char[] { ',' });
            byte[] dataByte = new byte[dataArray.Length];
            for (int i = 0; i < dataArray.Length; i++)
            {
                dataByte[i] = Convert.ToByte(dataArray[i]);
            }

            rsa.FromXmlString(privateKey);
            var decryptedByte = rsa.Decrypt(dataByte, false);
            return encoder.GetString(decryptedByte);
        }

        public static string EncryptRSA(string data, string publicKey)
        {
            UnicodeEncoding encoder = new UnicodeEncoding();
            var rsa = new RSACryptoServiceProvider();

            rsa.FromXmlString(publicKey);
            var dataToEncrypt = encoder.GetBytes(data);
            var encryptedByteArray = rsa.Encrypt(dataToEncrypt, false).ToArray();
            var length = encryptedByteArray.Count();
            var item = 0;
            var sb = new StringBuilder();
            foreach (var x in encryptedByteArray)
            {
                item++;
                sb.Append(x);

                if (item < length)
                    sb.Append(",");
            }

            return sb.ToString();
        }

    }
}
