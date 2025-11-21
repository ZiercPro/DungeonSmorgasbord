using UnityEngine;
using ZiercCode._DungeonGame._Scripts.AttackSystem;
using ZiercCode.FakeHeight;
using ZiercCode.Utilities;

namespace ZiercCode._DungeonGame._Scripts.WeaponClass
{
    /// <summary>
    /// 针头
    /// </summary>
    public class Projectile_Needle : VerticalProjectile
    {
        [SerializeField]
        private GameObject fixedShadow;

        [SerializeField]
        private float startAngle; //发射时的初始角度 以向右为标准

        [SerializeField]
        private Vector2 endAngle; //触地后的插在地面的角度 以向右为标准

        private float _startAngle;
        private float _endAngle;

        private FakeHeightTransform _fakeHeightTransform;
        private SpriteRenderer _casterSpriteRenderer;
        private Vector3 _startShadowPosition;

        private float _rotateSpeed;

        private RangeDetector _rangeDetector = new(5);

        protected override void Awake()
        {
            base.Awake();
            _fakeHeightTransform = GetComponent<FakeHeightTransform>();
            _casterSpriteRenderer = _fakeHeightTransform.casterTransform.GetComponent<SpriteRenderer>();
            _startShadowPosition = fixedShadow.transform.localPosition;
        }

        public void StopRotation()
        {
            _rotateSpeed = 0f;
        }

        public override void Init(BaseWeapon myWeapon)
        {
            base.Init(myWeapon);
            fixedShadow.SetActive(false);
            InitRotation();
            _fakeHeightTransform.casterTransform.rotation = Quaternion.identity;
            _fakeHeightTransform.casterTransform.Rotate(_fakeHeightTransform.casterTransform.forward, _startAngle);
            float flyTime = myWeapon.shootDistance / myWeapon.projectileSpeed;
            _rotateSpeed = (_endAngle - _startAngle) / flyTime;
        }

        protected override void PauseUpdate()
        {
            base.PauseUpdate();
            _fakeHeightTransform.casterTransform.Rotate(_fakeHeightTransform.casterTransform.forward,
                _rotateSpeed * Time.deltaTime);
        }

        protected override void CheckHit()
        {
            if (_rangeDetector.DetectInCircleByLayer(myWeapon.myHolder.targetFaction, fixedShadow.transform.position,
                    myWeapon.projectileSize / 2f))
            {
                Collider2D[] hits = _rangeDetector.GetColliders();
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i] && hits[i].TryGetComponent(out IAttackAble attackAble))
                    {
                        DoAttack(attackAble);
                    }
                }
            }
        }

        protected override void SyncFlip()
        {
            base.SyncFlip();
            mySpriteRenderer.flipX = !HolderAutoFlipComponent.IsFacingRight;
        }

        //初始化 初始角度和结束角度 用于模拟针筒随重力旋转效果
        private void InitRotation()
        {
            if (HolderAutoFlipComponent.IsFacingRight)
            {
                _startAngle = startAngle;
                _endAngle = Random.Range(endAngle.x, endAngle.y);
            }
            else
            {
                _startAngle = -startAngle;
                _endAngle = -Random.Range(endAngle.x, endAngle.y);
            }
        }

        public void InitFixedShadow()
        {
            fixedShadow.transform.localPosition = _startShadowPosition *
                                                  (MyMath.BoolToInt(!_casterSpriteRenderer.flipX) == 1
                                                      ? 1f
                                                      : -1f);
            fixedShadow.transform.rotation = Quaternion.identity;
            fixedShadow.SetActive(true);
        }
    }
}