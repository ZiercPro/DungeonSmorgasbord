using UnityEngine;
using ZiercCode.DungeonSmorgasbord.ScriptObject;

namespace ZiercCode.DungeonSmorgasbord.Weapon
{
    /// <summary>
    /// 能够生成射弹的武器组件
    /// </summary>
    public class WeaponCreateProjectile : MonoBehaviour
    {
        /// <summary>
        /// 射弹数据
        /// </summary>
        [SerializeField] private WeaponDataSo projectile;

        /// <summary>
        /// 飞行速度
        /// </summary>
        [SerializeField] private float flySpeed;

        /// <summary>
        /// 射弹资源默认前方向
        /// </summary>
        [SerializeField] private ProjectileAssetDirection defaultForwardDirection = ProjectileAssetDirection.Right;

        /// <summary>
        /// 射弹方向
        /// </summary>
        private enum ProjectileAssetDirection
        {
            Up,
            Down,
            Left,
            Right
        }
        
        
    }
}