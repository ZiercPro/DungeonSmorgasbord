using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ZiercCode.Test.StateMachine
{
    public class StateMachine : IStateMachine
    {
        private readonly Dictionary<Type, IState> _states = new Dictionary<Type, IState>();
        private IState _currentState;
        public IState CurrentState => _currentState;

        private Object _owner;
        public object Owner => _owner;

        private bool _isInitialized;

        public void Initialize(Object owner)
        {
            if (!_isInitialized)
            {
                _owner = owner;
                _isInitialized = true;
            }
            else
            {
                Debug.LogWarning("状态机已经初始化!");
            }
        }


        /// <summary>
        /// 添加状态
        /// </summary>
        /// <typeparam name="T">状态类型</typeparam>
        public void AddState<T>() where T : IState
        {
            Type type = typeof(T);
            AddState(type);
        }

        /// <summary>
        /// 添加状态
        /// </summary>
        /// <param name="stateType">状态实例</param>
        public void AddState(Type stateType)
        {
            IState newState = Activator.CreateInstance(stateType) as IState;

            if (newState == null)
            {
                throw new Exception($"无法获取{stateType}为状态实例");
            }

            if (!_states.TryAdd(stateType, newState))
            {
                Debug.LogWarning($"已添加状态{stateType.Name}");
                return;
            }

            newState.OnCreate(this);
        }

        /// <summary>
        /// 初始化状态机
        /// </summary>
        /// <typeparam name="T">起始状态</typeparam>
        public void Run<T>() where T : IState
        {
            Type type = typeof(T);

            Run(type);
        }

        /// <summary>
        /// 初始化状态机
        /// </summary>
        /// <param name="startStateType">起始状态类型</param>
        public void Run(Type startStateType)
        {
            if (_states.TryGetValue(startStateType, out IState state))
            {
                _currentState = state;
                _currentState.OnEnter();
            }
            else
            {
                Debug.LogError($"{startStateType.Name}状态没有添加");
            }
        }

        /// <summary>
        /// 改变状态
        /// </summary>
        /// <typeparam name="T">目标状态</typeparam>
        public void ChangeState<T>() where T : IState
        {
            Type type = typeof(T);
            ChangeState(type);
        }

        /// <summary>
        /// 改变状态
        /// </summary>
        /// <param name="stateType">目标状态类型</param>
        public void ChangeState(Type stateType)
        {
            if (_states.TryGetValue(stateType, out IState state))
            {
                _currentState.OnExit();
                _currentState = state;
                _currentState.OnEnter();
            }
            else
            {
                Debug.LogError($"{stateType.FullName}状态没有添加");
            }
        }
    }
}