using UnityEngine;
using ZiercCode.DungeonSmorgasbord.Damage;

namespace ZiercCode.DungeonSmorgasbord.Buff
{
    [CreateAssetMenu(fileName = "FireBuff", menuName = "ScriptObject/Buff/FireBuff")]
    public class FireBuffSo : BuffBaseSo
    {
        public int damage;

        /// <summary>
        /// 火焰粒子效果
        /// </summary>
        [SerializeField] private GameObject fireParticle;

        private GameObject _fireParticleInstance;
        private IDamageable _damageable;
        private DamageInfo _damageInfo;

        public override void Init(BuffEffective buffEffective)
        {
            base.Init(buffEffective);
            _damageable = buffEffective.GetComponent<IDamageable>();
            _fireParticleInstance = Instantiate(fireParticle, buffEffective.transform);
            _fireParticleInstance.transform.localPosition = Vector3.zero;
            _damageInfo = new DamageInfo(damage, DamageType.Magic, _fireParticleInstance.transform);
        }

        public override void Active()
        {
            base.Active();
            _damageable.TakeDamage(_damageInfo);
        }

        public override void InActive()
        {
            base.InActive();
            Destroy(_fireParticleInstance);
            _damageInfo = null;
        }
    }
}