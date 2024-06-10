using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ZiercCode.Test.ObjectPool
{
    public static class ZiercPool
    {
        private static readonly Dictionary<string, ObjectPool> Pools =
            new Dictionary<string, ObjectPool>();

        public static void Register(string objName, Object rootObject)
        {
            if (Pools.ContainsKey(objName))
            {
                Debug.LogWarning($"{objName}已经注册物品池");
                return;
            }

            ObjectPool newO = new ObjectPool(objName, rootObject);
            Pools.Add(objName, newO);
        }

        public static Object Get(string objName)
        {
            if (Pools.ContainsKey(objName))
            {
                return Pools[objName].Get();
            }

            Debug.LogWarning($"{objName}还未注册对象池");
            return null;
        }
    }
}