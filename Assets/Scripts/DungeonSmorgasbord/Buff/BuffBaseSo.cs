﻿using NaughtyAttributes;
using UnityEngine;
using ZiercCode.Core.Pool;
using ZiercCode.Core.Utilities;

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
        /// 是否有粒子效果
        /// </summary>
        [field: SerializeField]
        public bool HaveParticle { get; protected set; }

        /// <summary>
        /// 粒子对象池数据
        /// </summary>
        [field: SerializeField, ShowIf("HaveParticle")]
        public PoolObjectSo particlePoolSo { get; protected set; }

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
        /// buff效果持有者
        /// </summary>
        private BuffEffective _buffEffective;

        /// <summary>
        /// 粒子生成处理器
        /// </summary>
        private SpawnHandle _particleSpawnHandle;

        public virtual void Init(BuffEffective buffEffective)
        {
            _buffEffective = buffEffective;

            if (HaveParticle)
            {
                GameObject particle = _particleSpawnHandle.GetObject();
                particle.transform.SetParent(_buffEffective.transform);
                particle.transform.localEulerAngles = Vector3.zero;
                particle.transform.localPosition = Vector3.zero;
            }


            Enable = true;

            _enableTimer = new Timer(duration);
            _activeTimer = new Timer(effectInternal);

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
            if (_activeTimer == null) return;
            _activeTimer.StartTimer();
        }

        public virtual void InActive()
        {
            if (HaveParticle)
                _particleSpawnHandle.Release();
            if (_activeTimer == null || _enableTimer == null) return;
            _activeTimer.StopTimer();
            _enableTimer.StopTimer();
            _activeTimer = null;
            _enableTimer = null;
        }

        public void ReSetDuration()
        {
            if (_enableTimer == null) return;
            _enableTimer.StartTimer();
        }

        public void SetSpawnHandle(SpawnHandle handle)
        {
            _particleSpawnHandle = handle;
        }

        protected BuffEffective GetBuffHolder()
        {
            return _buffEffective;
        }
    }
}