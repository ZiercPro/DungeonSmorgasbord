namespace ZiercCode
{
    /// <summary>
    /// 引用对象接口 通过引用池获取实例的类都需要实现该接口
    /// </summary>
    public interface IReference
    {
        /// <summary>
        /// 被创建时调用
        /// </summary>
        void OnSpawn();
    }
}