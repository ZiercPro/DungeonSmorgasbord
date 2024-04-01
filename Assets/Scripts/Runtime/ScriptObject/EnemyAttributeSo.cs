using UnityEngine;
using ZiercCode.Runtime.Damage;

namespace ZiercCode.Runtime.ScriptObject
{
    [CreateAssetMenu(menuName = "ScriptObj/Attributes/Enemy", fileName = "Enemy")]
    public class EnemyAttributeSo : AttributesBaseSo
    {
        public int difficulty;
        public int damageAmount;
        public DamageType damageType;
        public LayerMask attackLayer;
        public float attackSpeed;
        public float attackRange;
    }
}