using System.Collections.Generic;
using UnityEngine;
using ZiercCode.Core.Utilities;

namespace ZiercCode.DungeonSmorgasbord.Buff
{
    public class BuffEffective : MonoBehaviour
    {
        private List<BuffBaseSo> _buffBases;

        private void OnEnable()
        {
            _buffBases = new List<BuffBaseSo>();
            _buffBases.Clear();
        }

        private void Update()
        {
            if (_buffBases.Count > 0)
            {
                MyMath.ForeachFromLast(_buffBases, buff =>
                {
                    buff.BuffTimeUpdate();
                    if (!buff.Enable)
                    {
                        buff.InActive();
                        _buffBases.Remove(buff);
                    }
                });
            }
        }

        private void OnDisable()
        {
            ClearAllBuff();
        }

        /// <summary>
        /// 清除所有buff
        /// </summary>
        public void ClearAllBuff()
        {
            if (_buffBases.Count > 0)
            {
                foreach (var buff in _buffBases)
                {
                    buff.InActive();
                }
            }
        }

        public void AddBuff(BuffBaseSo buffBaseSo)
        {
            //如果以及存在了相同的buff 看是否可叠加，可叠加则重置持续时间
            foreach (var buff in _buffBases)
            {
                if (buff.buffId == buffBaseSo.buffId)
                {
                    if (buff.addAble)
                    {
                        buff.ReSetDuration();
                    }

                    buffBaseSo.InActive();
                    return;
                }
            }

            //添加新的buff
            _buffBases.Add(buffBaseSo);
            buffBaseSo.Init(this);
        }
    }
}