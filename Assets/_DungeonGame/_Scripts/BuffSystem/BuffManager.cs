using System.Collections.Generic;
using ZiercCode.Utilities;
using Object = UnityEngine.Object;

namespace ZiercCode._DungeonGame._Scripts.BuffSystem
{
    /// <summary>
    /// 管理所有实例的buff效果
    /// </summary>
    public class BuffManager : USingleton<BuffManager>
    {
        private Dictionary<Object, BuffHandler>
            _hanlderBindings = new Dictionary<Object, BuffHandler>(); //维护每一个实例身上的buff处理器

        private void Update()
        {
            foreach (var handler in _hanlderBindings.Values)
            {
                handler.Update();
            }
        }


        public void AddBuff(Object target, BuffBase buff)
        {
            if (!_hanlderBindings.ContainsKey(target))
            {
                BuffHandler handler = new BuffHandler(target);
                _hanlderBindings.Add(target, handler);
            }

            _hanlderBindings[target].AddBuff(buff);
        }

        public void RemoveBuff(Object target, BuffBase buff)
        {
            if (_hanlderBindings.ContainsKey(target))
            {
                _hanlderBindings[target].RemoveBuff(buff);
            }
        }
    }
}