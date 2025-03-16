using UnityEngine;

namespace ZiercCode
{
    public class FixedShadow : MonoBehaviour //预制影子
    {
        [SerializeField] private SpriteRenderer caster;
        [SerializeField] private SpriteRenderer shadow;

        private void LateUpdate()
        {
            shadow.sortingLayerName = caster.sortingLayerName;
            shadow.sortingOrder = caster.sortingOrder - 1;
        }
    }
}