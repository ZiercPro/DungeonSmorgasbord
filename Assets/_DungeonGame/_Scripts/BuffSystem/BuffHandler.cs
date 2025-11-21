using System.Collections.Generic;
using UnityEngine;

namespace ZiercCode._DungeonGame._Scripts.BuffSystem
{
    /// <summary>
    /// 管理实体的buff
    /// </summary>
    public class BuffHandler
    {
        private Dictionary<BuffTypeEnum, LinkedList<BuffBase>> _buffBindings;
        private Object _holder;

        public BuffHandler(Object holder)
        {
            _buffBindings = new Dictionary<BuffTypeEnum, LinkedList<BuffBase>>();
            _holder = holder;
        }

        public void AddBuff(BuffBase buff)
        {
            buff.BindHolder(_holder);
            if (!_buffBindings.ContainsKey(buff.GetBuffType())) //是否包含修改数值类型的buff链表
            {
                LinkedList<BuffBase> buffList = new LinkedList<BuffBase>();
                _buffBindings.Add(buff.GetBuffType(), buffList);
            }

            //是否已经添加该buff
            LinkedListNode<BuffBase> targetNode = _buffBindings[buff.GetBuffType()].Find(buff);

            if (targetNode != null)
            {
                targetNode.Value.ReAddBuff();
                RefreshBuffListFrom(targetNode.Next);
                return;
            }

            _buffBindings[buff.GetBuffType()].AddLast(buff);
            buff.ApplyBuff();
        }

        public void RemoveBuff(BuffBase buff)
        {
            if (_buffBindings.ContainsKey(buff.GetBuffType()))
            {
                LinkedListNode<BuffBase> currentBuff = _buffBindings[buff.GetBuffType()].Find(buff);
                if (currentBuff != null)
                {
                    currentBuff.Value.RemoveBuff();
                    LinkedListNode<BuffBase> nextBuff = currentBuff.Next;
                    _buffBindings[buff.GetBuffType()].Remove(currentBuff);
                    currentBuff = nextBuff;

                    RefreshBuffListFrom(currentBuff);
                }
            }
        }

        public void Update()
        {
            foreach (var kv in _buffBindings)
            {
                LinkedListNode<BuffBase> currentBuff = kv.Value.First; //从后往前 越前越晚添加
                while (currentBuff != null)
                {
                    currentBuff.Value.Update();

                    if (!currentBuff.Value.IsActive()) break;

                    currentBuff = currentBuff.Next;
                }

                //如果当前节点不是空 则说明有节点需要被移除了，同时后面的节点将重新计算值
                if (currentBuff != null)
                {
                    currentBuff.Value.RemoveBuff();
                    LinkedListNode<BuffBase> nextBuff = currentBuff.Next;
                    kv.Value.Remove(currentBuff);
                    currentBuff = nextBuff;

                    RefreshBuffListFrom(currentBuff);
                }
            }
        }

        //从某个节点开始应用buff链表
        private void RefreshBuffListFrom(LinkedListNode<BuffBase> targetNode)
        {
            while (targetNode != null)
            {
                targetNode.Value.ApplyBuff();
                targetNode.Value.Update();
                targetNode = targetNode.Next;
            }
        }
    }
}