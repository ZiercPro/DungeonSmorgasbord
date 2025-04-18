using UnityEngine;
using ZiercCode.DungeonSmorgasbord.Component;

namespace ZiercCode.DungeonSmorgasbord.Weapon
{
    public class WeaponKnockBack : MonoBehaviour
    {
        /// <summary>
        /// 击退力
        /// </summary>
        [SerializeField] private float force;

        public void DoKnockBack(Collider2D c2d)
        {
            if (c2d.TryGetComponent(out KnockBackFeedBack knockBackFeedBack))
            {
                //todo 逻辑有问题 不过这个脚本也不用了
                knockBackFeedBack.StartBackMove(transform.right, force);
            }
        }
    }
}