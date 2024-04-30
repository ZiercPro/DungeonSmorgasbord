using UnityEngine;
using UnityEngine.Events;

namespace ZiercCode.DungeonSmorgasbord.Weapon
{
    /// <summary>
    /// 武器输入处理类，用于监听玩家输入并触发相应事件
    /// </summary>
    public class WeaponInputHandler : MonoBehaviour
    {
        public UnityEvent leftButtonPressPerformed;
        public UnityEvent leftButtonPressCanceled;
        public UnityEvent rightButtonPressPerformed;
        public UnityEvent rightButtonPressCanceled;
        public UnityEvent<Vector2> viewPositionChange;
    }
}