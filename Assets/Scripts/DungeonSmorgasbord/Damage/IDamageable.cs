namespace ZiercCode.DungeonSmorgasbord.Damage
{
    /// <summary>
    /// 可被伤害接口
    /// </summary>
    public interface IDamageable
    {
        public void TakeDamage(DamageInfo info);
    }
}