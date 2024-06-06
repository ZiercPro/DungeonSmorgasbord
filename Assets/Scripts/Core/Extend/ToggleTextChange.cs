using UnityEngine;
using UnityEngine.UI;

namespace ZiercCode.Core.Extend
{
    /// <summary>
    /// 用于toggle的文本更新
    /// </summary>
    public class ToggleTextChange : UiTextChange
    {
        [SerializeField] private Toggle targetToggle;

        protected override void OnDisable()
        {
            base.OnDisable();
            SwitchByToggleState();
        }

        public override void UpdateText()
        {
            base.UpdateText();
            SwitchByToggleState();
        }

        /// <summary>
        /// 切换文本，tigger真则修改，否则为原文本
        /// </summary>
        /// <param name="trigger">切换触发器</param>
        public virtual void TextSwitch(bool trigger)
        {
            if (trigger) Change();
            else Recover();
        }

        /// <summary>
        /// 根据toggle状态来切换文本
        /// </summary>
        private void SwitchByToggleState()
        {
            TextSwitch(targetToggle.isOn);
        }
    }
}