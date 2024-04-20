using System;

namespace ZiercCode.Old.Damage
{
    /// <summary>
    /// 可被伤害接口
    /// </summary>
    public interface IDamageable
    {
        public void TakeDamage(DamageInfo info);
    }
}