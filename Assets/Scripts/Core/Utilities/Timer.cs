using System;
using UnityEngine;

namespace ZiercCode.Core.Utilities
{
    /// <summary>
    /// 一个简单的计时器
    /// </summary>
    public class Timer
    {
        /// <summary>
        /// 计时器触发事件
        /// </summary>
        public event Action TimerTrigger;

        /// <summary>
        /// 开始计时的时间
        /// </summary>
        private float _startTime;

        /// <summary>
        /// 计时持续时间
        /// </summary>
        private float _duration;

        /// <summary>
        /// 计时结束时间
        /// </summary>
        private float _endTime;

        /// <summary>
        /// 计时器是否被启动
        /// </summary>
        private bool _isActive;

        public Timer(float duration)
        {
            _duration = duration;
            _isActive = false;
        }

        /// <summary>
        /// 启动计时器
        /// </summary>
        public void StartTimer()
        {
            _startTime = Time.time;
            _endTime = _startTime + _duration;
            _isActive = true;
        }

        /// <summary>
        /// 暂停计时器
        /// </summary>
        public void StopTimer()
        {
            _isActive = false;
        }

        /// <summary>
        /// 计时循环，在循环方法中调用
        /// </summary>
        public void Tick()
        {
            //如果没有启动 则不会计时
            if (!_isActive) return;
            //一旦到达结束事件，停止计时
            if (Time.time >= _endTime)
            {
                StopTimer();
                TimerTrigger?.Invoke();
            }
        }
        
    }
}