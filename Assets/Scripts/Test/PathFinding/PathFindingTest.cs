using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZiercCode.Core.Extend;
using Object = UnityEngine.Object;

namespace ZiercCode.Test.PathFinding
{

    public class PathFindingTest : MonoBehaviour
    {
        [SerializeField] private GameObject blockCube;

        [Header("movable info")] [Space] [SerializeField]
        private Rigidbody2D pathFindAI;

        [SerializeField] private float moveSpeed;
        private PathFinding _pathFinding;
        private PathNode _startNode;
        private PathNode _endNode;
        private Vector3 _mousePosition;

        private List<PathNode> _pathNodes;
        private List<Vector3> _path;

        private void Awake()
        {
            _pathFinding = new PathFinding(20, 20, 1f, new Vector3(-8, -4));
        }

        private void Start()
        {
           // AudioPlayerManager.Instance.PlayAudioAsync(AudioName.MenuBgm);
        }
#if UNITY_EDITOR
        [Button("GridDebug")]
        private void DebugMode()
        {
            _pathFinding.GridBase.DebugDrawLine(Color.red);
        }
#endif


        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (_startNode != null)
                {
                    _endNode = _pathFinding.GridBase.GetGridObject(_mousePosition);
                    _pathNodes = _pathFinding.FindPath(_startNode, _endNode);
                }
                else
                {
                    Debug.Log("no startPosition");
                }

                Debug.Log(_endNode);
                Debug.Log(_pathFinding.GridBase.GetWorldPosition(_endNode.X, _endNode.Y));

                if (_pathNodes is { Count: > 0 })
                {
                    for (int i = 0; i < _pathNodes.Count - 1; i++)
                    {
                        Debug.DrawLine(
                            _pathFinding.GridBase.GetWorldPosition(_pathNodes[i].X, _pathNodes[i].Y) +
                            (Vector3.right + Vector3.up) * (_pathFinding.GridBase.CellSize * 0.5f),
                            _pathFinding.GridBase.GetWorldPosition(_pathNodes[i + 1].X, _pathNodes[i + 1].Y) +
                            (Vector3.right + Vector3.up) * (_pathFinding.GridBase.CellSize * 0.5f), Color.green, 2f);
                    }
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _startNode = _pathFinding.GridBase.GetGridObject(_mousePosition);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                PathNode blockNode = _pathFinding.GridBase.GetGridObject(_mousePosition);
                blockNode.IsActive = false;
                GameObject newBlock = Object.Instantiate(blockCube,
                    _pathFinding.GridBase.GetWorldPosition(blockNode.X, blockNode.Y) +
                    (Vector3.right + Vector3.up) * (_pathFinding.GridBase.CellSize * 0.5f), Quaternion.identity);
                newBlock.transform.localScale = new Vector3(_pathFinding.GridBase.CellSize, _pathFinding.GridBase.CellSize,
                    _pathFinding.GridBase.CellSize);
                newBlock.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _path = _pathFinding.FindPath(pathFindAI.position, _mousePosition);
                // foreach (var pathNode in _path)
                // {
                //     Debug.Log(pathNode);
                // }
                MoveTo(_path, pathFindAI, moveSpeed);
            }
        }

        private void MoveTo(List<Vector3> path, Rigidbody2D targetRigid2d, float speed)
        {
            // StartCoroutine(TranslationCoroutine(path, targetRigid2d, speed, 0.1f));
            MyCoroutineTool.Instance.StartCoroutine(TranslationCoroutine(path, targetRigid2d, speed, 0.1f));
        }

        IEnumerator TranslationCoroutine(List<Vector3> path, Rigidbody2D targetRigid2d, float speed,
            float nearestDistance)
        {
            yield return null;
            int nodeIndex = path.Count - 2;
            while (nodeIndex >= 0)
            {
                yield return MoveUnit(targetRigid2d, path[nodeIndex], speed, nearestDistance);
                nodeIndex--;
            }
        }

        IEnumerator MoveUnit(Rigidbody2D unit, Vector3 targetPostion, float speed, float nearestDistance)
        {
            while (true)
            {
                if (MyMath.CalculateDistance(unit.position, targetPostion) <= nearestDistance)
                {
                    Debug.Log(MyMath.CalculateDistance(unit.position, targetPostion));
                    yield break;
                }

                unit.velocity = (targetPostion - unit.transform.position).normalized * speed;
            }
        }
    }
}