using System;
using System.Collections.Generic;

namespace ZiercCode.Event
{
    public static class EventBus
    {
        /// <summary>
        /// 事件id监听字典
        /// </summary>
        private static Dictionary<int, LinkedList<Action<IEventArgs>>> _eventListeners =
            new Dictionary<int, LinkedList<Action<IEventArgs>>>();

        /// <summary>
        /// 添加监听
        /// </summary>
        /// <param name="action">监听方法</param>
        /// <typeparam name="TEvent">事件信息类型</typeparam>
        public static void AddListener<TEvent>(Action<IEventArgs> action) where TEvent : IEventArgs
        {
            Type type = typeof(TEvent);
            AddListener(type, action);
        }

        /// <summary>
        /// 添加监听
        /// </summary>
        /// <param name="action">监听方法</param>
        /// <param name="eventType">事件信息类型</param>
        public static void AddListener(Type eventType, Action<IEventArgs> action)
        {
            int eventId = eventType.GetHashCode();
            if (!_eventListeners.ContainsKey(eventId))
            {
                LinkedList<Action<IEventArgs>> newList = new LinkedList<Action<IEventArgs>>();
                _eventListeners.Add(eventId, newList);
            }

            if (!_eventListeners[eventId].Contains(action))
            {
                _eventListeners[eventId].AddLast(action);
            }
        }

        /// <summary>
        /// 移除事件监听
        /// </summary>
        /// <param name="action">事件监听</param>
        /// <typeparam name="TEvent">事件信息类型</typeparam>
        public static void RemoveListener<TEvent>(Action<IEventArgs> action) where TEvent : IEventArgs
        {
            Type type = typeof(TEvent);
            RemoveListener(type, action);
        }


        /// <summary>
        /// 移除事件监听
        /// </summary>
        /// <param name="eventType">事件信息类型</param>
        /// <param name="action">事件监听</param>
        public static void RemoveListener(Type eventType, Action<IEventArgs> action)
        {
            int eventId = eventType.GetHashCode();
            if (_eventListeners.ContainsKey(eventId))
            {
                _eventListeners[eventId].Remove(action);
            }
        }

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <param name="eventArgs">事件信息</param>
        public static void Invoke(IEventArgs eventArgs)
        {
            Type type = eventArgs.GetType();
            int eventId = type.GetHashCode();
            if (_eventListeners.ContainsKey(eventId))
            {
                LinkedList<Action<IEventArgs>> linkedList = _eventListeners[eventId];

                if (linkedList.Count > 0)
                {
                    var currentNode = linkedList.Last;
                    while (currentNode != null)
                    {
                        currentNode.Value.Invoke(eventArgs);
                        currentNode = currentNode.Previous;
                    }
                }
            }
        }

        /// <summary>
        /// 清除所有事件监听
        /// </summary>
        public static void Clear()
        {
            foreach (var kv in _eventListeners)
            {
                kv.Value.Clear();
            }

            _eventListeners.Clear();
        }
    }
}