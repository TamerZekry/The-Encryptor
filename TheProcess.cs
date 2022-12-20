using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace The_Encryptor
{

    class TheProcess : Login_Informations
    {
        private Aes  aes;
        private byte[] _key = new byte[32];
        private byte[] _iv = new byte[16];
        public TheProcess(string password_text, string hint_text) : base(password_text, hint_text)
        {
            aes = Aes.Create();
            aes.GenerateKey();
            aes.GenerateIV();
            _key = aes.Key;
            _iv = aes.IV;

            aes.Padding = PaddingMode.PKCS7;
        }
        public TheProcess()
        {

        }
        private byte[] TotalBytestoBeWriten()
        {
            byte[] temp_byte_array;
            KeysandIvs KnI = new();
            temp_byte_array = _key.Concat(_iv).Concat(CreateBytes4Length()).ToArray();
            temp_byte_array = KnI.Enc(temp_byte_array);
            return temp_byte_array.Concat(IntToByteArray(temp_byte_array.Length)).ToArray();
        }
        public byte[][] Get_HashedPassword_and_hint(byte[] _array)
        {
            int hashed_PasswordLength = _array[^1];
            int hintlength = _array[^2];
            byte[][] result =
            {
                new byte [hashed_PasswordLength],
                new byte [hintlength]
            };
            Array.Copy(_array, 48 + hintlength, result[0], 0, hashed_PasswordLength);
            Array.Copy(_array, 48, result[1], 0, hintlength);

            return result;


        }
        public async Task StartEnc(Carrier _carrier)
        {


            byte[] data = new byte[_carrier.FileInfo.Length];
            using FileStream infs = new(_carrier.FileInfo.FullName, FileMode.Open, FileAccess.Read);
            infs.Read(data, 0, (int)_carrier.FileInfo.Length);
            using (FileStream outFs = new(_carrier.FileInfo.FullName + ".enc", FileMode.Create))
            {
                using (CryptoStream outStreamEncrypted = new(outFs, aes.CreateEncryptor(_key, _iv), CryptoStreamMode.Write))
                {
                    await outStreamEncrypted.WriteAsync(data, 0, data.Length);

                    outStreamEncrypted.FlushFinalBlock();




                    outFs.Write(TotalBytestoBeWriten(), 0, TotalBytestoBeWriten().Length);

                    outFs.Flush();
                }

            }







            //int Blocksize = aes.BlockSize / 8;
            //byte[] data = new byte[Blocksize];
            //using (FileStream outFs = new FileStream(_carrier.FileInfo.FullName + ".enc", FileMode.Create))
            //{
            //    using (CryptoStream outStreamEncrypted = new CryptoStream(outFs, aes.CreateEncryptor(_key, _iv), CryptoStreamMode.Write))
            //    {
            //        int count = 0;
            //        int offset = 0;
            //        int bytesRead = 0;
            //        using (FileStream inFs = new FileStream(_carrier.FileInfo.FullName, FileMode.Open))
            //        {
            //            do
            //            {
            //                count = inFs.Read(data, 0, Blocksize);
            //                offset += count;
            //               outStreamEncrypted.Write(data, 0, count);
            //                bytesRead += Blocksize;



            //            }
            //            while (count > 0);
            //           await  inFs.DisposeAsync();
            //        }
            //        outStreamEncrypted.FlushFinalBlock();
            //          outFs.Write(TotalBytestoBeWriten(), 0, TotalBytestoBeWriten().Length);
            //    }
            //       outFs.Dispose();
            //}
            //// _carrier.FileInfo.Delete();

        }
        public async Task StartDec(Carrier _carrier)
        {
            aes = new AesCryptoServiceProvider();
            aes.Padding = PaddingMode.PKCS7;
            byte[] _array = ReadByteFromtheFile(_carrier);
            Array.Copy(_array, 0, _key, 0, 32);
            Array.Copy(_array, 32, _iv, 0, 16);
            int leng = lengthOfAddedBytes(_carrier);
            byte[] Enc_file_bytes = new byte[_carrier.FileInfo.Length - leng - 3];
            using FileStream Enc_file = new(_carrier.FileInfo.FullName, FileMode.Open, FileAccess.Read);
            Enc_file.Read(Enc_file_bytes, 0, (int)_carrier.FileInfo.Length - leng - 3);
            using FileStream outFs = new(_carrier.RemoveEnc().FullName, FileMode.Create);
            using (CryptoStream outStreamEncrypted = new(outFs, aes.CreateDecryptor(_key, _iv), CryptoStreamMode.Write))
            {
                await outStreamEncrypted.WriteAsync(Enc_file_bytes, 0, Enc_file_bytes.Length);
            }

        }
    }
}


