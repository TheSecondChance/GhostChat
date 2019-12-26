using System;
using System.Text;

namespace GhostChat.BusinessLogic
{
    public static class Converter
    {
        public static string ToBinaryString(string input)
        {
            StringBuilder builder = new StringBuilder();
            byte[] utf8Bytes = Encoding.Default.GetBytes(input);

            foreach (byte b in utf8Bytes)
            {
                string binaryByte = Convert.ToString(b, 2);

                while (binaryByte.Length < 8)
                    binaryByte = "0" + binaryByte;
                builder.Append(binaryByte);
                builder.Append(' ');
            }
            builder.Remove(builder.Length - 1, 1);

            return builder.ToString();
        }
        public static string ToString(string binaryString)
        {
            string[] stringBytes = binaryString.Split(' ');
            byte[] bytes = new byte[stringBytes.Length];

            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(stringBytes[i], 2);
            }
            return Encoding.Default.GetString(bytes);
        }
        public static string FromHexToBinary(string hexString)
        {
            string[] hexBytes = new string[hexString.Length / 2];
            for (int i = 0; i < hexBytes.Length; i++)
                hexBytes[i] = hexString.Substring(i * 2, 2);

            string binaryString = string.Empty;
            foreach (string hexByte in hexBytes)
                binaryString += Convert.ToString(Convert.ToByte(hexByte, 16), 2).PadLeft(8, '0');

            return binaryString;
        }
        public static string FromBinaryToHex(string binaryString)
        {
            string[] binaryBytes = new string[binaryString.Length / 8];
            for (int i = 0; i < binaryBytes.Length; i++)
                binaryBytes[i] = binaryString.Substring(i * 8, 8);

            string hexString = string.Empty;
            foreach (string binaryByte in binaryBytes)
                hexString += string.Format("{0:x2}", Convert.ToByte(binaryByte, 2));          

            return hexString;
        }
    }
}
