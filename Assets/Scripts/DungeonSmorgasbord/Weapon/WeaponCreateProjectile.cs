using System.Collections.Generic;
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
        [SerializeField] private WeaponDataSo projectileDataSo;

        /// <summary>
        /// 发射的武器
        /// </summary>
        [SerializeField] private WeaponBase fireWeapon;

        /// <summary>
        /// 射弹数量
        /// </summary>
        [SerializeField, Tooltip("最终生成多少射弹")] private int projectileNum;

        /// <summary>
        /// 没有发射时显示多少个射弹
        /// </summary>
        [SerializeField, Tooltip("未发射前，显示多少射弹")]
        private int projectileNumUnFired;

        /// <summary>
        /// 飞行速度
        /// </summary>
        [SerializeField] private float flySpeed;

        /// <summary>
        /// 射弹资源默认前方向
        /// </summary>
        [SerializeField] private AssetDirection projectileDefaultHeadDirection = AssetDirection.Right;

        /// <summary>
        /// 武器资源默认前方向
        /// </summary>
        [SerializeField] private AssetDirection weaponDefaultHeadDirection = AssetDirection.Right;

        /// <summary>
        /// 射弹方向
        /// </summary>
        private enum AssetDirection
        {
            Up,
            Down,
            Left,
            Right
        }

        /// <summary>
        /// 射弹实例
        /// </summary>
        private List<GameObject> _projectileInstances;

        private void Awake()
        {
            _projectileInstances = new List<GameObject>();
        }

        /// <summary>
        /// 获取射弹初始角度
        /// </summary>
        /// <param name="assetDirection">资源默认的方向</param>
        /// <returns></returns>
        private Vector3 GetProjectileDirection(AssetDirection assetDirection)
        {
            switch (assetDirection)
            {
                case AssetDirection.Up:
                    return new Vector3(0f, 0f, 270f);
                case AssetDirection.Down:
                    return new Vector3(0f, 0f, 90);
                case AssetDirection.Left:
                    return new Vector3(0f, 0f, 180f);
                case AssetDirection.Right:
                    return new Vector3(0f, 0f, 0f);
                default:
                    return new Vector3(0f, 0f, 0f);
            }
        }

        /// <summary>
        /// 获取发射方向
        /// </summary>
        /// <param name="assetDirection">资源默认的方向</param>
        /// <returns></returns>
        private Vector3 GetFireDirection(AssetDirection assetDirection)
        {
            switch (assetDirection)
            {
                case AssetDirection.Up:
                    return transform.up;
                case AssetDirection.Down:
                    return -transform.up;
                case AssetDirection.Right:
                    return transform.right;
                case AssetDirection.Left:
                    return -transform.right;
                default:
                    return transform.right;
            }
        }

        /// <summary>
        /// 生成射弹
        /// </summary>
        public void CreateProjectile()
        {
            if (_projectileInstances.Count > 0)
                _projectileInstances.Clear();
            //获取默认方向
            Vector3 direction = GetProjectileDirection(projectileDefaultHeadDirection);
            for (int i = 0; i < projectileNum; i++)
            {
                //生成新的射弹
                GameObject newP = Instantiate(projectileDataSo.prefab, transform);
                //初始化新的射弹
                newP.GetComponent<WeaponProjectile>().Init(fireWeapon.GetWeaponUserBase());
                //设置方向
                newP.transform.localRotation = Quaternion.Euler(direction);
                newP.transform.localPosition = Vector3.zero;
                //添加到实例链表
                _projectileInstances.Add(newP);
                //如果已经生成了n个 其他的射弹都隐藏
                if (i > projectileNumUnFired - 1)
                {
                    newP.SetActive(false);
                }
            }
        }

        /// <summary>
        /// 发射射弹
        /// </summary>
        public void Fire()
        {
            foreach (var projectile in _projectileInstances)
            {
                projectile.transform.SetParent(null, true);
                projectile.GetComponent<WeaponProjectile>().Fire(GetFireDirection(weaponDefaultHeadDirection));
            }
        }
    }
}