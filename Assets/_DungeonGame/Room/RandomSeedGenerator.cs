using System;
using System.Text;
using Random = UnityEngine.Random;

namespace ZiercCode._DungeonGame.Room
{
    public class RandomSeedGenerator //随机种子生成器
    {
        //生成随机种子 
        public string GetRandomSeed(int minLenght, int maxLenght)
        {
            int seedLenght = Random.Range(minLenght, maxLenght);

            StringBuilder seed = new StringBuilder();

            for (int i = 0; i < seedLenght; i++)
            {
                seed.Append((char)ToAscii(Random.Range(0, 61)));
            }

            return seed.ToString();
        }

        //将0-61转换为ascii表中的48-57 65-90 97-122
        private int ToAscii(int value)
        {
            if (value is >= 0 and <= 9)
                value += '0';
            else if (value is >= 10 and <= 35)
                value += 'A' - 10;
            else if (value is >= 36 and <= 61)
                value += 'a' - 36;
            else
                throw new Exception("数字范围错误");

            return value;
        }
    }
}