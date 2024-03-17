using System.Collections;
using UnityEngine;

namespace Runtime.FeedBack
{
    public class KnockBackFeedBack : MonoBehaviour
    {
        [SerializeField] private float backMoveTime;
        [SerializeField] private float force;
        private Rigidbody2D _rb2d;
        private Movement _moveC;

        private void Awake()
        {
            _moveC = GetComponent<Movement>();
            _rb2d = GetComponent<Rigidbody2D>();
        }

        public void StartBackMove(DamageInfo info)
        {
            Transform damager = info.owner;
            switch (_moveC == null)
            {
                case true:
                    StartCoroutine(BackMove(damager));
                    break;
                case false:
                    StartCoroutine(BackMoveWithMoveController(damager));
                    break;
            }
        }

        private IEnumerator BackMove(Transform attackerTransform)
        {
            Vector2 backMoveDir = (transform.position - attackerTransform.position).normalized;
            _rb2d.AddForce(backMoveDir * force, ForceMode2D.Impulse);
            yield return new WaitForSeconds(backMoveTime);
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