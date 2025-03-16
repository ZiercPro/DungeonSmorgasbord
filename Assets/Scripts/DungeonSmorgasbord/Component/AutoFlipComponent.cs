using UnityEngine;

namespace ZiercCode.DungeonSmorgasbord.Component
{
    public class AutoFlipComponent : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        [SerializeField] private bool isFacingRight = true;

        public bool IsFacingRight => isFacingRight;

        private void Flip()
        {
            isFacingRight = !isFacingRight;
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        public void FaceTo(Vector2 viewPos)
        {
            float faceX = (viewPos - (Vector2)transform.position).x;
            if (faceX < 0 && isFacingRight)
            {
                Flip();
            }
            else if (faceX > 0 && !isFacingRight)
            {
                Flip();
            }
        }
    }
}