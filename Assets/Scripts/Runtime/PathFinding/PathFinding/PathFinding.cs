using Runtime.PathFinding.Grid;
using UnityEngine;

namespace Runtime.PathFinding.PathFinding
{
    public class PathFinding
    {
        private Grid<PathNode> _grid;

        public PathFinding(int width, int height)
        {
            _grid = new Grid<PathNode>(width, height, 1f, (g, x, y) => new PathNode(g, x, y));
        }
    }
}