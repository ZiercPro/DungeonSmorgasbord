using NaughtyAttributes;
using System;
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
        /// 发射的武器
        /// </summary>
        [SerializeField] private WeaponBase fireWeapon;

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
            /// 射弹数据
            /// </summary>
            [SerializeField] private WeaponDataSo projectileDataSo;

            /// <summary>
            /// 射弹轨道的transform
            /// 用于确认射弹生成的位置
            /// </summary>
            [SerializeField] private Transform projectileFireTransform;


            /// <summary>
            /// 相对总方向的角度差
            /// </summary>
            [SerializeField] private float fireAngelOffset;

            /// <summary>
            /// 射弹数量
            /// </summary>
            [SerializeField, Tooltip("最终生成多少射弹")] private int projectileNum = 1;

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
            public GameObject ProjectileInstances { get; private set; }


            /// <summary>
            /// 生成射弹
            /// </summary>
            public void CreateProjectile(WeaponBase fireWeapon)
            {
                for (int i = 0; i < projectileNum; i++)
                {
                    //生成新的射弹
                    ProjectileInstances = Instantiate(projectileDataSo.prefab, projectileFireTransform);
                    //初始化新的射弹
                    WeaponProjectile weaponProjectile = ProjectileInstances.GetComponent<WeaponProjectile>();
                    weaponProjectile.Init(fireWeapon.GetWeaponUserBase());
                    //设置方向
                    ProjectileInstances.transform.localRotation = Quaternion.identity;
                    //设置位置
                    ProjectileInstances.transform.localPosition = Vector3.zero;
                    if (!showBeforeFired)
                        ProjectileInstances.SetActive(false);
                }
            }

            /// <summary>
            /// 发射方法
            /// </summary>   
            public void Fire()
            {
                ProjectileInstances.transform.SetParent(null, true);

                if (!showBeforeFired)
                    ProjectileInstances.SetActive(true);
                Vector2 fireDirection = projectileFireTransform.right;
                //考虑插值
                if (fireAngelOffset != 0)
                {
                    float radiusOffset = fireAngelOffset / Mathf.Rad2Deg;
                    fireDirection += new Vector2(Mathf.Cos(radiusOffset), Mathf.Sin(radiusOffset));
                }

                ProjectileInstances.GetComponent<WeaponProjectile>()
                    .Fire(fireDirection, flySpeed);
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
                projectileConfig.CreateProjectile(fireWeapon);
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