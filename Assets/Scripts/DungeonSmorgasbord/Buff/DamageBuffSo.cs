using UnityEngine;
using ZiercCode.Core.Pool;
using ZiercCode.DungeonSmorgasbord.Damage;

namespace ZiercCode.DungeonSmorgasbord.Buff
{
    [CreateAssetMenu(fileName = "DamageBuff", menuName = "ScriptObject/Buff/DamageBuff")]
    public class DamageBuffSo : BuffBaseSo
    {
        public int damage;

        private IDamageable _damageable;
        private DamageInfo _damageInfo;

        public override void Init(BuffEffective buffEffective)
        {
            base.Init(buffEffective);
            _damageable = buffEffective.GetComponent<IDamageable>();
            _damageInfo = new DamageInfo(damage, DamageType.Magic, buffEffective.transform);
        }

        public override void Active()
        {
            base.Active();
            _damageable.TakeDamage(_damageInfo);
        }

        public override void InActive()
        {
            base.InActive();
            _damageInfo = null;
        }
    }
}