using UnityEngine;
using ZiercCode._DungeonGame._Scripts.AttackSystem;
using ZiercCode._DungeonGame._Scripts.EventClasses;
using ZiercCode._DungeonGame._Scripts.Juice;
using ZiercCode.DungeonSmorgasbord.Component;
using ZiercCode.EventBusSystem;
using ZiercCode.FakeHeight;
using ZiercCode.Utilities;

namespace ZiercCode._DungeonGame.TestRoom.TestStake
{
    /// <summary>
    /// 测试木桩
    /// </summary>
    public class TestStake : MonoBehaviour, IAttackAble
    {
        [SerializeField]
        private FlashFeedBack flashFeedBack;

        [SerializeField]
        private HitShake hitShake;

        private EventsGroup _eventsGroup = new();

        private void OnEnable()
        {
            _eventsGroup.AddListener<AttackEvent.WeaponAttack>(GetWeaponAttack);
        }

        private void OnDisable()
        {
            _eventsGroup.RemoveAllListener();
        }

        private void GetWeaponAttack(IEventArgs args)
        {
            if (args is AttackEvent.WeaponAttack weaponAttack)
            {
                if (weaponAttack.Target == (IAttackAble)this)
                {
                    flashFeedBack.Flash();
                    if (weaponAttack.Projectile.TryGetComponent(out FakeHeightTransform fakeHeight))
                    {
                        hitShake.DoShake(fakeHeight.groundVelocity.normalized);
                    }
                }
            }
        }

        [field: SerializeField]
        public float MaxHealth { get; set; }

        [field: SerializeField]
        public float CurrentHealth { get; set; }

        [field: SerializeField]
        public float Armor { get; set; }

        [field: SerializeField]
        public float DamageReduction { get; set; }

        [field: SerializeField]
        public EditableDictionary<DamageType, float> ElementResistanceTable { get; set; }

        public LayerMask MyFaction => gameObject.layer;
        public Transform Transform => transform;
    }
}