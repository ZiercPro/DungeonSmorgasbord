using DG.Tweening;
using TMPro;
using UnityEngine;

namespace ZiercCode.Runtime.UI
{
    /// <summary>
    /// 互动物品的互动按钮UI
    /// </summary>
    public class InterActiveButtonUI : MonoBehaviour
    {
        public HotKey hotkey;
        private string _buttonText;
        private Tween _typeWriterT;
        public float duration = 1f;
        private string _originalText;
        private TextMeshPro _textMesh;

        private void Awake()
        {
            _textMesh = GetComponentInChildren<TextMeshPro>();
            GetText();
        }

        public void ShowText()
        {
            string currentT = "";
            _originalText = _textMesh.text;
            string endT = _originalText + _buttonText;
            _typeWriterT = DOTween.To(() => currentT, x => currentT = x, endT, duration).OnUpdate(
                () =>
                {
                    _textMesh.text = currentT;
                });
        }

        private void GetText()
        {
            switch (hotkey)
            {
                case HotKey.E:
                    _buttonText += " E";
                    break;
                case HotKey.Q:
                    _buttonText += " Q";
                    break;
                default:
                    break;
            }
        }
    }

    public enum HotKey
    {
        E, Q, Space
    }
}