using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace ZiercCode.Core.UI
{
    /// <summary>
    /// 面板管理，用栈储存UI
    /// </summary>
    public class PanelManager
    {
        /// <summary>
        /// 面板栈，保存储存管理的面板
        /// </summary>
        private Stack<BasePanel> _panelStack;

        /// <summary>
        /// ui管理器
        /// </summary>
        private UIManager _uiManager;

        /// <summary>
        /// 当前置顶面板
        /// </summary>
        private BasePanel _curPanel;

        private PlayerInputAction _playerInputAction;
        private Action<InputAction.CallbackContext> _escAction;

        public PanelManager()
        {
            _panelStack = new Stack<BasePanel>();
            _uiManager = new UIManager();
            _playerInputAction = new PlayerInputAction();
        }

        /// <summary>
        /// Panel的入栈操作
        /// </summary>
        /// <param name="nextPanel">要显示的面板</param>
        /// <returns>新显示的面板</returns>
        public BasePanel Push(BasePanel nextPanel)
        {
            if (_panelStack.Count > 0)
                _curPanel.OnPause();

            _curPanel = nextPanel;

            _panelStack.Push(nextPanel);

            GameObject newP = _uiManager.GetSingleUI(nextPanel.UIType);

            nextPanel.Init(new UITool(newP), this, _uiManager);
            nextPanel.OnEnter();

            return nextPanel;
        }

        /// <summary>
        /// 当前面板的出栈操作
        /// </summary>
        public void Pop()
        {
            if (_panelStack.Count > 1)
            {
                _panelStack.Pop().OnExit();
                _curPanel = _panelStack.Peek();
                _curPanel.OnResume();
            }
        }

        /// <summary>
        /// 弹出所有面板 
        /// </summary>
        public void PopAll()
        {
            while (_panelStack.Count > 0)
            {
                _panelStack.Pop().OnExit();
            }

            _curPanel = null;
        }
    }
}