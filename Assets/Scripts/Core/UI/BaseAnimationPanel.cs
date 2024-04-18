namespace ZiercCode.Core.UI
{
    /// <summary>
    /// 含有进出动画的面板基类，含有进出动画的面板需要继承并实现其中的动画方法
    /// </summary>
    public abstract class BaseAnimationPanel : BasePanel, IUiIOAnimation
    {
        protected BaseAnimationPanel(UIType uiType) : base(uiType)
        {
        }

        public abstract void InAnimate(float animationTime);
        public abstract void OutAnimate(float animationTime);
    }
}