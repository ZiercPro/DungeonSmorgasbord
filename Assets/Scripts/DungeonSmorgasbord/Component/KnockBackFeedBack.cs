using System.Collections;
using UnityEngine;

namespace ZiercCode.DungeonSmorgasbord.Component
{
    public class KnockBackFeedBack : MonoBehaviour
    {
        [SerializeField]
        private float backMoveTime = 0.15f;

        [SerializeField]
        private Rigidbody2D rb2d;

        [SerializeField]
        private MoveComponent moveC;

        private Coroutine _backMoveCoroutine;

        // private void Awake()
        // {
        //     moveC = GetComponent<MoveComponent>();
        //     rb2d = GetComponent<Rigidbody2D>();
        // }

        private void OnDisable()
        {
            if (_backMoveCoroutine != null)
                StopCoroutine(_backMoveCoroutine);
            moveC.Stop();
        }

        public void StartBackMove(Vector2 dir, float force)
        {
            switch (moveC == null)
            {
                case true:
                    if (!gameObject.activeInHierarchy) return;
                    _backMoveCoroutine = StartCoroutine(BackMove(dir, force));
                    break;
                case false:
                    if (!gameObject.activeInHierarchy) return;
                    _backMoveCoroutine = StartCoroutine(BackMoveWithMoveController(dir, force));
                    break;
            }
        }

        private IEnumerator BackMove(Vector2 dir, float force)
        {
            rb2d.AddForce(dir * force, ForceMode2D.Impulse);
            yield return new WaitForSeconds(backMoveTime);
            rb2d.velocity = Vector2.zero;
        }

        private IEnumerator BackMoveWithMoveController(Vector2 dir, float force)
        {
            moveC.enabled = false;
            moveC.Stop();
            rb2d.AddForce(dir * force, ForceMode2D.Impulse);
            yield return new WaitForSeconds(backMoveTime);
            rb2d.velocity = Vector2.zero;
            moveC.enabled = true;
        }
    }
}