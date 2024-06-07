using UnityEngine;
using ZiercCode.Core.Pool;
using ZiercCode.Old.Audio;
using ZiercCode.Old.Manager;
using ZiercCode.DungeonSmorgasbord.Component;

namespace ZiercCode.DungeonSmorgasbord.Enemy
{
    public class BigMouseEnemy : Enemy
    {
        [Header("粒子效果"), Space] [SerializeField]
        private PoolObjectSo deadParticle;

        protected override void Start()
        {
            base.Start();

            StateMachine.AddState<BigMouseEnemyState.Idle>();
            StateMachine.AddState<BigMouseEnemyState.Chase>();

            StateMachine.Run<BigMouseEnemyState.Idle>();
        }

        public override void OnAttack(Transform target)
        {
            //弹出文本
            TextPopupSpawner.Instance.InitPopupText(target.position, Color.red, -attribute.AttributesData.damageAmount);
            //击退
            KnockBackFeedBack knockBackFeedBack = GetComponent<KnockBackFeedBack>();
            knockBackFeedBack.StartBackMove(transform, 0.5f);
        }


        public override void Dead()
        {
            poolObjectSpawner.SpawnPoolObjectWithAutoRelease(deadParticle, transform.position, 5f);
            AudioPlayer.Instance.PlayAudioAsync(AudioName.EnemyDead4);
            base.Dead();
        }
    }
}