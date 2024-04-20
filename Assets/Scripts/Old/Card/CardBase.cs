using UnityEngine;
using UnityEngine.Events;

namespace ZiercCode.Old.Card
{
    /// <summary>
    /// 卡片基类，不同的卡片都需要继承该基类
    /// </summary>
    public class CardBase
    {
        /// <summary>
        /// 卡片id
        /// </summary>
        public int id;

        /// <summary>
        /// card实现的效果
        /// </summary>
        public UnityAction<Transform> cardEffect { get; private set; }

        public CardBase(int id, UnityAction<Transform> effect)
        {
            this.id = id;
            cardEffect = effect;
        }
    }
}