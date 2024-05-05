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

        private Coroutine _flashCoroutine;

        private void Awake()
        {
            DefaultMaterial = characterS.material;
        }

        private void OnDisable()
        {
            if (_flashCoroutine != null)
                StopCoroutine(_flashCoroutine);
            characterS.material = DefaultMaterial;
        }

        public void Flash()
        {
            if (!gameObject.activeInHierarchy) return;
            characterS.material = flashMaterial;
            _flashCoroutine = StartCoroutine(FlashTimer());
        }

        IEnumerator FlashTimer()
        {
            yield return new WaitForSeconds(maintainTime);
            characterS.material = DefaultMaterial;
        }
    }
}