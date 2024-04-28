using System;
using System.Collections;
using UnityEngine;

namespace ZiercCode.DungeonSmorgasbord.Component
{
    public class FlashFeedBack : MonoBehaviour
    {
        private Material DefaultMaterial; //默认
        
        [SerializeField] private Material flashMaterial; //闪烁材质 
        [SerializeField] private SpriteRenderer characterS;

        [SerializeField] private float maintainTime = 0.1f;

        private void Awake()
        {
            DefaultMaterial = characterS.material;
        }

        public void Flash()
        {
            characterS.material = flashMaterial;
            StartCoroutine(FlashTimer());
        }

        IEnumerator FlashTimer()
        {
            yield return new WaitForSeconds(maintainTime);
            characterS.material = DefaultMaterial;
        }
    }
}