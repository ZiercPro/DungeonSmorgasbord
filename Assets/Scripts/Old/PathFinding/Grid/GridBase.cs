using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ZiercCode.Old.PathFinding.Grid
{
    public class GridBase<TGridNode>
    {
        public event EventHandler<OnGridValueChangedEventArgs> OnValueChanged;

        private int _width;
        private int _height;
        private float _cellSize;
        private Vector3 _originPosition;

        private TGridNode[,] _gridArray;


        public int Width => _width;
        public int Height => _height;
        public float CellSize => _cellSize;

        public GridBase(int width, int height, float cellSize, Func<GridBase<TGridNode>, int, int, TGridNode> createGridObject,
            Vector3 originPosition = default)
        {
            _width = width;
            _height = height;
            _cellSize = cellSize;
            _originPosition = originPosition;

            _gridArray = new TGridNode[width, height];
            for (int i = 0; i < _gridArray.GetLength(0); i++)
            {
                for (int j = 0; j < _gridArray.GetLength(1); j++)
                {
                    _gridArray[i, j] = createGridObject(this, i, j);
                }
            }
        }

        public void GetXY(Vector3 worldPostion, out int x, out int y)
        {
            x = Mathf.FloorToInt((worldPostion - _originPosition).x / _cellSize);
            y = Mathf.FloorToInt((worldPostion - _originPosition).y / _cellSize);
        }

        public Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3(x, y) * _cellSize + _originPosition;
        }

        public void SetGridObject(Vector3 worldPosition, TGridNode value)
        {
            int x, y;
            GetXY(worldPosition, out x, out y);
            SetGridObject(x, y, value);
        }

        public void SetGridObject(int x, int y, TGridNode value)
        {
            if (x >= 0 && y >= 0 && x < _width && y < _height)
            {
                _gridArray[x, y] = value;
            }
            else
            {
                Debug.LogWarning("超出数组范围");
            }

            OnValueChanged?.Invoke(this, new OnGridValueChangedEventArgs { X = x, Y = y });
        }

        public TGridNode GetGridObject(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < _width && y < _height)
            {
                return _gridArray[x, y];
            }
            else
            {
                return default;
            }
        }

        public TGridNode GetGridObject(Vector3 worldPosition)
        {
            int x, y;
            GetXY(worldPosition, out x, out y);
            return GetGridObject(x, y);
        }
#if UNITY_EDITOR
        public void DebugDrawLine(Color color, Object debugObject = null)
        {
            for (int i = 0; i < _gridArray.GetLength(0); i++)
            {
                for (int j = 0; j < _gridArray.GetLength(1); j++)
                {
                    if (debugObject) Object.Instantiate(debugObject, GetWorldPosition(i, j), Quaternion.identity);
                    Debug.DrawLine(GetWorldPosition(i, j),
                        GetWorldPosition(i + 1, j), color,
                        Single.PositiveInfinity);
                    Debug.DrawLine(GetWorldPosition(i, j),
                        GetWorldPosition(i, j + 1), color,
                        Single.PositiveInfinity);
                }
            }
        }
#endif
    }
}