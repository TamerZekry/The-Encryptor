namespace The_Encryptor
{
    public static class Helper
    {
        /// <summary>
        /// Convert an integer 568 to a byte array {5,6,8} to 
        /// </summary>
        /// <param name="_int"></param>
        /// <returns></returns>
        public static byte[] IntToByteArray(this int _int)
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
        /// Convert byte array {1,2,3} to an integer 123
        /// </summary>
        /// <param name="_byte"></param>
        /// <returns></returns>
        public static int ByteArrayToInt(this byte[] _byte)
        {
            string res = string.Empty;
            foreach (byte item in _byte)
            {
                res += item.ToString();
            }
            return int.Parse(res);
        }




    }
}
