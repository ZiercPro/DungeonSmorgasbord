using Runtime.PathFinding.Grid;

namespace Runtime.PathFinding.PathFinding
{
    public class PathNode
    {
        private Grid<PathNode> _grid;
        private int _x;
        private int _y;

        public int GCost;//从起始位置到该位置最短消耗
        public int HCost;//从该点到目标位置的理想消耗
        public int FCost;//G+H

        public PathNode lastNode;//上一个所在节点

        public PathNode(Grid<PathNode> grid, int x, int y)
        {
            _grid = grid;
            _x = x;
            _y = y;
        }

        public override string ToString()
        {
            return _x + "," + _y;
        }
    }
}