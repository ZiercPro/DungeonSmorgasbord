namespace ZiercCode.Old.Basic
{
    /// <summary>
    /// 普通单例
    /// </summary>
    /// <typeparam name="T">单例类型</typeparam>
    public class SingletonDestroy<T>  where T: new() 
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