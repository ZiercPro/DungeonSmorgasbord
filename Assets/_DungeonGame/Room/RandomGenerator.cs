namespace ZiercCode._DungeonGame.Room
{
    public class RandomGenerator // 随机生成器 需要一个种子初始化
    {
        private System.Random _generator;

        public RandomGenerator(string seed)
        {
            int seedHash = seed.GetHashCode();
            _generator = new System.Random(seedHash);
        }

        //随机范围内整数
        public int Next(int minValue, int maxValue)
        {
            return _generator.Next(minValue, maxValue);
        }

        //获取随机双精度浮点数
        public double NextDouble()
        {
            return _generator.NextDouble();
        }

        public void NextBytes(byte[] buffer)
        {
            _generator.NextBytes(buffer);
        }
    }
}