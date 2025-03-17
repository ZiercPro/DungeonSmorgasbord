using UnityEngine;

namespace ZiercCode
{
    public class CanvasGroupUser : MonoBehaviour // 通过CanvasGroup组件 禁用视图
    {
        [SerializeField] private CanvasGroup canvasGroup;

        public void Enable(float alpha = 1f)
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = alpha;
        }

        public void Disable(float alpha = 0f)
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = alpha;
        }
    }
}