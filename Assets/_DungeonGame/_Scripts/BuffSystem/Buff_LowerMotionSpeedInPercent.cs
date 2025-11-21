using UnityEngine;

namespace ZiercCode._DungeonGame._Scripts.BuffSystem
{
    /// <summary>
    /// 百分比降低实体整体速率
    /// </summary>
    public class Buff_LowerMotionSpeedInPercent : EntityBuffBase
    {
        private float _previousValue;
        private float _currentValue;

        private float _lowerRate;

        public Buff_LowerMotionSpeedInPercent(float time, float lowerRate)
        {
            BuffTime = time;
            _lowerRate = lowerRate;
        }

        public override BuffTypeEnum GetBuffType()
        {
            return BuffTypeEnum.EntityMotionSpeed;
        }

        public override void ApplyBuff()
        {
            Enabled = true;

            _previousValue = MyEntity.motionSpeed;
            _currentValue = _previousValue * (1 - _lowerRate);
            MyEntity.motionSpeed = _currentValue;
        }

        public override void RemoveBuff()
        {
            MyEntity.motionSpeed = _previousValue;
        }

        public override void ReAddBuff()
        {
            ResetTimer();
        }

        public override void Update()
        {
            if (Enabled)
            {
                BuffTimer -= Time.deltaTime;
                if (BuffTimer <= 0f)
                {
                    Enabled = false;
                }
            }
        }
    }
}