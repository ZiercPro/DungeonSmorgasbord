﻿

namespace ZRuntime
{
    public class PathNode
    {
        private Grid<PathNode> _grid;
        private int _x;
        private int _y;

        /// <summary>
        /// 从起始位置到该位置最短消耗
        /// </summary>
        public int GCost;

        /// <summary>
        /// 从该点到目标位置的理想消耗
        /// </summary>
        public int HCost;

        /// <summary>
        /// G+H
        /// </summary>
        public int FCost;

        /// <summary>
        /// 上一个所在节点
        /// </summary>
        public PathNode LastNode;

        /// <summary>
        /// 该节点是否有效
        /// </summary>
        public bool IsActive;

        public int X => _x;
        public int Y => _y;

        public PathNode(Grid<PathNode> grid, int x, int y)
        {
            IsActive = true;
            _grid = grid;
            _x = x;
            _y = y;
        }

        public void CalculateFCost()
        {
            FCost = GCost + HCost;
        }

        public override string ToString()
        {
            return _x + "," + _y;
        }
    }
}