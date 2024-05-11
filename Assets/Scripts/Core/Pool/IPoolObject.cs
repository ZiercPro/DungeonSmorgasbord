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

        /// <summary>
        /// 设置生成处理器
        /// 方便自己处理释放
        /// </summary>
        public void SetSpawnHandle(SpawnHandle handle);
    }
}