using Runtime.PathFinding.Grid;
using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;

namespace Runtime.PathFinding.PathFinding
{
    public class PathFinding
    {
        private const int DEFAULT_STRAIGHT_COST = 10;
        private const int DEFAULT_DIAGONAL_COST = 14;

        private Grid<PathNode> _grid;
        private List<PathNode> _openList;
        private List<PathNode> _closeList;

        public Grid<PathNode> Grid => _grid;

        public PathFinding(int width, int height, float cellSize = 1f, Vector3 originPosition = default)
        {
            _grid = new Grid<PathNode>(width, height, cellSize, (g, x, y) => new PathNode(g, x, y), originPosition);
        }

        public List<PathNode> FindPath(PathNode startNode, PathNode endNode)
        {
            _openList = new List<PathNode> { startNode };
            _closeList = new List<PathNode>();

            //初始化每个节点
            for (int i = 0; i < _grid.Width; i++)
            {
                for (int j = 0; j < _grid.Height; j++)
                {
                    PathNode pathNode = _grid.GetGridObject(i, j);
                    pathNode.GCost = int.MaxValue;
                    pathNode.CalculateFCost();
                    pathNode.lastNode = null;
                }
            }

            startNode.GCost = 0;
            startNode.HCost = CalculateDistanceCost(startNode, endNode);
            startNode.CalculateFCost();

            //寻找路径
            while (_openList.Count > 0)
            {
                PathNode currentNode = FindLowestFCost(_openList);
                if (currentNode == endNode)
                {
                    //到达目标
                    return CalculateFinalPath(endNode);
                }

                _openList.Remove(currentNode);
                _closeList.Add(currentNode);
                //更新所有相邻节点的信息
                List<PathNode> neighbourList = GetNeighbourList(currentNode);
                foreach (PathNode neighbourNode in neighbourList)
                {
                    if (_closeList.Contains(neighbourNode)) continue;

                    int tempGCost = currentNode.GCost + CalculateDistanceCost(currentNode, neighbourNode);
                    if (neighbourNode.GCost > tempGCost)
                    {
                        neighbourNode.lastNode = currentNode;
                        neighbourNode.GCost = tempGCost;
                        neighbourNode.HCost = CalculateDistanceCost(neighbourNode, endNode);
                        neighbourNode.CalculateFCost();
                    }

                    if (!_openList.Contains(neighbourNode)) _openList.Add(neighbourNode);
                }
            }

            //找不到路径
            return null;
        }

        //获取所有相邻的节点
        private List<PathNode> GetNeighbourList(PathNode node)
        {
            List<PathNode> result = new List<PathNode>();
            if (node.X - 1 >= 0)
            {
                //左下
                if (node.Y - 1 >= 0) result.Add(_grid.GetGridObject(node.X - 1, node.Y - 1));
                //左
                result.Add(_grid.GetGridObject(node.X - 1, node.Y));
                //左上
                if (node.Y + 1 < _grid.Height) result.Add(_grid.GetGridObject(node.X - 1, node.Y + 1));
            }

            //下
            if (node.Y - 1 >= 0) result.Add(_grid.GetGridObject(node.X, node.Y - 1));
            //上
            if (node.Y + 1 < _grid.Height) result.Add(_grid.GetGridObject(node.X, node.Y + 1));
            if (node.X + 1 < _grid.Width)
            {
                //右下
                if (node.Y - 1 >= 0) result.Add(_grid.GetGridObject(node.X + 1, node.Y - 1));
                //右
                result.Add(_grid.GetGridObject(node.X + 1, node.Y));
                //右上
                if (node.Y + 1 < _grid.Height) result.Add(_grid.GetGridObject(node.X + 1, node.Y + 1));
            }

            return result;
        }

        //计算最短路径
        private List<PathNode> CalculateFinalPath(PathNode endNode)
        {
            List<PathNode> pathList = new List<PathNode>();
            PathNode currentNode = endNode;
            pathList.Add(endNode);
            while (currentNode.lastNode != null)
            {
                pathList.Add(currentNode.lastNode);
                currentNode = currentNode.lastNode;
            }
            return pathList;
        }

        //计算两点之间的最短距离
        private int CalculateDistanceCost(PathNode a, PathNode b)
        {
            int xDistance = Mathf.Abs(a.X - b.X);
            int yDistance = Mathf.Abs(a.Y - b.Y);
            int distance = Mathf.Abs(xDistance - yDistance);
            return DEFAULT_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + DEFAULT_STRAIGHT_COST * distance;
        }

        //计算最小的FCost
        private PathNode FindLowestFCost(List<PathNode> pathNodes)
        {
            PathNode result = pathNodes[0];
            for (int i = 1; i < pathNodes.Count; i++)
            {
                if (result.FCost > pathNodes[i].FCost)
                {
                    result = pathNodes[i];
                }
            }

            return result;
        }
    }
}