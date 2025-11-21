using UnityEngine;
using ZiercCode._DungeonGame._Scripts.BuffSystem;

namespace ZiercCode.Test.Buff
{
    public class MoveSpeedBuff15Add : EntityBuffBase
    {
        private float _previousSpeed;
        private float _currentSpeed;

        public MoveSpeedBuff15Add()
        {
            BuffTimer = 2.5f;
        }

        public override BuffTypeEnum GetBuffType()
        {
            return BuffTypeEnum.EntityMoveSpeed;
        }

        public override void ApplyBuff()
        {
            Enabled = true;
            _previousSpeed = MyEntity.moveSpeed;
            _currentSpeed = MyEntity.moveSpeed + 15f;
            MyEntity.moveSpeed = _currentSpeed;
        }

        public override void RemoveBuff()
        {
            MyEntity.moveSpeed = _previousSpeed;
        }

        public override void ReAddBuff()
        {
            BuffTimer = 2.5f;
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