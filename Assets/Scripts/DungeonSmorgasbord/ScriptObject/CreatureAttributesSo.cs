using UnityEngine;

namespace ZiercCode.DungeonSmorgasbord.ScriptObject
{
    /// <summary>
    /// 储存游戏对象基本的数据
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptObject/Attributes/BaseAttributes", fileName = "BaseAttributes")]
    public class CreatureAttributesSo : ScriptableObject
    {
        /// <summary>
        /// 属性持有者预制件
        /// </summary>
        public GameObject instance;

        /// <summary>
        /// 持有者名称
        /// </summary>
        [Space, Header("属性")] public string myName;

        /// <summary>
        /// 最大生命值
        /// </summary>
        public int maxHealth = 0;

        /// <summary>
        /// 移动速度
        /// </summary>
        public float moveSpeed = 0;

        /// <summary>
        /// 暴击率
        /// </summary>
        public float criticalChance = 0;
    }
}