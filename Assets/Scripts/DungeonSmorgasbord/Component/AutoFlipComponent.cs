using UnityEngine;

namespace ZiercCode.DungeonSmorgasbord.Component
{
    public class AutoFlipComponent : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        [Tooltip("材质是否默认面向右边")] [SerializeField]
        private bool defaultRight = true;

        public bool IsFacingRight { get; private set; }

        private void Start()
        {
            IsFacingRight = defaultRight;
        }

        private void Flip()
        {
            IsFacingRight = !IsFacingRight;
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        public void FaceTo(Vector2 viewPos)
        {
            float faceX = (viewPos - (Vector2)transform.position).x;
            if (faceX < 0 && IsFacingRight)
            {
                Flip();
            }
            else if (faceX > 0 && !IsFacingRight)
            {
                Flip();
            }
        }
    }
}