using DG.Tweening;
using UnityEngine;

public class MagneticEffect : MonoBehaviour
{
    [Header("目标Tag")] [SerializeField] private string targetTag;
    [Header("磁力半径")] [SerializeField] private float radius;
    [Header("磁力大小")] [SerializeField] private float force;

    private Transform _targetTransform;
    private Tweener _tweener;
    private bool _getTarget;

    private void DectectTarget()
    {
        Collider2D[] c2d = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D target in c2d)
        {
            if (target.CompareTag(targetTag))
            {
                _targetTransform = target.transform;
                _getTarget = true;
                //Debug.Log($"find you!{tag}");
            }
        }
    }

    private void Magnetize()
    {
        force += Time.deltaTime;
        if (!_tweener.IsActive())
            _tweener = transform.DOMove(_targetTransform.position, 1 / force).SetAutoKill(true);
    }

    private void Update()
    {
        if (_getTarget)
            Magnetize();
        else
            DectectTarget();
    }
}