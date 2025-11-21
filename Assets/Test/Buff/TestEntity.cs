using UnityEngine;
using ZiercCode._DungeonGame._Scripts.BuffSystem;
using ZiercCode._DungeonGame._Scripts.EntityClasses;

namespace ZiercCode.Test.Buff
{
    public class TestEntity : Entity
    {
        protected override void DeathCheck()
        {
        }

        public BuffHandler _buffHandler;

        protected void Awake()
        {
            _buffHandler = new BuffHandler(this);
        }

        protected override void Start()
        {
            base.Start();
            _buffHandler.AddBuff(new MoveSpeedBuff15Add());
            _buffHandler.AddBuff(new MoveSpeedBuff15Percent());
        }

        protected override void PauseUpdate()
        {
            base.PauseUpdate();
            _buffHandler.Update();

            Debug.Log(moveSpeed);
        }
    }
}