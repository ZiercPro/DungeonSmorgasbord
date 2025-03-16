using NaughtyAttributes.Scripts.Core.DrawerAttributes_SpecialCase;
using UnityEngine;

namespace ZiercCode._DungeonGame.SceneChangeEffect
{
    public class SceneChangeEffect : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        [Button("测试")]
        public void Change()
        {
            animator.SetTrigger("change");
        }
    }
}