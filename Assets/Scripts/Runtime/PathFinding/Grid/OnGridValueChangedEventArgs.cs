using System;

namespace ZRuntime
{
    /// <summary>
    /// Grid节点数值改变事件参数
    /// 记录数值改变节点位置
    /// </summary>
    public class OnGridValueChangedEventArgs : EventArgs
    {
        public int X;
        public int Y;
    }
}