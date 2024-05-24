using DG.Tweening;
using System;
using UnityEngine;
using ZiercCode.Core.Utilities;

namespace ZiercCode.DungeonSmorgasbord.Item
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MagneticEffect : MonoBehaviour
    {
        [Header("目标Tag")] [SerializeField] private string targetTag;
        [Header("磁力半径")] [SerializeField] private float radius = 1.5f;

        [Header("磁力曲线")] [SerializeField] private AnimationCurve forceCurve;

        [SerializeField] private Collider2D entityCollider2D;
        [SerializeField] private Rigidbody2D r2D;

        private Transform _targetTransform;
        private bool _getTarget;


        /// <summary>
        /// 检测目标
        /// </summary>
        private void DetectTarget()
        {
            Collider2D[] c2d = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach (Collider2D target in c2d)
            {
                if (target.CompareTag(targetTag))
                {
                    _targetTransform = target.transform;
                    _getTarget = true;
                    entityCollider2D.enabled = false;
                }
            }
        }

        /// <summary>
        /// 根据曲线获取磁力
        /// </summary>
        /// <param name="distance">两极距离</param>
        /// <returns></returns>
        private float GetForceByCurve(float distance)
        {
            return forceCurve.Evaluate(distance);
        }

        /// <summary>
        /// 磁力
        /// </summary>
        private void Magnetize()
        {
            Vector3 targetPosition = _targetTransform.position;
            Vector3 myPosition = transform.position;
            bool isInRadius = MyMath.CompareDistanceWithRange(myPosition, targetPosition, radius);
            if (!isInRadius) return;
            Vector2 forceDir = (targetPosition - myPosition).normalized;

            float distance = Vector3.Distance(myPosition, targetPosition);
            float distanceRate = distance / radius;

            r2D.AddForce(forceDir * GetForceByCurve(distanceRate), ForceMode2D.Force);
        }

        private void FixedUpdate()
        {
            if (_getTarget)
                Magnetize();
            else
                DetectTarget();
        }
    }
}