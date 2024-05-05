using UnityEngine;

namespace ZiercCode.Core.Pool
{
    /// <summary>
    /// 对象池对象数据
    /// 所有需要用对象池管理的对象都需要先创建对应的数据
    /// </summary>
    [CreateAssetMenu(fileName = "PoolObjectData", menuName = "ScriptableObject/Pool/PoolObjectData")]
    public class PoolObjectSo : ScriptableObject
    {
        /// <summary>
        /// 对象池名称
        /// </summary>
        public string poolName;

        /// <summary>
        /// 预制件
        /// </summary>
        public GameObject prefab;

        /// <summary>
        /// 是否含有IPoolObject接口
        /// </summary>
        public bool haveIPoolObject;

        /// <summary>
        /// 对象池初始大小
        /// </summary>
        public int initSize;

        /// <summary>
        /// 对象池最大大小
        /// </summary>
        public int maxSize;
    }
}