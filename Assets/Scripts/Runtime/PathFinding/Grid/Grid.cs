using UnityEngine;

namespace Runtime.PathFinding.Grid
{
    public class Grid<TGridNode>
    {
        private int _width;
        private int _height;
        private float _cellSize;
        private Vector3 _originPosition;

        private TGridNode[,] _gridArray;

        public Grid(int width, int height, float cellSize, Vector3 originPosition = default)
        {
            _width = width;
            _height = height;
            _cellSize = cellSize;
            _originPosition = originPosition;

            _gridArray = new TGridNode[width, height];
        }

        private Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3(x, y) * _cellSize + _originPosition;
        }

        private void GetXY(Vector3 worldPostion, out int x, out int y)
        {
            x = Mathf.FloorToInt((worldPostion - _originPosition).x / _cellSize);
            y = Mathf.FloorToInt((worldPostion - _originPosition).y / _cellSize);
        }

        public void SetValue(Vector3 worldPosition, TGridNode value)
        {
            int x, y;
            GetXY(worldPosition, out x, out y);
            SetValue(x, y, value);
        }

        public void SetValue(int x, int y, TGridNode value)
        {
            if (x >= 0 && y >= 0 && x < _width && y < _height)
            {
                _gridArray[x, y] = value;
            }
            else
            {
                Debug.LogWarning("超出数组范围");
            }
        }

        public TGridNode GetValue(int x, int y)
        {
            if (x > 0 && y > 0 && x < _width && y < _height)
            {
                return _gridArray[x, y];
            }
            else
            {
                return default;
            }
        }

        public TGridNode GetValue(Vector3 worldPosition)
        {
            int x, y;
            GetXY(worldPosition, out x, out y);
            return GetValue(x, y);
        }
    }
}