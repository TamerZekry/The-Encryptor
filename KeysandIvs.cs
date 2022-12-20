using System;
using System.Security.Cryptography;

namespace The_Encryptor
{
    class KeysandIvs
    {
        #region My BIG STRING
        private static readonly string temp2 = "KlvkdOv045dlnJNfvXevjYg6qqO7CUkO6wGJ7wzDIhmlpNmyDJ+ox7JnfkQxmx7HY9sx3hmqRDLhLC2RwBINnsQ17nho2Rv/l3qjBlFKA8EWG+9vzPJy9tG+cF9ZXW2+BeWnd+658tDPjGjNk18wmDF2n5FkVFgq902/tNDmmUuYtx1+nW1u62HrtSQZc+xN4rQZ4/3SU61sM1xt33zW7qinOXsnzLUhKmIM+WksLWcAVCdkkt0CcNBMbQdx5E1h54KgrWrkj/uFvPTTWmZL/4OBwzCaJswuXUaJEE/qIQpLHvMvecpVAQCO8ddULz+mdoW5o++kCysKaEKrNqtAhqnVM/sr2fT7j7zGEGdrEJbBd/FdHa5aR7i93C4dW1uOon2tgagjT0rFefpIvuFHS8eAa33zWo2wRHmovLZtC3Z2Np2r19KwEddJG6A2ERv2AODVYqwaON76ad+rjd4FhX2JdvG5cSza282FyWJJwC0IVfyftirl+xxokpHN2LvT82NFbT/BVcTPj0b8mSjWgugCOFqh7/MPTomLSp6nLI0kQwedt7l8O2JlFPufgPGpq1Tbm8RrAXCjaQx3b+v4iFsgxLsqQnNvhCUK9BFyOf8ZKvEO569syVGmq3D9bn0japs0IwlyAKjH/wl0TXz/5iXGrmcRK5kauORYpjzB5VdLfguwhRGQLG84ebCaVxxTSGSHVtTYerE1qIkJ+CUxfUxOqd/Gvkho8DgbAyRX+vh4Bwd8clLdTaXNW6tt4W28BLL7y0gfuLTptWxQEAxKWWSTOp324v5/Rssq5my0vlDm9INn6bq8o61HHBHa0bW61f0IXmiN17Va0bgkCh7PMSxCTocZQtrhgxACMI1P3frEO6lohtDvDNVcasA6/f4Lz0DpCuHrd9CtcRVx277Feq3Lo3IE1ZKnx2bp5cpaVhbhyQ51qHpiXD2HSIjjPwJPLsl2BE/4YTWmQfWRIGuZKJpbsGPYzMctUqpmHHEymdw2gtHu/0a48535EdJmXt9XXBDzEHbmrpY8+RGQnkHX5ShMimsW8hrD95n5BCE1cGoiBVCBBKq7T0rMxHQDToTfWvTfwJe7xyZ90IrJ3RAB6lHzmtrdT9m4AxB6BPHqTeZXkyIW5VDsBwtmqgKLpbmu+agNy0D1467VYN5PHzX8wWElCeem8TorhcC0uTTQf493OOxVzDyDngmiamz6GfMt4y/BuRxM9tZyeDwNwVNTvhxWToSYi0IOlQCWSH7/zSXjDQ93hJgg9qsB2VcgGETlqI4t2wDK6tvlqNc3ogYvXnITO+KEc1NqRA7pWLGOOmItdE1tSeRCGOir5Hx3eAu3Y4TqXDlCCei149eK58oydv2f0xvETYIxM6izshXM7PE=";
        #endregion
        private byte[] GetAllBytes() => Convert.FromBase64String(temp2);
        private byte[][] GetKeys()
        {
            byte[] _bytes = GetAllBytes();
            byte[][] _key = { new byte[32], new byte[16] };
            string _myString = " 12345678";
            byte count = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int result = Convert.ToInt32(_myString[j]) * (i + 1);
                    _key[0][count] = _bytes[result];
                    count++;
                }
                Array.Copy(_bytes, 1024, _key[1], 0, 16);
            }
            return _key;
        }
        public byte[] Enc(byte[] _msg)
        {
            if (_msg.Length < 50 || _msg.Length > 200)
            {
                throw new Exception("Invalid Operation.... Please Don't try THIS");

            }
            Aes  aes =   Aes.Create();
            ICryptoTransform crypto = aes.CreateEncryptor(GetKeys()[0], GetKeys()[1]);
            byte[] result = crypto.TransformFinalBlock(_msg, 0, _msg.Length);
            aes.Dispose();
            return result;
        }
        public byte[] Dec(byte[] _Enc_msg)
        {
            if (_Enc_msg.Length < 50 || _Enc_msg.Length > 200)
            {
                throw new Exception("Invalid Operation.... Please Don't try THIS");

            }
            Aes aes =   Aes.Create();
            ICryptoTransform crypto = aes.CreateDecryptor(GetKeys()[0], GetKeys()[1]);
            byte[] result = crypto.TransformFinalBlock(_Enc_msg, 0, _Enc_msg.Length);
            aes.Dispose();
            return result;
        }
    }

}
