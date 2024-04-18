using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ZiercCode.Core.UI
{
    /// <summary>
    /// 面板管理，用栈储存UI
    /// </summary>
    public class PanelManager
    {
        private Stack<BasePanel> panelStack;
        private UIManager uiManager;
        private BasePanel curPanel;

        private PlayerInputAction _playerInputAction;
        private Action<InputAction.CallbackContext> _escAction;

        public PanelManager()
        {
            panelStack = new Stack<BasePanel>();
            uiManager = new UIManager();
            _playerInputAction = new PlayerInputAction();
            SetEscAction();
        }

        /// <summary>
        /// Panel的入栈操作
        /// </summary>
        /// <param name="nextPanel">要显示的面板</param>
        /// <returns>新显示的面板</returns>
        public BasePanel Push(BasePanel nextPanel)
        {
            if (panelStack.Count > 0)
                curPanel.OnPause();

            curPanel = nextPanel;

            panelStack.Push(nextPanel);

            GameObject newP = uiManager.GetSingleUI(nextPanel.UIType);

            nextPanel.Init(new UITool(newP), this, uiManager);
            nextPanel.OnEnter();

            return nextPanel;
        }

        /// <summary>
        /// 当前面板的出栈操作
        /// </summary>
        public void Pop()
        {
            if (panelStack.Count > 1)
            {
                panelStack.Pop().OnExit();
                curPanel = panelStack.Peek();
                curPanel.OnResume();
            }
        }

        /// <summary>
        /// 弹出所有面板 
        /// </summary>
        public void PopAll()
        {
            while (panelStack.Count > 0)
            {
                panelStack.Pop().OnExit();
            }

            curPanel = null;
        }

        private void SetEscAction()
        {
            _escAction = e => { curPanel.OnEsc(); };
            _playerInputAction.ShortKey.Enable();
            _playerInputAction.ShortKey.Back.performed += _escAction;
        }

        private void DeleteEscAction()
        {
            _playerInputAction.ShortKey.Back.performed -= _escAction;
            _playerInputAction.ShortKey.Disable();
        }
    }
}