using NaughtyAttributes;
using NaughtyAttributes.Scripts.Core.DrawerAttributes;
using System;
using UnityEngine;
using UnityEngine.Pool;
using ZiercCode.Core.Pool;
using ZiercCode.DungeonSmorgasbord.ScriptObject;

namespace ZiercCode.DungeonSmorgasbord.Weapon
{
    /// <summary>
    /// 能够生成射弹的武器组件
    /// </summary>
    public class WeaponCreateProjectile : MonoBehaviour
    {
        /// <summary>
        /// 发射的武器
        /// </summary>
        [SerializeField] private WeaponBase fireWeapon;

        /// <summary>
        /// 对象生成器
        /// </summary>
        [SerializeField] private PoolObjectSpawner spawner;

        /// <summary>
        /// 射弹总轨道
        /// 确认发射总方向
        /// </summary>
        [SerializeField] private Transform projectileAimParent;


        /// <summary>
        /// 射弹配置数组
        /// </summary>
        [SerializeField, AllowNesting] private ProjectileConfig[] projectileConfigs;


        /// <summary>
        /// 射弹配置
        /// </summary>
        [Serializable]
        private class ProjectileConfig
        {
            /// <summary>
            /// 射弹对象池数据
            /// </summary>
            [SerializeField] private PoolObjectSo projectilePoolObjectSo;

            /// <summary>
            /// 射弹轨道的transform
            /// 用于确认射弹生成的位置
            /// </summary>
            [SerializeField] private Transform projectilePosition;


            /// <summary>
            /// 相对总方向的角度差
            /// </summary>
            [SerializeField, Tooltip("相对瞄准方向的角度差值")]
            private float fireAngelOffset;

            /// <summary>
            /// 没有发射时显示多少个射弹
            /// </summary>
            [SerializeField, Tooltip("未发射前，是否显示")] private bool showBeforeFired;


            /// <summary>
            /// 飞行速度
            /// </summary>
            [SerializeField] private float flySpeed;


            /// <summary>
            /// 射弹实例
            /// </summary>
            public GameObject ProjectileInstance { get; private set; }


            /// <summary>
            /// 生成射弹
            /// </summary>
            public void CreateProjectile(WeaponBase fireWeapon, PoolObjectSpawner spawner)
            {
                SpawnHandle handle =
                    spawner.SpawnPoolObjectWithAutoRelease(projectilePoolObjectSo, projectilePosition,
                        Quaternion.identity, 5f);
                // 获取新的射弹实例
                ProjectileInstance = handle.GetObject();
                //初始化新的射弹
                WeaponProjectile weaponProjectile = ProjectileInstance.GetComponent<WeaponProjectile>();
                weaponProjectile.Init(fireWeapon.GetWeaponUserBase());

                //设置方向
                ProjectileInstance.transform.localEulerAngles = new Vector3(0, 0,
                    ProjectileInstance.transform.localEulerAngles.z + fireAngelOffset);
            }

            /// <summary>
            /// 发射方法
            /// </summary>   
            public void Fire()
            {
                ProjectileInstance.transform.SetParent(null, true);

                if (!showBeforeFired)
                    ProjectileInstance.SetActive(true);

                ProjectileInstance.GetComponent<WeaponProjectile>()
                    .Fire(flySpeed);
            }
        }


        /// <summary>
        /// 更新总轨道瞄准的方向
        /// </summary>
        /// <param name="viewPosition"></param>
        public void UpdateAimDirection(Vector2 viewPosition)
        {
            //瞄准的方向
            Vector2 viewDirection = viewPosition - (Vector2)projectileAimParent.position;
            //设置方向
            projectileAimParent.right = viewDirection;
        }

        //创建射弹
        public void CreateProjectiles()
        {
            foreach (var projectileConfig in projectileConfigs)
            {
                projectileConfig.CreateProjectile(fireWeapon, spawner);
            }
        }


        /// <summary>
        /// 发射射弹
        /// </summary>
        public void Fire()
        {
            foreach (var projectileConfig in projectileConfigs)
            {
                projectileConfig.Fire();
            }
        }
    }
}