using UnityEngine;
using ZiercCode.DungeonSmorgasbord.Damage;
using ZiercCode.Old.Manager;

namespace ZiercCode.DungeonSmorgasbord.Buff
{
    [CreateAssetMenu(fileName = "DamageBuff", menuName = "ScriptableObject/Buff/DamageBuff")]
    public class DamageBuffSo : BuffBaseSo
    {
        /// <summary>
        /// 伤害
        /// </summary>
        public int damage;

        /// <summary>
        /// 伤害类型
        /// </summary>
        public DamageType damageType;

        /// <summary>
        /// 伤害数字颜色
        /// </summary>
        public Color damageTextColor;

        private IDamageable _damageable;
        private DamageInfo _damageInfo;

        public override void Init(BuffEffective buffEffective)
        {
            base.Init(buffEffective);
            _damageable = buffEffective.GetComponent<IDamageable>();
            _damageInfo = new DamageInfo(damage, damageType, buffEffective.transform);
        }

        public override void Active()
        {
            base.Active();
            _damageable.TakeDamage(_damageInfo);
            TextPopupSpawner.Instance.InitPopupText(BuffHolderPosition, damageTextColor,
                _damageInfo.damageAmount);
        }

        public override void InActive()
        {
            _damageInfo = null;
            base.InActive();
        }
    }
}