using System;
using UnityEngine;
using ZiercCode.Test.Base;

namespace ZiercCode.Test.Procedure
{
    public class ProcedureComponent : ZiercComponent
    {
        private StateMachine.StateMachine _procedureMachine;

        /// <summary>
        /// 可使用流程类型全名
        /// </summary>
        [SerializeField] private string[] availableTypeFullNames = null;

        /// <summary>
        /// 初始流程序号
        /// </summary>
        [SerializeField] private string launchProcedureFullName;

        private void Start()
        {
            _procedureMachine = new StateMachine.StateMachine();
            InitializeProcedureMachine();
            RunProcedureMachine();
        }

        private void Update()
        {
            if (_procedureMachine.CurrentState != null)
                _procedureMachine.CurrentState.OnUpdate();
        }


        private Type GetLaunchProcedureType()
        {
            if (string.IsNullOrEmpty(launchProcedureFullName))
            {
                throw new Exception("没有选择启动流程");
            }

            Type type = Type.GetType(launchProcedureFullName);
            if (type == null)
            {
                throw new NullReferenceException($"无法获取初始流程类型");
            }

            return type;
        }

        private void InitializeProcedureMachine()
        {
            _procedureMachine.Initialize(this);
        }

        private void RunProcedureMachine()
        {
            foreach (var fullName in availableTypeFullNames)
            {
                Type newType = Type.GetType(fullName);

                if (newType == null)
                {
                    throw new Exception($"无法获取类型{fullName}");
                }

                _procedureMachine.AddState(newType);
            }

            _procedureMachine.Run(GetLaunchProcedureType());
        }
    }
}