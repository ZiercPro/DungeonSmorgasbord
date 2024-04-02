using System.Collections;
using UnityEngine;

namespace ZiercCode.Runtime.Envionment
{
    /// <summary>
    /// 相机真实追踪的目标 为了实现相机向鼠标方向轻微移动的效果
    /// </summary>
    public class CameraTarget : MonoBehaviour
    {
        [Header("Static")] [Space] [SerializeField]
        private float threshold; //位移阈值

        [SerializeField] private float moveSpeed;

        [Header("Dynamic")] [Space] private Vector2 pointerPos; //鼠标位置
        private Vector2 newPos; //更新后的位置
        private Transform heroTransform; //英雄组件
        public bool Active { get; private set; }

        IEnumerator UpDatePosition()
        {
            while (Active)
            {
                pointerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                newPos = (pointerPos + (Vector2)heroTransform.position) / 2f;

                newPos.x = Mathf.Clamp(newPos.x, -threshold + heroTransform.position.x,
                    threshold + heroTransform.position.x);
                newPos.y = Mathf.Clamp(newPos.y, -threshold + heroTransform.position.y,
                    threshold + heroTransform.position.y);
                transform.position = Vector2.Lerp(transform.position, newPos, moveSpeed);
                yield return null;
            }
        }

        public void Follow()
        {
            if (heroTransform == null)
            {
                Debug.LogWarning("目标不存在");
                return;
            }

            Active = true;
            StartCoroutine(UpDatePosition());
        }

        public void StopFollow()
        {
            Active = false;
            StopCoroutine(UpDatePosition());
        }

        public void SetTarget(Transform tar)
        {
            if (tar == null)
            {
                Debug.LogWarning("要设置的组件为空!");
                return;
            }

            heroTransform = tar;
        }

        public void RemoveTarget()
        {
            heroTransform = null;
        }
    }
}