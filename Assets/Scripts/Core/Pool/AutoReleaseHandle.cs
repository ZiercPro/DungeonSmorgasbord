using ZiercCode.Core.Utilities;

namespace ZiercCode.Core.Pool
{
    /// <summary>
    /// 自动释放处理类
    /// </summary>
    public class AutoReleaseHandle
    {
        private Timer _timer;

        private bool _released;

        private SpawnHandle _spawnHandle;

        public AutoReleaseHandle(SpawnHandle handle, float releaseTime)
        {
            _spawnHandle = handle;
            _timer = new Timer(releaseTime);
            _timer.TimerTrigger += Release;
            _timer.StartTimer();
        }

        public void Update()
        {
            _timer.Tick();
        }

        public void Reset(SpawnHandle handle, float releaseTime)
        {
            _released = false;
            _spawnHandle = handle;
            _timer = new Timer(releaseTime);
            _timer.TimerTrigger += Release;
            _timer.StartTimer();
        }

        /// <summary>
        /// 是否已经释放
        /// </summary>
        /// <returns></returns>
        public bool IsReleased()
        {
            return _released;
        }

        /// <summary>
        /// 主动释放
        /// </summary>
        public void Release()
        {
            _spawnHandle.Release();
            _released = true;
        }
    }
}