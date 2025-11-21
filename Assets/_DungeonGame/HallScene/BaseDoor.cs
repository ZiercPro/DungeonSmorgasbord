using UnityEngine;
using UnityEngine.Events;
using ZiercCode.Utilities;

//门基类
namespace ZiercCode._DungeonGame.HallScene
{
    public abstract class BaseDoor : MonoBehaviour
    {
        [SerializeField]
        private Vector2 collisionBox; //碰撞检测范围

        [SerializeField]
        protected LayerMask targetLayer;

        protected RangeDetector RangeDetector = new(5);
        protected IOpenClose IOpenClose;
        protected bool IsPlayerEnter;

        [HideInInspector]
        public UnityEvent onPlayerEnter = new();

        protected virtual void Awake()
        {
            IOpenClose = GetComponent<IOpenClose>();
        }

        protected virtual void FixedUpdate()
        {
            DetectCollision();
        }

        //检测玩家是否触碰门
        protected virtual void DetectCollision()
        {
            if (!IOpenClose.IsClosed && !IsPlayerEnter)
            {
                if (RangeDetector.DetectInBoxByLayer(targetLayer, transform.position, collisionBox))
                {
                    IsPlayerEnter = true;
                    onPlayerEnter?.Invoke();
                }
            }
            else if (IsPlayerEnter && !RangeDetector.DetectInBoxByLayer(targetLayer, transform.position, collisionBox))
            {
                IsPlayerEnter = false;
            }
        }

#if UNITY_EDITOR
        //debug 绘制碰撞
        protected virtual void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, collisionBox);
        }
#endif
    }
}