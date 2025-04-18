using UnityEngine;
using ZiercCode._DungeonGame._Scripts.EventClasses;
using ZiercCode.EventBusSystem;
using ZiercCode.FakeHeight;
using ZiercCode.ObjectPool;
using Random = UnityEngine.Random;

namespace ZiercCode._DungeonGame._Scripts.WeaponComponent
{
    /// <summary>
    /// 枪械抛壳效果组件
    /// </summary>
    public class GunBulletShellComponent : BaseWeaponComponent
    {
        [Header("弹壳")]
        [SerializeField]
        private GameObject shellCase; //弹壳

        [SerializeField]
        private Transform shellCasePoint; //抛壳点

        [SerializeField]
        private Vector2 shellGroundVRange; //抛壳水平速度范围

        [SerializeField]
        private Vector2 shellVerticalVRange; //抛壳垂直速度范围

        [SerializeField]
        private Vector2 shellRotateVRange; //抛壳旋转速度范围

        [SerializeField]
        private string shellPoolName;

        [SerializeField]
        private int shellPoolMinSize;

        [SerializeField]
        private int shellPoolMaxSize;

        protected override void Awake()
        {
            base.Awake();
            PoolManager.Instance.Register(shellPoolName, shellCase, shellPoolMinSize, shellPoolMaxSize);
        }

        protected void OnEnable()
        {
            EventsGroup.AddListener<WeaponEvent.WeaponFired>(OnFire);
        }

        protected void OnDestroy()
        {
            EventsGroup.RemoveAllListener();
            PoolManager.Instance.Dispose(shellPoolName);
        }

        public void OnFire(IEventArgs args)
        {
            if (args is WeaponEvent.WeaponFired weaponFire)
            {
                if (weaponFire.Weapon == MyWeapon)
                {
                    SpawnShellCase();
                }
            }
        }


        //生成弹壳
        public void SpawnShellCase()
        {
            GameObject shell = (GameObject)PoolManager.Instance.Get(shellPoolName);
            shell.SetActive(true);
            shell.transform.position = shellCasePoint.position;
            shell.transform.rotation = Quaternion.identity;

            shell.GetComponent<FakeHeightTransform>()
                .Init(Random.insideUnitCircle * Random.Range(shellGroundVRange.x, shellGroundVRange.y),
                    Random.Range(shellVerticalVRange.x, shellVerticalVRange.y),
                    true, Random.Range(shellRotateVRange.x, shellRotateVRange.y));
        }
    }
}