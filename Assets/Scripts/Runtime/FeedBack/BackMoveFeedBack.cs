using System.Collections;
using UnityEngine;
/// <summary>
/// 视差效果
/// </summary>
public class BackMoveFeedBack : MonoBehaviour
{
    [SerializeField] private float moveOffSet;

    private Camera _camera;

    private Vector2 startPos;
    private bool canMove;
    private void Awake()
    {
        _camera = Camera.main;
        startPos = transform.position;
    }
    private void OnDisable()
    {
        StopCoroutine(Move());
    }
    public void OnMove()
    {
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
