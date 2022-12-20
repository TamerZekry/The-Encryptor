using System.IO;

namespace The_Encryptor
{
    /// <summary>
    ///  Class for validating entered data 
    /// </summary>
    public class Validator
    {
        public FileInfo Validat_File(string FileName)
        {
            FileInfo fileInfo = new(FileName);
            if (fileInfo.Exists)
            {
                FileStream stream = new(fileInfo.FullName, FileMode.Open, FileAccess.ReadWrite);
                stream.Dispose();
                return fileInfo;
            }
            else
            {
                fileInfo = null;
                return fileInfo;

            }
        }
        public bool Validat_PassWord(string _password)
        {
            if (_password.Length < 8)
                return false;
            return true;
        }
        public bool Validat_Hint(string _hint)
        {
            if (!string.IsNullOrEmpty(_hint.Trim()))
            {
                if (_hint.Length < 8)
                {
                    return false;
                }
            }

            return true;



        }
    }
}
