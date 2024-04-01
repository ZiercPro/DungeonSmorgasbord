using System.Collections;
using UnityEngine;

namespace ZRuntime
{
    
    public class HeroDash : MonoBehaviour
    {
        [SerializeField] private AnimationCurve dashSpeedCurve;
        [SerializeField] private GameObject dashParticle;

        private Movement _moveC;
        private BoxCollider2D _c2d;
        private Rigidbody2D _rb;

        public float dashCoolDown;
        public float dashTime;

        private float _dashSpeed;
        private bool _dashlocked;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _c2d = GetComponent<BoxCollider2D>();
            _moveC = GetComponent<Movement>();
        }

        public void StartDash(Vector2 dashDir)
        {
            if (_dashlocked) return;
            _dashlocked = true;
            DashEffect();
            Vector2 Dir = dashDir.normalized;
            StartCoroutine(Dashing(Dir));
        }

        private IEnumerator CoolDown()
        {
            yield return new WaitForSeconds(dashCoolDown);
            _dashlocked = false;
        }

        //public void DashSpriteGenerate() {
        //    GameObject newS = Instantiate(dashSpriteTemp, transform.position, Quaternion.identity);
        //    DashGhostFeedback dg = newS.GetComponent<DashGhostFeedback>();
        //    dg.color = dashSpriteColor;
        //    dg.m_sprite = heroSpriteRender.sprite;
        //}
        public void DashEffect()
        {
            GameObject particle = Instantiate(dashParticle, transform);
            particle.transform.position = transform.position;
        }

        IEnumerator Dashing(Vector2 dashDir)
        {
            _c2d.enabled = false;
            _moveC.canMove = false;
            for (float i = 0; i < dashTime; i += Time.deltaTime)
            {
                _dashSpeed = dashSpeedCurve.Evaluate(i);
                _rb.velocity = dashDir * _dashSpeed;
                yield return null;
            }

            _c2d.enabled = true;
            _moveC.canMove = true;
            yield return CoolDown();
        }
    }
}