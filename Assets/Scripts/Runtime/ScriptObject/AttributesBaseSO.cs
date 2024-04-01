using UnityEngine;

namespace ZiercCode.Runtime.ScriptObject
{
    /// <summary>
    /// 储存游戏对象基本的静态数据
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptObj/Attributes", fileName = "Attributes")]
    public class AttributesBaseSO : ScriptableObject
    {
        public int maxHealth;
        public float moveSpeed;
    }
}