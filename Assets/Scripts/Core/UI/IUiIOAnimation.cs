namespace ZiercCode.Core.UI
{
    /// <summary>
    /// 如果UI进入弹出有动画，需要实现该接口
    /// </summary>
    public interface IUiIOAnimation
    {
        /// <summary>
        /// 进入动画
        /// </summary>
        public void InAnimate(float animationTime);
        
        /// <summary>
        /// 弹出动画
        /// </summary>
        public void OutAnimate(float animationTime);
    }
}