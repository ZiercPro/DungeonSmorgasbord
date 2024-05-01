using UnityEngine;
using ZiercCode.Core.Extend;

namespace ZiercCode.DungeonSmorgasbord.Buff
{
    /// <summary>
    /// buff基类 所有buff效果都要继承这个类
    /// </summary>
    public abstract class BuffBaseSo : ScriptableObject
    {
        /// <summary>
        /// 用来确认是否为同一类型的buff
        /// </summary>
        public int buffId;

        /// <summary>
        /// 持续时间
        /// </summary>
        public float duration;

        /// <summary>
        /// 生效间隙时间
        /// </summary>
        public float effectInternal;

        /// <summary>
        /// 是否可叠加
        /// </summary>
        public bool addAble;

        /// <summary>
        /// buff效果还在
        /// </summary>
        public bool Enable { get; protected set; }

        /// <summary>
        /// 有效计时器
        /// </summary>
        private Timer _enableTimer;

        /// <summary>
        /// 生效计时器
        /// </summary>
        private Timer _activeTimer;

        /// <summary>
        /// buff持有者
        /// </summary>
        protected BuffEffective BuffEffective;


        public virtual void Init(BuffEffective buffEffective)
        {
            BuffEffective = buffEffective;
            _enableTimer = new Timer(duration);
            _activeTimer = new Timer(effectInternal);
            Enable = true;
            _enableTimer.TimerTrigger += () => { Enable = false; };
            _activeTimer.TimerTrigger += Active;
            _enableTimer.StartTimer();
        }

        public void BuffTimeUpdate()
        {
            _enableTimer.Tick();
            _activeTimer.Tick();
        }


        public virtual void Active()
        {
            _activeTimer.StartTimer();
        }

        public virtual void InActive()
        {
            _enableTimer = null;
            _activeTimer = null;
        }

        public void ReSetDuration()
        {
            _enableTimer.StartTimer();
        }
    }
}