using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZiercCode.EventBusSystem
{
    /// <summary>
    /// 事件组 通过组的形式管理事件监听
    /// 用来实现事件的注册与注销
    /// </summary>
    public class EventsGroup
    {
        /// <summary>
        /// 事件监听字典
        /// </summary>
        private readonly Dictionary<Type, List<Action<IEventArgs>>> _eventListeners;

        public EventsGroup()
        {
            _eventListeners = new Dictionary<Type, List<Action<IEventArgs>>>();
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="action">监听方法</param>
        /// <typeparam name="TEvent">事件信息类型</typeparam>
        public void AddListener<TEvent>(Action<IEventArgs> action) where TEvent : IEventArgs
        {
            Type type = typeof(TEvent);
            if (!_eventListeners.ContainsKey(type))
            {
                List<Action<IEventArgs>> newActionList = new();
                _eventListeners.Add(type, newActionList);
            }

            if (!_eventListeners[type].Contains(action))
            {
                _eventListeners[type].Add(action);
                EventBus.AddListener<TEvent>(action);
            }
            else
            {
                Debug.LogWarning($"{action}已经注册{type}事件");
            }
        }

        /// <summary>
        /// 移除事件
        /// </summary>
        /// <param name="action"></param>
        /// <typeparam name="TEvent"></typeparam>
        public void RemoveListener<TEvent>(Action<IEventArgs> action) where TEvent : IEventArgs
        {
            if (_eventListeners.ContainsKey(typeof(TEvent)))
            {
                _eventListeners[typeof(TEvent)].Remove(action);
                EventBus.RemoveListener<TEvent>(action);
            }
        }

        /// <summary>
        /// 移除所有监听
        /// </summary>
        public void RemoveAllListener()
        {
            foreach (KeyValuePair<Type, List<Action<IEventArgs>>> kv in _eventListeners)
            {
                Type type = kv.Key;

                for (int i = 0; i < kv.Value.Count; i++)
                {
                    EventBus.RemoveListener(type, kv.Value[i]);
                }

                kv.Value.Clear();
            }

            _eventListeners.Clear();
        }
    }
}