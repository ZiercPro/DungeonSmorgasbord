using DG.Tweening;
using UnityEngine;

namespace ZiercCode._DungeonGame.Juice
{
    public class HitShake : MonoBehaviour //受击时抖动
    {
        [SerializeField] private float moveStrength; //移动幅度
        [SerializeField] private float moveDuration; //移动时间
        [SerializeField] private float shakeDuration; //抖动时间
        [SerializeField] private float shakeStrength; //抖动幅度

        [SerializeField] private float doHitShakeInterval; //受击抖动执行间隔

        [SerializeField] private Transform animator; //只是贴图变化，逻辑位置不变
        private Tween _shake; //动画

        private float _intervalTimer;
        private bool _canShake = true;

        private void Update()
        {
            if (_intervalTimer > 0f)
            {
                _intervalTimer -= Time.deltaTime;
            }
            else if (!_canShake)
            {
                _canShake = true;
            }
        }


        //超受击反方向抖动
        public void DoShake(Vector2 hitDir)
        {
            if (!_canShake) return;
            _canShake = false;
            _intervalTimer = doHitShakeInterval;

            animator.DOLocalMove(hitDir * moveStrength, moveDuration).OnComplete(
                () => animator.DOShakePosition(shakeDuration, shakeStrength)
                    .OnComplete(() =>
                        animator.DOLocalMove(Vector3.zero, moveDuration).SetEase(Ease.Linear)));
            //.SetAutoKill(true); //因为是相对父物体移动 所以只需要相对0 0 0 添加位移
        }
    }
}