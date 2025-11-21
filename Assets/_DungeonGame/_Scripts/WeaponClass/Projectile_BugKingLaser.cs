using UnityEngine;
using ZiercCode._DungeonGame._Scripts.AttackSystem;

namespace ZiercCode._DungeonGame._Scripts.WeaponClass
{
    public class Projectile_BugKingLaser : LaserProjectile
    {
        protected override void CheckHit()
        {
            if (RangeDetector.DetectInBoxByLayer(myWeapon.myHolder.targetFaction,
                    myTransform.position + (myTransform.right * CurrentCollisionBox.x / 2f),
                    CurrentCollisionBox, myTransform.rotation.eulerAngles.z))
            {
                Collider2D[] hit = RangeDetector.GetColliders();
                for (int i = 0; i < hit.Length; i++)
                {
                    if (hit[i] && hit[i].TryGetComponent(out IAttackAble attackAble))
                    {
                        DoAttack(attackAble);
                    }
                }
            }
        }
    }
}