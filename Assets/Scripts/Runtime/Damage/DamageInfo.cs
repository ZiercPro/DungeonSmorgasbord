using UnityEngine;

/// <summary>
/// 伤害信息类，储存相关信息
/// </summary>
public class DamageInfo
{
    /// <summary>
    /// 伤害数值
    /// </summary>
    public int damageAmount;
    /// <summary>
    /// 伤害类型
    /// </summary>
    public DamageType type;
    /// <summary>
    /// 伤害方
    /// </summary>
    public Transform owner;

    /// <summary>
    /// 生成伤害信息
    /// </summary>
    /// <param name="damageAmount">伤害数值</param>
    /// <param name="type">伤害类型</param>
    /// <param name="owner">伤害方</param>
    public DamageInfo(int damageAmount, DamageType type, Transform owner)
    {
        this.damageAmount = damageAmount;
        this.type = type;
        this.owner = owner;
    }
}