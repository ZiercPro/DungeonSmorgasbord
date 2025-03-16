namespace ZiercCode.Utilities
{
    /// <summary>
    /// 普通单例
    /// 无单例则直接实例化
    /// </summary>
    /// <typeparam name="T">单例类型</typeparam>
    public class Singleton<T>  where T: new() 
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                }

                return _instance;
            }
        }
    }
}