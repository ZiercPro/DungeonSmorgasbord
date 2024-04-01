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
        private TextMeshPro textMesh;
        private string buttonText = "";
        private string originalText;
        private Tween typeWriterT;
        public float duration = 1f;

        private void Awake()
        {
            textMesh = GetComponentInChildren<TextMeshPro>();
            GetText();
        }

        public void ShowText()
        {
            string currentT = "";
            originalText = textMesh.text;
            typeWriterT = DOTween.To(() => currentT, x => currentT = x, originalText + buttonText, duration).OnUpdate(
                () =>
                {
                    textMesh.text = currentT;
                });
        }

        private void GetText()
        {
            switch (hotkey)
            {
                case HotKey.E:
                    buttonText += " E";
                    break;
                case HotKey.Q:
                    buttonText += " Q";
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