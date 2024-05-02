using NaughtyAttributes;
using UnityEngine;
using ZiercCode.Core.Extend;
using ZiercCode.Core.Pool;

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
        [SerializeField] private bool haveParticle;

        /// <summary>
        /// 粒子效果预制件
        /// </summary>
        [SerializeField, ShowIf("haveParticle")]
        private GameObject particlePrefab;

        /// <summary>
        /// 粒子对象池初始大小
        /// </summary>
        [SerializeField, ShowIf("haveParticle")]
        private int particlePoolInitSize;


        /// <summary>
        /// 粒子对象池最大大小
        /// </summary>
        [SerializeField, ShowIf("haveParticle")]
        private int particlePoolMaxSize;


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
        /// 粒子效果实例
        /// </summary>
        private GameObject _particleInstance;

        /// <summary>
        /// buff持有者
        /// </summary>
        protected BuffEffective BuffEffective;


        private GameObject CreateFunc()
        {
            GameObject newParticle = Instantiate(particlePrefab);
            return newParticle;
        }

        private void GetFunc(GameObject particle)
        {
            particle.SetActive(true);
        }

        private void ReleaseFunc(GameObject particle)
        {
            PoolManager.Instance.DefaultReleaseFunc(particle.name, particle);
        }

        private void DestroyFunc(GameObject particle)
        {
            Destroy(particle);
        }

        public virtual void Init(BuffEffective buffEffective)
        {
            //粒子效果初始化
            if (haveParticle)
            {
                PoolManager.Instance.CreatePool(particlePrefab.name, CreateFunc, GetFunc, ReleaseFunc, DestroyFunc,
                    false, particlePoolInitSize, particlePoolMaxSize);
                _particleInstance = PoolManager.Instance.GetPoolObject(particlePrefab.name, buffEffective.transform);
            }

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
            PoolManager.Instance.ReleasePoolObject(particlePrefab.name, _particleInstance);
        }

        public void ReSetDuration()
        {
            _enableTimer.StartTimer();
        }
    }
}