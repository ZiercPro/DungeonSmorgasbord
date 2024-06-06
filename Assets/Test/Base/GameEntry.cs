using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZiercCode.Test.Base
{
    public static class GameEntry
    {
        private static readonly Dictionary<Type, ZiercComponent> ZiercComponents = new Dictionary<Type, ZiercComponent>();


        public static ZiercComponent GetComponent<T>() where T : ZiercComponent
        {
            Type type = typeof(T);

            if (ZiercComponents.TryGetValue(type, out ZiercComponent component))
            {
                return component;
            }
            else
            {
                Debug.LogError($"没有注册组件{type.FullName}!");
                return null;
            }
        }

        public static void RegisterComponent(ZiercComponent component)
        {
            Type type = component.GetType();

            if (ZiercComponents.ContainsKey(type))
            {
                Debug.LogWarning($"组件{type.FullName}已经注册!");
                return;
            }

            ZiercComponents.Add(type, component);
        }
    }
}