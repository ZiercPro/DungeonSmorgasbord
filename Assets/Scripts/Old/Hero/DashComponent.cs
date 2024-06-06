using NaughtyAttributes.Scripts.Core.MetaAttributes;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.Serialization;
using ZiercCode.DungeonSmorgasbord.Component;
using ZiercCode.Old.Component;

namespace ZiercCode.Old.Hero
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class DashComponent : MonoBehaviour
    {
        [SerializeField, Tooltip("是否使用速度曲线")] private bool useSpeedCurve;

        [SerializeField, HideIf("useSpeedCurve")]
        private float dashSpeed;

        [SerializeField, ShowIf("useSpeedCurve")]
        private AnimationCurve dashSpeedCurve;

        [SerializeField] private GameObject dashParticle;

        [SerializeField] private float dashCoolDown = 1.0f;
        [SerializeField] private float dashTime = 0.3f;

        public UnityEvent dashStarted;
        public UnityEvent dashing;
        public UnityEvent dashEnded;

        private MoveComponent _moveC;
        private Rigidbody2D _rb;


        private bool _isDashLocked;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _moveC = GetComponent<MoveComponent>();
        }

        public void Dash(Vector2 dashDirection)
        {
            if (_isDashLocked) return;
            _isDashLocked = true;
            DashEffect();
            Vector2 directionNormalized = dashDirection.normalized;
            StartCoroutine(Dashing(directionNormalized));
        }

        private void DashEffect()
        {
            GameObject particle = Instantiate(dashParticle, transform);
            particle.transform.localPosition = Vector3.zero;
        }

        private IEnumerator Dashing(Vector2 dashDir)
        {
            dashStarted?.Invoke();
            _moveC.Disable();
            for (float i = 0; i < dashTime; i += Time.deltaTime)
            {
                dashing?.Invoke();
                if (useSpeedCurve)
                    dashSpeed = dashSpeedCurve.Evaluate(i);
                _rb.velocity = dashDir * dashSpeed;
                yield return null;
            }

            dashEnded?.Invoke();
            _moveC.Enable();
            yield return new WaitForSeconds(dashCoolDown);
            _isDashLocked = false;
        }
    }
}