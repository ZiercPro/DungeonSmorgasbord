using DG.Tweening;
using UnityEngine;

namespace ZRuntime
{
    public class MagneticEffect : MonoBehaviour
    {
        [Header("目标Tag")] [SerializeField] private string targetTag;
        [Header("磁力半径")] [SerializeField] private float radius;
        [Header("磁力大小")] [SerializeField] private float force;

        [SerializeField] private Collider2D _entityCollider2D;

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
                    if (_entityCollider2D != null) _entityCollider2D.enabled = false;
                    //Debug.Log($"find you!{tag}");
                }
            }
        }

        private void Magnetize()
        {
            if (_tweener.IsActive()) return;
            _tweener = transform.DOMove(_targetTransform.position, 1 / force).SetAutoKill(true).OnUpdate(() =>
            {
                force += Time.deltaTime;
                transform.DOMove(_targetTransform.position, 1 / force).SetEase(Ease.OutFlash);
            });
        }

        private void Update()
        {
            if (_getTarget)
                Magnetize();
            else
                DectectTarget();
        }
    }
}