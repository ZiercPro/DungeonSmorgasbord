namespace ZiercCode.Core.Data
{
    /// <summary>
    /// 数据仓库接口
    /// </summary>
    /// <typeparam name="T">具体数据类型</typeparam>
    public interface IDataStore<T> where T : IDataStore<T>
    {

        /// <summary>
        /// 保存数据
        /// </summary>
        public void Save();

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Load(out T data);
    }
}