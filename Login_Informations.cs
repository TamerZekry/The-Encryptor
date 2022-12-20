using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace The_Encryptor
{
    class Login_Informations
    {
        private readonly string _password_text;
        private readonly string _hint_text;
        /// <summary>
        /// Create instance of Login Information
        /// </summary>
        /// <param name="password_text"></param>
        /// <param name="hint_text"></param>
        public Login_Informations(string password_text, string hint_text)
        {
            _password_text = password_text;
            _hint_text = hint_text;
        }

        public Login_Informations()
        {
        }

        private void PrintBytes(byte[] array)
        {
            Console.Write("<");
            foreach (byte item in array)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine(" ");
        }
        /// <summary>
        /// Convert integer 568 to a _byte array {5,6,8} to 
        /// </summary>
        /// <param name="_int"></param>
        /// <returns></returns>
        public byte[] IntToByteArray(int _int)
        {
            string IntInString = $"{_int:D3}";
            byte[] result = new byte[IntInString.Length];
            for (int i = 0; i < IntInString.Length; i++)
            {
                result[i] = byte.Parse(IntInString[i].ToString());
            }
            return result;
        }
        /// <summary>
        /// Convert _byte array {1,2,3} to integer 123
        /// </summary>
        /// <param name="_byte"></param>
        /// <returns></returns>
        private int ByteArrayToInt(byte[] _byte)
        {
            string res = string.Empty;
            foreach (var item in _byte)
            {
                res += item.ToString();
            }
            return int.Parse(res);
        }
        /*
         should return:
                  1) Hint byte array 
                  2) hashed Password  array 
                  3) Hint byte array length 
                  4) hashed Password  array length
         */
        /// <summary>
        /// Create hint byte [], HashedPassword[], hint byte[] length,HashedPassword[] length 
        /// </summary>
        /// <returns></returns>
        protected byte[] CreateBytes4Length()
        {
            SHA512 sHA1 = SHA512.Create();
            byte[] _hashed_password_in_bytes = sHA1.ComputeHash(Encoding.ASCII.GetBytes(_password_text));
            byte[] hint_in_bytes = Encoding.ASCII.GetBytes(_hint_text);
            byte[] hintlength = { byte.Parse(hint_in_bytes.Length.ToString()) };
            byte[] hashed_pass_length = { byte.Parse(_hashed_password_in_bytes.Length.ToString()) };
            byte[] AllBytes = hint_in_bytes.Concat(_hashed_password_in_bytes).Concat(hintlength).Concat(hashed_pass_length).ToArray();
            return AllBytes;
        }
        internal byte[] ReadByteFromtheFile(Carrier _carrier)
        {
            byte[] byteinfile = new byte[3];
            byte[] mydata;
            using (FileStream fileStream = new FileStream(_carrier.FileInfo.FullName, FileMode.Open, FileAccess.Read))
            {
                fileStream.Seek(-3, SeekOrigin.End);
                fileStream.Read(byteinfile, 0, 3);
                fileStream.Seek(-ByteArrayToInt(byteinfile) - 3, SeekOrigin.End);
                mydata = new byte[ByteArrayToInt(byteinfile)];
                fileStream.Read(mydata, 0, ByteArrayToInt(byteinfile));
            }
            byte[] temp_byte_array;
            KeysandIvs KnI = new KeysandIvs();

            temp_byte_array = KnI.Dec(mydata);
            return temp_byte_array;

        }
        internal int lengthOfAddedBytes(Carrier _carrier)
        {
            byte[] byteinfile = new byte[3];
            byte[] mydata;
            using (FileStream fileStream = new FileStream(_carrier.FileInfo.FullName, FileMode.Open, FileAccess.Read))
            {
                fileStream.Seek(-3, SeekOrigin.End);
                fileStream.Read(byteinfile, 0, 3);
                fileStream.Seek(-ByteArrayToInt(byteinfile) - 3, SeekOrigin.End);
                mydata = new byte[ByteArrayToInt(byteinfile)];
                fileStream.Read(mydata, 0, ByteArrayToInt(byteinfile));
            }

            return mydata.Length;

        }






    }
}

