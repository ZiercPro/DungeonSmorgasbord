using UnityEngine;
using ZiercCode.DungeonSmorgasbord.Component;

namespace ZiercCode._DungeonGame._Scripts.WeaponClass
{
    /// <summary>
    /// 有射弹实体和飞行轨迹的射弹
    /// </summary>
    public abstract class Projectile : BaseProjectile
    {
        [SerializeField]
        protected SpriteRenderer mySpriteRenderer;

        protected AutoFlipComponent HolderAutoFlipComponent;

        /// <summary>
        /// 当前移动的距离
        /// </summary>
        [HideInInspector]
        public float currentMoveDistance;

        public override void Init(BaseWeapon myWeapon)
        {
            base.Init(myWeapon);
            SyncFlip();
            currentMoveDistance = 0f;
        }

        protected override void PauseUpdate()
        {
            base.PauseUpdate();
            UpdateDistance();
        }

        private void UpdateDistance()
        {
            currentMoveDistance += myWeapon.projectileSpeed * Time.deltaTime;
        }

        /// <summary>
        /// 同步射弹翻转状态
        /// </summary>
        protected virtual void SyncFlip()
        {
            if (!HolderAutoFlipComponent)
            {
                HolderAutoFlipComponent = myWeapon.myHolder.GetComponent<AutoFlipComponent>();
            }
        }
    }
}