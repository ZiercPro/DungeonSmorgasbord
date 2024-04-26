using UnityEngine;

namespace ZiercCode.DungeonSmorgasbord.Damage
{
    /// <summary>
    /// 可被伤害接口，所有可被造成伤害的类都需要实现该接口
    /// </summary>
    public interface IDamageable
    {
        /// <summary>
        /// 接受伤害
        /// </summary>
        /// <param name="info">伤害信息</param>
        public void TakeDamage(DamageInfo info);
    }
}