using System.Collections;
using UnityEngine;
using ZiercCode.Runtime.Damage;

namespace ZiercCode.Runtime.Component
{

    public class KnockBackFeedBack : MonoBehaviour
    {
        [SerializeField] private float backMoveTime = 0.15f;
        [SerializeField] private float force = 10f;
        private Coroutine _backMoveCoroutinel;
        private Rigidbody2D _rb2d;
        private Movement _moveC;

        private void Awake()
        {
            _moveC = GetComponent<Movement>();
            _rb2d = GetComponent<Rigidbody2D>();
        }

        private void OnDisable()
        {
            if (_backMoveCoroutinel != null)
                StopCoroutine(_backMoveCoroutinel);
            _moveC.StopMovePerform();
        }

        public void StartBackMove(DamageInfo info)
        {
            Transform damager = info.owner;
            switch (_moveC == null)
            {
                case true:
                    _backMoveCoroutinel = StartCoroutine(BackMove(damager));
                    break;
                case false:
                    _backMoveCoroutinel = StartCoroutine(BackMoveWithMoveController(damager));
                    break;
            }
        }

        private IEnumerator BackMove(Transform attackerTransform)
        {
            _moveC.canMove = false;
            Vector2 backMoveDir = (transform.position - attackerTransform.position).normalized;
            _rb2d.AddForce(backMoveDir * force, ForceMode2D.Impulse);
            yield return new WaitForSeconds(backMoveTime);
            _moveC.canMove = true;
        }

        private IEnumerator BackMoveWithMoveController(Transform attackerTransform)
        {
            _moveC.canMove = false;
            Vector2 backMoveDir = (transform.position - attackerTransform.position).normalized;
            _rb2d.AddForce(backMoveDir * force, ForceMode2D.Impulse);
            yield return new WaitForSeconds(backMoveTime);
            _moveC.canMove = true;
        }
    }
}