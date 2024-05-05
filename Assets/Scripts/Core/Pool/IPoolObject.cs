namespace ZiercCode.Core.Pool
{
    public interface IPoolObject
    {
        /// <summary>
        /// 获取时调用
        /// </summary>
        public void OnGet();

        /// <summary>
        /// 释放时调用
        /// </summary>
        public void OnRelease();
    }
}