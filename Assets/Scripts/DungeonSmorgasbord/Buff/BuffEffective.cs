using System.Collections.Generic;
using UnityEngine;
using ZiercCode.Core.Extend;

namespace ZiercCode.DungeonSmorgasbord.Buff
{
    public class BuffEffective : MonoBehaviour
    {
        private List<BuffBaseSo> _buffBases;

        private void Awake()
        {
            _buffBases = new List<BuffBaseSo>();
        }

        private void Update()
        {
            if (_buffBases.Count > 0)
            {
                MyMath.ForeachChangeListAvailable(_buffBases, buff =>
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

        public void AddBuff(BuffBaseSo buffBaseSo)
        {
            //如果以及存在了相同的buff 看是否可叠加，可叠加则重置持续时间
            foreach (var buff in _buffBases)
            {
                if (buff.buffId == buffBaseSo.buffId)
                {
                    if (buff.addAble)
                        buff.ReSetDuration();
                    return;
                }
            }


            BuffBaseSo buffBaseSoIns = Instantiate(buffBaseSo);
            _buffBases.Add(buffBaseSoIns);
            buffBaseSoIns.Init(this);
            buffBaseSoIns.Active();
        }
    }
}