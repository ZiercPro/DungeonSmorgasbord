using NaughtyAttributes;
using Runtime.Helper;
using UnityEngine;
using Runtime.PathFinding.Grid;
using Runtime.PathFinding.PathFinding;
using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V12;

public class PathFindingTest : MonoBehaviour
{
    private PathFinding _pathFinding;
    private PathNode _startNode;
    private PathNode _endNode;
    private List<PathNode> _path;
    private Vector3 _mousePosition;

    private void Awake()
    {
        _pathFinding = new PathFinding(20, 20, 1f, new Vector3(-8, -4));
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
                Debug.Log(_startNode + " | " + _endNode);
                _path = _pathFinding.FindPath(_startNode, _endNode);
            }
            else
            {
                Debug.Log("no startPosition");
            }


            if (_path is { Count: > 0 })
            {
                foreach (PathNode node in _path)
                {
                    TextPopupSpawner.Instance.InitPopupText(_pathFinding.Grid.GetWorldPosition(node.X, node.Y),
                        Color.green, node.FCost);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _startNode = _pathFinding.Grid.GetGridObject(_mousePosition);
        }
    }
}