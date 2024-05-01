using NaughtyAttributes;
using UnityEngine;
using ZiercCode.DungeonSmorgasbord.Damage;

namespace ZiercCode.DungeonSmorgasbord.Buff
{
    [CreateAssetMenu(fileName = "DamageBuff", menuName = "ScriptObject/Buff/DamageBuff")]
    public class DamageBuffSo : BuffBaseSo
    {
        public int damage;

        [SerializeField] private bool haveParticle;

        /// <summary>
        /// 粒子效果
        /// </summary>
        [SerializeField, ShowIf("haveParticle")]
        private GameObject fireParticle;

        private GameObject _fireParticleInstance;
        private IDamageable _damageable;
        private DamageInfo _damageInfo;

        public override void Init(BuffEffective buffEffective)
        {
            base.Init(buffEffective);
            _damageable = buffEffective.GetComponent<IDamageable>();
            if (haveParticle)
            {
                _fireParticleInstance = Instantiate(fireParticle, buffEffective.transform);
                _fireParticleInstance.transform.localPosition = Vector3.zero;
            }

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
            Destroy(_fireParticleInstance);
            _damageInfo = null;
        }
    }
}