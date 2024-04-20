using TMPro;
using UnityEngine;

namespace ZiercCode.Old.UI
{
    /// <summary>
    /// ui元素文本自定义变化，用于美化ui，比如在鼠标选中时，在两边添加符号
    /// 支持两种模式，根据bool切换和直接手动切换
    /// </summary>
    public class UiTextChange : MonoBehaviour
    {
        //修饰用的字符
        [SerializeField] protected string sModifier = "-";

        private TextMeshProUGUI _textMeshProUGUI;
        private string _originalText; //初始文本
        private string _targetText; //目标文本

        private void Awake()
        {
            UpdateText();
        }

        protected virtual void OnDisable()
        {
            //在切换页面时，防止一些文本没有被恢复
            Recover();
        }

        /// <summary>
        /// 修改文本
        /// </summary>
        public virtual void Change()
        {
            _textMeshProUGUI.text = _targetText;
        }

        /// <summary>
        /// 恢复文本
        /// </summary>
        public virtual void Recover()
        {
            _textMeshProUGUI.text = _originalText;
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
        /// 手动更新组件初始文本和目标文本，语言切换时调用
        /// </summary>
        public virtual void UpdateText()
        {
            _textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>(true);
            _originalText = _textMeshProUGUI.text;
            if (_originalText.Contains(sModifier))
            {
                string[] temp = _originalText.Split(sModifier);
                _originalText = temp[1];
            }

            _targetText = sModifier + _originalText + sModifier;
        }
    }
}