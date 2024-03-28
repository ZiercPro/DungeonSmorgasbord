using UnityEngine;

namespace Runtime.Component.Base
{
    public class FlipController : MonoBehaviour
    {
        public bool isFaceingRight { get; private set; }

        private void Start()
        {
            isFaceingRight = true;
        }

        private void Flip()
        {
            isFaceingRight = !isFaceingRight;
            transform.Rotate(new Vector3(0, 180, 0));
        }

        public void FaceTo(Vector2 viewPos)
        {
            float faceX = (viewPos - (Vector2)transform.position).x;
            if (faceX < 0 && isFaceingRight)
            {
                Flip();
            }
            else if (faceX > 0 && !isFaceingRight)
            {
                Flip();
            }
        }
    }
}