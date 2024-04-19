using System;
using UnityEngine.InputSystem;
using ZiercCode.Core.UI;

namespace ZiercCode.DungeonSmorgasbord.UI
{
    /// <summary>
    /// 可以监听玩家输入的面板
    /// </summary>
    public class BaseInputActionPanel : BasePanel
    {
        /// <summary>
        /// 玩家输入数据表，每次进入面板时会被重新分配
        /// </summary>
        protected PlayerInputAction PlayerInputAction;

        public BaseInputActionPanel(UIType uiType) : base(uiType)
        {
            PlayerInputAction = new PlayerInputAction();
        }

        /// <summary>
        /// UI进入时执行
        /// </summary>
        public override void OnEnter()
        {
            ReleaseUiInput();
        }

        /// <summary>
        /// UI暂停时执行
        /// </summary>
        public override void OnPause()
        {
            BanUiInput();
        }


        /// <summary>
        /// UI继续时执行
        /// </summary>
        public override void OnResume()
        {
            ReleaseUiInput();
        }

        /// <summary>
        /// UI退出时执行
        /// </summary>
        public override void OnExit()
        {
            BanUiInput();
            PlayerInputAction = null;
        }

        /// <summary>
        /// 禁用UI快捷输入
        /// </summary>
        protected void BanUiInput()
        {
            PlayerInputAction.UI.Disable();
        }

        /// <summary>
        /// 允许UI快捷输入
        /// </summary>
        protected void ReleaseUiInput()
        {
            PlayerInputAction.UI.Enable();
        }


        /// <summary>
        /// 设置输入监听，在进入面板时调用
        /// </summary>
        /// <param name="inputAction">输入委托类型</param>
        /// <param name="action">委托</param>
        protected void SetAction(InputAction inputAction, Action<InputAction.CallbackContext> action)
        {
            inputAction.started += action;
        }

        /// <summary>
        /// 获取view输入委托
        /// </summary>
        /// <returns></returns>
        protected InputAction GetViewInputAction()
        {
            return PlayerInputAction.UI.View;
        }

        /// <summary>
        /// 获取back输入委托
        /// </summary>
        /// <returns></returns>
        protected InputAction GetBackInputAction()
        {
            return PlayerInputAction.UI.Back;
        }
    }
}