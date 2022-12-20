using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace The_Encryptor
{
    public class KeyFileOperations
    {
        const string _file_Name = "key.bin";

        public static string GenrateFile(string _UserName, string _Password)
        {
            string UserName = _UserName;
            string PassWord = _Password;
            byte[] bytesofUserNameandPassword = Encoding.ASCII.GetBytes(UserName + PassWord);
            SHA512 sha = SHA512.Create();
            byte[] HasedbytesofUserNameandPassword = sha.ComputeHash(bytesofUserNameandPassword);
            return Convert.ToBase64String(HasedbytesofUserNameandPassword);
        }
        public static string GetKeyFileString() => File.ReadAllText("Key.bin");

    }
}
