using UnityEngine;
using UnityEngine.AI;
using ZiercCode._DungeonGame._Scripts.AttackSystem;
using ZiercCode._DungeonGame._Scripts.EntityClasses;
using ZiercCode._DungeonGame._Scripts.EventClasses;
using ZiercCode.DungeonSmorgasbord.Component;
using ZiercCode.EventBusSystem;
using ZiercCode.FiniteStateMachine;

namespace ZiercCode._DungeonGame._Scripts.EnemyClasses
{
    /// <summary>
    /// 敌怪基类
    /// </summary>
    [RequireComponent(typeof(FlashFeedBack))]
    public abstract class Enemy_Base : Entity
    {
        [SerializeField]
        protected Animator animator;

        [SerializeField]
        protected AutoFlipComponent autoFlipComponent;

        public NavMeshAgent navMeshAgent;

        protected StateMachine StateMachine;

        [HideInInspector]
        public IAttackAble attackTarget;

        protected EventsGroup EventsGroup = new();

        protected FlashFeedBack FlashFeedBack;

        protected virtual void OnEnable()
        {
            EventsGroup.AddListener<AttackEvent.WeaponAttack>(InnerHandleAttack);
        }

        protected virtual void OnDisable()
        {
            EventsGroup.RemoveAllListener();
        }

        protected virtual void Awake()
        {
            StateMachine = new StateMachine();
            FlashFeedBack = GetComponent<FlashFeedBack>();
        }

        protected override void Start()
        {
            base.Start();
            StateMachine.Initialize(this);
        }

        protected override void PauseUpdate()
        {
            base.PauseUpdate();
            animator.speed = motionSpeed;
            navMeshAgent.speed = moveSpeed;
            autoFlipComponent.FaceTo(navMeshAgent.velocity + transform.position);
        }

        private void InnerHandleAttack(IEventArgs args)
        {
            if (args is AttackEvent.WeaponAttack weaponAttack)
            {
                if (weaponAttack.Target == (IAttackAble)this)
                {
                    HandleAttack();
                }
            }
        }

        protected virtual void HandleAttack()
        {
            FlashFeedBack.Flash();
        }
    }
}