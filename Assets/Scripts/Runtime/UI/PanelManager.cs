using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Runtime.UI
{
    using Manager;
    using UIFramework;

    /// <summary>
    /// 面板管理，用栈储存UI
    /// </summary>
    public class PanelManager
    {
        private Stack<BasePanel> panelStack;
        private UIManager uiManager;
        private BasePanel curPanel;

        public PanelManager()
        {
            panelStack = new Stack<BasePanel>();
            uiManager = new UIManager();
            GameRoot.Instance.OnEsc.AddListener(() =>
            {
                Pop();
            });
        }

        /// <summary>
        /// Panel的入栈操作
        /// </summary>
        /// <param name="nextPanel">要显示的面板</param>
        /// <returns>新显示的面板</returns>
        public BasePanel Push(BasePanel nextPanel)
        {
            if (panelStack.Count > 0)
            {
                curPanel = panelStack.Peek();
                curPanel.OnPause();
            }

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
                panelStack.Peek().OnResume();
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
        }

        /// <summary>
        /// 判断panel类型是否相同
        /// </summary>
        /// <param name="panel1">panel1</param>
        /// <param name="panel2">panel2</param>
        /// <returns>true则相同，false则不同</returns>
        public static bool isPanelClassEquals(BasePanel panel1, BasePanel panel2)
        {
            return panel1.GetType() == panel2.GetType();
        }
    }
}