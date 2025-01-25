using System.Collections;
using UnityEngine;

namespace ZiercCode.DungeonSmorgasbord.Component
{
    public class KnockBackFeedBack : MonoBehaviour
    {
        [SerializeField] private float backMoveTime = 0.15f;
        private Rigidbody2D _rb2d;
        private MoveComponent _moveC;

        private Coroutine _backMoveCoroutine;

        private void Awake()
        {
            _moveC = GetComponent<MoveComponent>();
            _rb2d = GetComponent<Rigidbody2D>();
        }

        private void OnDisable()
        {
            if (_backMoveCoroutine != null)
                StopCoroutine(_backMoveCoroutine);
            _moveC.Stop();
        }

        public void StartBackMove(Transform startPosition, float force)
        {
            switch (_moveC == null)
            {
                case true:
                    if (!gameObject.activeInHierarchy) return;
                    _backMoveCoroutine = StartCoroutine(BackMove(startPosition, force));
                    break;
                case false:
                    if (!gameObject.activeInHierarchy) return;
                    _backMoveCoroutine = StartCoroutine(BackMoveWithMoveController(startPosition, force));
                    break;
            }
        }

        private IEnumerator BackMove(Transform attackerTransform, float force)
        {
            Vector2 backMoveDir = (transform.position - attackerTransform.position).normalized;
            _rb2d.AddForce(backMoveDir * force, ForceMode2D.Impulse);
            yield return new WaitForSeconds(backMoveTime);
        }

        private IEnumerator BackMoveWithMoveController(Transform attackerTransform, float force)
        {
            _moveC.enabled = false;
            _moveC.Stop();
            Vector2 backMoveDir = (transform.position - attackerTransform.position).normalized;
            _rb2d.AddForce(backMoveDir * force, ForceMode2D.Impulse);
            yield return new WaitForSeconds(backMoveTime);
            _moveC.enabled = true;
        }
    }
}