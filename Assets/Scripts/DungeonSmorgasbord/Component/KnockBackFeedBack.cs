using System.Collections;
using UnityEngine;
using ZiercCode.DungeonSmorgasbord.Damage;

namespace ZiercCode.DungeonSmorgasbord.Component
{
    public class KnockBackFeedBack : MonoBehaviour
    {
        [SerializeField] private float backMoveTime = 0.15f;
        private Coroutine _backMoveCoroutine;
        private Rigidbody2D _rb2d;
        private MoveComponent _moveC;

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
                    _backMoveCoroutine = StartCoroutine(BackMove(startPosition, force));
                    break;
                case false:
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
            _moveC.Disable();
            Vector2 backMoveDir = (transform.position - attackerTransform.position).normalized;
            _rb2d.AddForce(backMoveDir * force, ForceMode2D.Impulse);
            yield return new WaitForSeconds(backMoveTime);
            _moveC.Enable();
        }
    }
}