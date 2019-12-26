using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GhostChat.BusinessLogic
{
    public static class Ghost
    {
        public static string EncryptionKey
        {
            get { return "2a5c93c3-2ad61b93-3ae52826-2a4ac547-6ccd1da4-db2c93b4-4b7d2d39-8eaed16c"; }
        }

        public static string Encrypt(string plaintext, string key)
        {
            int[] keyOrder = { 0, 1, 2, 3, 4, 5, 6, 7, 0, 1, 2, 3, 4, 5, 6, 7, 0, 1, 2, 3, 4, 5, 6, 7, 7, 6, 5, 4, 3, 2, 1, 0 };
            byte[] data = Encoding.Default.GetBytes(plaintext);
            
            string hexString = string.Join("", data.Select(i => i.ToString("x2")));

            return Algorithm(hexString, key, keyOrder);
        }

        public static string Decrypt(string ciphertext, string key)
        {
            int[] keyOrder = { 0, 1, 2, 3, 4, 5, 6, 7, 7, 6, 5, 4, 3, 2, 1, 0, 7, 6, 5, 4, 3, 2, 1, 0, 7, 6, 5, 4, 3, 2, 1, 0 };
            string hexPlaintext = Algorithm(ciphertext, key, keyOrder);

            var result = new byte[hexPlaintext.Length / 2];
            for (var i = 0; i < result.Length; i++)
                result[i] = Convert.ToByte(hexPlaintext.Substring(i * 2, 2), 16);

            return Encoding.Default.GetString(result).Trim();
        }

        private static string Algorithm(string data, string key, int[] keyOrder)
        {
            List<string> textBlocks = new List<string>();
            int blockLength = 64;

            data = Converter.FromHexToBinary(data);

            // Разбиваем текст на 64-битные блоки
            for (int i = 0; i < data.Length; i += blockLength)
            {
                try
                {
                    textBlocks.Add(data.Substring(i, blockLength));
                }
                catch(ArgumentOutOfRangeException)
                {
                    // Дополняем последний блок до 64 бит пробелами (utf-8)
                    string lastBlock = data.Substring(i, data.Length - i);
                    int additionalBytes = (blockLength - (data.Length - i)) / 8;
                    for (int j = 0; j < additionalBytes; j++)
                        lastBlock += "00100000";
                    textBlocks.Add(lastBlock);
                }
            }

            string[] rightBlock = new string[textBlocks.Count];
            string[] leftBlock = new string[textBlocks.Count];

            // Разделяем 64-битные текстовые блоки на 32-битные половины
            int count = 0;
            foreach (string block in textBlocks)
            {
                leftBlock[count] = block.Substring(0, blockLength / 2);
                rightBlock[count] = block.Substring(blockLength / 2, blockLength / 2);
                count++;
            }

            // Разбиваем исходный ключ на 8 32-битных блоков
            string[] keyBlocks = key.Split('-');
            
            string ciphertext = string.Empty;

            // Цикл шифрования по каждому блоку текста
            for (int blockN = 0; blockN < textBlocks.Count; blockN++)
            {
                uint temp;
                string rightBlockValue = rightBlock[blockN];
                uint leftBlockValue = Convert.ToUInt32(leftBlock[blockN], 2);

                bool isLastRound = false;
                // Цикл 32-раундового шифрования блока текста 
                for (int round = 0; round < keyOrder.Length; round++)
                {
                    if (round == keyOrder.Length - 1) isLastRound = true;

                    if (!isLastRound)
                    {
                        temp = leftBlockValue ^ Convert.ToUInt32(EncryptionFunction(keyBlocks[keyOrder[round]], rightBlockValue), 2);
                        leftBlockValue = Convert.ToUInt32(rightBlockValue, 2);
                        rightBlockValue = Convert.ToString(temp, 2); 
                    }
                    else
                    {
                        leftBlockValue = leftBlockValue ^ Convert.ToUInt32(EncryptionFunction(keyBlocks[keyOrder[round]], rightBlockValue), 2);
                    }
                    while (rightBlockValue.Length < 32)
                        rightBlockValue = "0" + rightBlockValue;
                }

                // Соединяем левую и правую части в шифрованный блок
                string leftBlockBinary = Convert.ToString(leftBlockValue, 2);
                while (leftBlockBinary.Length < 32)
                    leftBlockBinary = "0" + leftBlockBinary;

                string encryptedBlock = leftBlockBinary + rightBlockValue;

                // Записываем шифрованный блок в шифротекст
                ciphertext += encryptedBlock;
            }

            return Converter.FromBinaryToHex(ciphertext);
        }

        static private string EncryptionFunction(string keyBlock, string rightBlock)
        {
            uint keyValue = Convert.ToUInt32(keyBlock, 16);
            uint rightBlockValue = Convert.ToUInt32(rightBlock, 2);
            int subBlocksNumber = 8;
            
            // Выполняем операцию суммирования по модулю 2^32 для блока ключа и правого подблока текста
            string encryptedBlock = Convert.ToString(Convert.ToUInt32((keyValue + rightBlockValue) % Math.Pow(2, 32)), 2);
            
            // Дополняем полученный блок до 32 бит
            while (encryptedBlock.Length < 32)
                encryptedBlock = "0" + encryptedBlock;

            string[] encryptedSubBlock = new string[subBlocksNumber];
            int subBlockLength = encryptedBlock.Length / subBlocksNumber;
            // Делим результат суммирования на 8 4-битных последовательностей
            for (int i = 0; i < subBlocksNumber; i++)
            {
                encryptedSubBlock[i] = encryptedBlock.Substring(i * subBlockLength, subBlockLength);
            }

            encryptedBlock = string.Empty;
            // Замена значений с помощью s-блока
            for (int i = 0; i < encryptedSubBlock.Length; i++)
            {
                int subBlockValue = Convert.ToInt32(encryptedSubBlock[i], 2);
                subBlockValue = SubstitutionBlock.TC26[i, subBlockValue];
                string replacedSubBlock = Convert.ToString(subBlockValue, 2);
                
                // Дополняем подблок до 4 бит
                while (replacedSubBlock.Length < 4)
                    replacedSubBlock = "0" + replacedSubBlock;
                encryptedBlock += replacedSubBlock;
            }

            // Битовый сдвиг на 11 разрядов влево
            string shiftedBits = encryptedBlock.Substring(0, 11);
            encryptedBlock = encryptedBlock.Remove(0, 11);
            encryptedBlock += shiftedBits;

            return encryptedBlock;
        }
    }
}
