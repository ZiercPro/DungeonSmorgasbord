using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 卡片信息
/// </summary>
public class CardBase
{
    /// <summary>
    /// 物品id
    /// </summary>
    public int id;
    /// <summary>
    /// 卡片类型 包括角色和事件两种类型
    /// </summary>
    public CardType CardType { get; private set; }

    /// <summary>
    /// 卡片文字
    /// </summary>
    public string text { get; private set; }

    /// <summary>
    /// card实现的效果
    /// </summary>
    public UnityAction<Transform> cardEffect { get; private set; }
    public CardBase(int id, CardType cardType, string text, UnityAction<Transform> effect)
    {
        this.id = id;
        CardType = cardType;
        this.text = text;
        cardEffect = effect;
    }
}

public enum CardType
{
    Hero,
    Event
}