namespace ZiercCode.Old.Data
{
    /// <summary>
    /// 数据服务接口 提供数据服务的类接入
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <typeparam name="T">保存的数据类型</typeparam>
        /// <param name="relativePath">相对路径</param>
        /// <param name="data">数据对象</param>
        /// <param name="encrypted">是否加密</param>
        public bool SaveData<T>(string relativePath, T data, bool encrypted);

        /// <summary>
        /// 加载数据 json转为对象
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="relativePath">相对路径</param>
        /// <param name="encrypted">是否加密</param>
        /// <returns></returns>
        public T LoadData<T>(string relativePath, bool encrypted);
    }
}