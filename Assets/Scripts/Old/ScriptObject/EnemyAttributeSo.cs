using UnityEngine;
using ZiercCode.Old.Damage;

namespace ZiercCode.Old.ScriptObject
{
    [CreateAssetMenu(menuName = "ScriptObj/Attributes/Enemy", fileName = "Enemy")]
    public class EnemyAttributeSo : AttributesBaseSo
    {
        public int difficulty;
        public int damageAmount;
        public DamageType damageType;
        public float attackSpeed;
        public float attackRange;
    }
}