using UnityEngine;

namespace ZiercCode.Runtime.Environment
{
    public class Door : MonoBehaviour
    {
        private SpriteRenderer render;
        private Collider2D cd;
        [SerializeField] private Sprite closedDoor;
        [SerializeField] private Sprite openedDoor;
        private bool isClosed;

        private void Awake()
        {
            render = GetComponentInChildren<SpriteRenderer>();
            cd = GetComponentInChildren<Collider2D>();
            cd.enabled = false;
        }

        public void Open()
        {
            isClosed = false;
            cd.enabled = false;
            render.sprite = openedDoor;
        }

        public void Close()
        {
            isClosed = true;
            cd.enabled = true;
            render.sprite = closedDoor;
        }

        public void Switch()
        {
            if (isClosed)
            {
                render.sprite = openedDoor;
                cd.enabled = false;
            }
            else
            {
                render.sprite = closedDoor;
                cd.enabled = true;
            }

            isClosed = !isClosed;
        }
    }
}