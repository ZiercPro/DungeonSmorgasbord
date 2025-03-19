using UnityEngine;
using ZiercCode._DungeonGame.Juice;
using ZiercCode.DungeonSmorgasbord.Component;
using ZiercCode.DungeonSmorgasbord.Damage;

namespace ZiercCode._DungeonGame.Weapon.TestStake
{
    public class TestStake : MonoBehaviour, IHitAble, IDamageable
    {
        [SerializeField] private FlashFeedBack flashFeedBack;
        [SerializeField] private HitShake hitShake;
        // [SerializeField] private KnockBackFeedBack knockBackFeedBack;

        public void GetHit(Vector2 hitDir)
        {
            flashFeedBack.Flash();
            hitShake.DoShake(hitDir);
            //  knockBackFeedBack.StartBackMove(hit, 5f);
        }

        public void TakeDamage(DamageInfo info)
        {
            TextPopup.Instance.InitPopupText(info.owner.position, Color.red, info.damageAmount);
            //   Debug.Log(info.damageAmount);
        }
    }
}