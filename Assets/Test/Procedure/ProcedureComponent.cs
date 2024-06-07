using NaughtyAttributes.Scripts.Core.DrawerAttributes;
using NaughtyAttributes.Scripts.Core.MetaAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using ZiercCode.Test.Base;
using ZiercCode.Test.StateSystem;
using ZiercCode.Test.Utility;

namespace ZiercCode.Test.Procedure
{
    public class ProcedureComponent : ZiercComponent
    {
        private StateMachine _procedureMachine;

        private List<string> _availableTypeNames;

        private List<string> _availableTypeFullNames;

        public List<string> AvailableTypeNames
        {
            get
            {
                if (_availableTypeNames == null) _availableTypeNames = new List<string>();
                else _availableTypeNames.Clear();

                if (_availableTypeFullNames == null) _availableTypeFullNames = new List<string>();
                else _availableTypeFullNames.Clear();

                Assembly main = Assembly.LoadFrom("Library/ScriptAssemblies/Assembly-Csharp.dll");
                Type[] types = main.GetTypes();
                foreach (var type in types)
                {
                    if (ZiercType.GetAbstractTypes(type).Any(i => i == typeof(ProcedureBase)))
                    {
                        _availableTypeNames.Add(type.Name);
                        _availableTypeFullNames.Add(type.FullName);
                    }
                }

                return _availableTypeNames;
            }
        }

        [field: SerializeField, BoxGroup("AvailableProcedures"), Space, Dropdown("AvailableTypeNames")]
        public string LaunchProcedure { get; private set; }

        private void Start()
        {
            _procedureMachine = new StateMachine();
            InitializeProcedureMachine();
            RunProcedureMachine();
        }

        private void Update()
        {
            if (_procedureMachine.CurrentState != null)
                _procedureMachine.CurrentState.OnUpdate();
        }

        private string GetLaunchProcedureFullName()
        {
            for (int i = 0; i < _availableTypeNames.Count; i++)
            {
                if (string.Equals(_availableTypeNames[i], LaunchProcedure))
                {
                    return _availableTypeFullNames[i];
                }
            }

            throw new NullReferenceException($"无法获取初始流程的全名");
        }

        private Type GetLaunchProcedureType()
        {
            string launchProcedureFullName = GetLaunchProcedureFullName();

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
            foreach (var fullName in _availableTypeFullNames)
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