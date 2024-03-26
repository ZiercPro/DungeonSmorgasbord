using NaughtyAttributes;
using UnityEngine;
using Runtime.PathFinding.PathFinding;
using System.Collections.Generic;

public class PathFindingTest : MonoBehaviour
{
    [SerializeField] private GameObject blockCube;
    private PathFinding _pathFinding;
    private PathNode _startNode;
    private PathNode _endNode;
    private List<PathNode> _path;
    private Vector3 _mousePosition;

    private void Awake()
    {
        _pathFinding = new PathFinding(20, 20, 0.5f, new Vector3(-8, -4));
        //AudioPlayerManager.Instance.PlayAudio(AudioName.MenuBgm);
    }

    [Button("GridDebug")]
    private void DebugMode()
    {
        _pathFinding.Grid.DebugDrawLine(Color.red);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (_startNode != null)
            {
                _endNode = _pathFinding.Grid.GetGridObject(_mousePosition);
                _path = _pathFinding.FindPath(_startNode, _endNode);
            }
            else
            {
                Debug.Log("no startPosition");
            }

            Debug.Log(_endNode);
            Debug.Log(_pathFinding.Grid.GetWorldPosition(_endNode.X, _endNode.Y));

            if (_path is { Count: > 0 })
            {
                for (int i = 0; i < _path.Count - 1; i++)
                {
                    Debug.DrawLine(
                        _pathFinding.Grid.GetWorldPosition(_path[i].X, _path[i].Y) +
                        (Vector3.right + Vector3.up) * (_pathFinding.Grid.CellSize * 0.5f),
                        _pathFinding.Grid.GetWorldPosition(_path[i + 1].X, _path[i + 1].Y) +
                        (Vector3.right + Vector3.up) * (_pathFinding.Grid.CellSize * 0.5f), Color.green, 2f);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _startNode = _pathFinding.Grid.GetGridObject(_mousePosition);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            PathNode blockNode = _pathFinding.Grid.GetGridObject(_mousePosition);
            blockNode.IsActive = false;
            GameObject newBlock = Object.Instantiate(blockCube,
                _pathFinding.Grid.GetWorldPosition(blockNode.X, blockNode.Y) +
                (Vector3.right + Vector3.up) * (_pathFinding.Grid.CellSize * 0.5f), Quaternion.identity);
            newBlock.transform.localScale = new Vector3(_pathFinding.Grid.CellSize, _pathFinding.Grid.CellSize,
                _pathFinding.Grid.CellSize);
            newBlock.SetActive(true);
        }
    }
}