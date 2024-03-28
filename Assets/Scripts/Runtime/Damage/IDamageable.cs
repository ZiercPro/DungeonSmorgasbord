using System;

namespace Runtime.Damage
{
    /// <summary>
    /// 可被伤害接口
    /// </summary>
    public interface IDamageable
    {
        public event Action<DamageInfo> OnTakeDamage;
        public void TakeDamage(DamageInfo info);
    }
}