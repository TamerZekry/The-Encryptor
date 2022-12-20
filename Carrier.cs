using System.IO;
using System.Linq;

namespace The_Encryptor
{
    public class Carrier
    {
        private FileInfo _file_Info;
        private string _hint;
        private byte[] _hashed_Password;
        private string _msg;
        public readonly bool Encrypting;

        public Carrier(FileInfo fileInfo, string hint, string _msg)
        {
            FileInfo = fileInfo;
            Hint = hint;
            this._msg = _msg;
            Encrypting = fileInfo.Extension != ".enc";
        }

        public FileInfo FileInfo { get => _file_Info; set => _file_Info = value; }
        public string Hint { get => _hint; set => _hint = value; }
        public string Msg { get => _msg; set => _msg = value; }
        public byte[] HashedPassword { get => _hashed_Password; set => _hashed_Password = value; }
        public FileInfo RemoveEnc()
        {
            return new FileInfo(string.Concat(_file_Info.FullName.Take(_file_Info.FullName.Length - 4)));
        }
    }
}
