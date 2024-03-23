using System.Collections;
using UnityEngine;

/// <summary>
/// 视差效果
/// </summary>
[System.Serializable]
public class ParallaxMoveFeedBack
{
    [Tooltip("越小，离屏幕越近，越大,离屏幕越远")] [Range(0, 1)] [SerializeField]
    private float moveOffSet;

    [SerializeField] private Transform objectInstance;

    private Coroutine _backMoveCoroutine;
    private Camera _mainCamera;
    private Vector2 _startPos;
    private bool _canMove;

    public void OnMove(Camera parent)
    {
        _startPos = objectInstance.position;
        _mainCamera = parent;
        objectInstance.SetParent(_mainCamera.transform);
        _canMove = true;
        _backMoveCoroutine = MyCoroutineTool.Instance.StartMyCor(Move());
    }

    public void OnStop()
    {
        _canMove = false;
        objectInstance.SetParent(ParallaxMoveManager.Instance.environmentRoot);
        MyCoroutineTool.Instance.StopMyCor(_backMoveCoroutine);
    }

    IEnumerator Move()
    {
        while (_canMove)
        {
            Vector2 dist = _mainCamera.transform.position * moveOffSet;
            objectInstance.position = _startPos + dist;
            yield return null;
        }
    }
}