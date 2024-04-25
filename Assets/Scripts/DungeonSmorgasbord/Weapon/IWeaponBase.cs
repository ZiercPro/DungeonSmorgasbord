using ZiercCode.DungeonSmorgasbord.ScriptObject;

namespace ZiercCode.DungeonSmorgasbord.Weapon
{
    /// <summary>
    /// 武器必须实现该接口
    /// </summary>
    public interface IWeaponBase
    {
        /// <summary>
        /// 左按钮按下
        /// </summary>
        public void OnLeftButtonPressStarted();

        /// <summary>
        /// 左按钮按住
        /// </summary>
        public void OnLeftButtonPressed();

        /// <summary>
        /// 左按钮松开
        /// </summary>
        public void OnLeftButtonPressCanceled();

        /// <summary>
        /// 右按钮按下
        /// </summary>
        public void OnRightButtonPressStarted();

        /// <summary>
        /// 右按钮按住
        /// </summary>
        public void OnRightButtonPressed();

        /// <summary>
        /// 右按钮松开
        /// </summary>
        public void OnRightButtonPressCanceled();
    }
}