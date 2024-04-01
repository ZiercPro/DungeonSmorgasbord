namespace ZRuntime
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

        public UITool UITool { get; private set; }
        public PanelManager PanelManager { get; private set; }
        public UIManager UIManager { get; private set; }

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
        }

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
        public virtual void OnExit() { }
    }
}