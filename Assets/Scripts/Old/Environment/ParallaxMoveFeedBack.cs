using System.Collections;
using UnityEngine;
using ZiercCode.Core.Extend;

namespace ZiercCode.Old.Environment
{
    /// <summary>
    /// 视差效果
    /// </summary>
    [System.Serializable]
    public class ParallaxMoveFeedBack
    {
        [SerializeField] private Transform objectInstance;

        [Tooltip("越小，离屏幕越近，越大,离屏幕越远")] [Range(0, 1)] [SerializeField]
        private float moveOffSet;


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
}