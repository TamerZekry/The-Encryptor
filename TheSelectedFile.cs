using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace The_Encryptor
{
    struct Login_informations
    {
        private byte[] _hint_In_Bytes;
       // private byte[] hint_in_bytes_length;
        private byte[] _hashedpassword_In_Bytes;
     //   private byte[] Hashedpassword_in_bytes_length;
        public string hint;
        public string password;

        public Login_informations(string _hint, string _password) : this()
        {
            hint = _hint;
            password = _password;
        }
        private void computePasswordHash()
        {
            SHA512 sHA1 = SHA512.Create();
            _hashedpassword_In_Bytes = sHA1.ComputeHash(Encoding.ASCII.GetBytes(password));
        }
        private void computePasswordHash_lenth()
        {
            byte[] Hashedpassword_in_bytes_length = { byte.Parse(_hashedpassword_In_Bytes.Length.ToString()) };
        }
        private void GetHintInBytes() => _hint_In_Bytes = Encoding.ASCII.GetBytes(hint);




        /// <summary>
        /// Convert integer 568 to a _byte array {5,6,8} to 
        /// </summary>
        /// <param name="_int"></param>
        /// <returns></returns>
        private byte[] IntegerToByteArray(int _int)
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
        private int ByteArrayToIntger(byte[] _byte)
        {
            string res = string.Empty;
            foreach (var item in _byte)
            {
                res += item.ToString();
            }
            return int.Parse(res);
        }






    }
    class TheSelectedFile
    {
       // Login_informations myStruct = new Login_informations();
        private FileInfo fileinfo { get; set; }
    //    private byte[] hint_in_bytes { get; set; }
        private byte[] Password_in_bytes { get; set; }

    //    KeysandIvs keysandIvs;

        public TheSelectedFile(FileInfo _fileinfo)
        {

            fileinfo = _fileinfo;

        }
    }
}
