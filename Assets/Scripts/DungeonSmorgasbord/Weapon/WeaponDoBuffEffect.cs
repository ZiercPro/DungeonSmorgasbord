using UnityEngine;
using ZiercCode.DungeonSmorgasbord.Buff;

namespace ZiercCode.DungeonSmorgasbord.Weapon
{
    /// <summary>
    /// 武器造成buff效果
    /// </summary>
    public class WeaponDoBuffEffect : MonoBehaviour
    {
        [SerializeField] private WeaponBase weaponBase;
        [SerializeField] private BuffBaseSo buffBaseSo;

        [SerializeField] private bool canEffectSelf;

        public void DoBuffEffect(Collider2D c2D)
        {
            if (c2D.TryGetComponent(out BuffEffective buffEffective))
            {
                if (!canEffectSelf)
                    if (weaponBase.GetWeaponUserBase().GetWeaponUserTransform().GetComponent<BuffEffective>() ==
                        buffEffective)
                        return;
                buffEffective.AddBuff(buffBaseSo);
            }
        }
    }
}