using System;

namespace GhostChat.BusinessLogic
{
    public class KeyGenerator
    {
        private static int keyLength = 256;

        public static string Generate()
        {
            Random randomizer = new Random();
            string key = "";
            for (int i = 0; i < keyLength / 4; i++)
            {
                if (i % 8 == 0 && i != 0)
                    key += '-';
                key += Convert.ToString(randomizer.Next(15), 16);             
            }
            return key;
        }
    }
}
