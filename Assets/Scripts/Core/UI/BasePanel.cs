using System;
using UnityEngine.InputSystem;
using ZiercCode.Old.Hero;
using ZiercCode.Old.Manager;

namespace ZiercCode.Core.UI
{
    /// <summary>
    /// 所有面板UI的父类
    /// </summary>
    public abstract class BasePanel
    {
        /// <summary>
        /// 这个Panel的UI信息
        /// </summary>
        public UIType UIType { get; private set; }

        /// <summary>
        /// UI辅助工具
        /// </summary>
        public UITool UITool { get; private set; }

        /// <summary>
        ///  该Panel的管理器
        /// </summary>
        public PanelManager PanelManager { get; private set; }

        /// <summary>
        /// UI管理器
        /// </summary>
        public UIManager UIManager { get; private set; }

        /// <summary>
        /// 玩家输入委托表
        /// </summary>
        private PlayerInputAction _playerInputAction;

        public BasePanel(UIType uiType)
        {
            UIType = uiType;
        }

        /// <summary>
        /// 界面的初始化工作
        /// 分配UITool
        /// 分配UI管理器
        /// UI显示时执行 
        /// </summary>
        /// <param name="uITool">UITool</param>
        /// <param name="pm">PanelManager</param>
        /// <param name="um">UIManager</param>
        public void Init(UITool uITool, PanelManager pm, UIManager um)
        {
            UITool = uITool;
            PanelManager = pm;
            UIManager = um;
            _playerInputAction = new PlayerInputAction();
        }

        /// <summary>
        /// UI进入时执行
        /// </summary>
        public virtual void OnEnter() { }

        /// <summary>
        /// UI暂停时执行
        /// </summary>
        public virtual void OnPause() { }


        /// <summary>
        /// UI继续时执行
        /// </summary>
        public virtual void OnResume() { }

        /// <summary>
        /// UI退出时执行
        /// </summary>
        public virtual void OnExit()
        {
            RemovePlayerInputAction();
        }

        /// <summary>
        /// 禁止玩家输入
        /// </summary>
        protected void BanPlayerInput()
        {
            if (GameManager.playerTrans)
                GameManager.playerTrans.GetComponent<HeroInputManager>().enabled = false;
        }

        /// <summary>
        /// 允许玩家输入
        /// </summary>
        protected void ReleasePlayerInput()
        {
            if (GameManager.playerTrans)
                GameManager.playerTrans.GetComponent<HeroInputManager>().enabled = true;
        }

        /// <summary>
        /// 设置back指令
        /// </summary>
        /// <param name="action"></param>
        protected void SetBackEvent(Action<InputAction.CallbackContext> action)
        {
            _playerInputAction.ShortKey.Enable();
            _playerInputAction.ShortKey.Back.performed += action;
        }

        /// <summary>
        /// 设置view指令
        /// </summary>
        /// <param name="action"></param>
        protected void SetViewEvent(Action<InputAction.CallbackContext> action)
        {
            _playerInputAction.ShortKey.Enable();
            _playerInputAction.ShortKey.View.performed += action;
        }

        private void RemovePlayerInputAction()
        {
            _playerInputAction.RemoveAllBindingOverrides();
            _playerInputAction.ShortKey.Disable();
            _playerInputAction = null;
        }
    }
}