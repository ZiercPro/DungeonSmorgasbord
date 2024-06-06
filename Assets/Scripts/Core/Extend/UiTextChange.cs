using TMPro;
using UnityEngine;

namespace ZiercCode.Core.Extend
{
    /// <summary>
    /// ui元素文本自定义变化，用于美化ui，比如在鼠标选中时，在两边添加符号
    /// </summary>
    public class UiTextChange : MonoBehaviour
    {
        //修饰用的字符
        [SerializeField] protected string sModifier = "-";

        //需要个性化的文本
        [SerializeField] private TextMeshProUGUI textMeshProUGUI;

        private string _originalText; //初始文本
        private string _targetText; //目标文本

        protected void Start()
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
            textMeshProUGUI.text = _targetText;
        }

        /// <summary>
        /// 恢复文本
        /// </summary>
        public virtual void Recover()
        {
            textMeshProUGUI.text = _originalText;
        }


        /// <summary>
        /// 手动更新组件初始文本和目标文本，初始化时和语言切换时调用
        /// </summary>
        public virtual void UpdateText()
        {
            _originalText = textMeshProUGUI.text;

            if (_originalText.Contains(sModifier))
            {
                string[] temp = _originalText.Split(sModifier);
                _originalText = temp[1];
            }

            _targetText = sModifier + _originalText + sModifier;
        }
    }
}