using DG.Tweening;
using UnityEngine;

namespace ZiercCode.Core.UI
{
    /// <summary>
    /// 含有进出动画的面板基类，含有进出动画的面板需要继承或重构其中的动画方法
    /// </summary>
    public abstract class BaseAnimationPanel : BasePanel
    {
        protected BaseAnimationPanel(UIType uiType) : base(uiType) { }

        public override void OnEnter()
        {
            InAnimate(1.5f);
        }

        public override void OnExit()
        {
            OutAnimate(1f);
        }

        public virtual void InAnimate(float animationTime)
        {
            CanvasGroup cGroup = UITool.GetOrAddComponent<CanvasGroup>();
            cGroup.alpha = 0f;
            cGroup.DOFade(1f, animationTime).SetUpdate(true);
        }

        public virtual void OutAnimate(float animationTime)
        {
            CanvasGroup cGroup = UITool.GetOrAddComponent<CanvasGroup>();
            cGroup.DOFade(0f, animationTime).SetUpdate(true);
        }
    }
}