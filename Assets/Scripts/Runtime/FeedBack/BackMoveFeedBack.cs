using System.Collections;
using UnityEngine;
/// <summary>
/// 视差效果
/// </summary>
public class BackMoveFeedBack : MonoBehaviour
{
    [Tooltip("越小，离屏幕越近，越大,离屏幕越远")][Range(0,1)]
    [SerializeField] private float moveOffSet;

    private Camera _camera;

    private Vector2 startPos;
    private bool canMove;
    private void Awake()
    {
        startPos = transform.position;
    }
    private void OnDisable()
    {
        StopCoroutine(Move());
    }
    public void OnMove(Camera parent)
    {
        _camera = parent;
        transform.SetParent(_camera.transform);
        canMove = true;
        StartCoroutine(Move());
    }
    public void OnStop()
    {
        canMove = false;
        transform.SetParent(EnvironmentManager.Instance.environmentRoot);
        StopCoroutine(Move());
    }
    IEnumerator Move()
    {
        while (canMove)
        {
            Vector2 dist = _camera.transform.position * moveOffSet;
            transform.position = startPos + dist;
            yield return null;
        }
    }
}
