using UnityEngine;

namespace ZiercCode.Utilities
{
    //检测器
    public class RangeDetector
    {
        private int _maxDetectNum; //检测碰撞体最大数量
        private Collider2D[] _detectColliders;
        private RaycastHit2D[] _detectRay;

        public RangeDetector(int maxDetectNum)
        {
            _maxDetectNum = maxDetectNum;
            _detectColliders = new Collider2D[_maxDetectNum];
            _detectRay = new RaycastHit2D[_maxDetectNum];
        }

        public Collider2D[] GetColliders()
        {
            return _detectColliders;
        }

        public RaycastHit2D[] GetRayHits()
        {
            return _detectRay;
        }

        public bool DetectInBox(Vector2 point, Vector2 boxRange, float angle = 0f)
        {
            ClearColliderResults();

            int detectCount = Physics2D.OverlapBoxNonAlloc(point, boxRange, angle, _detectColliders);

            return detectCount > 0;
        }

        //检测是否碰到碰撞体 圆形范围
        public bool DetectInCircle(Vector2 point, float radius)
        {
            ClearColliderResults();

            int detectCount = Physics2D.OverlapCircleNonAlloc(point, radius, _detectColliders);

            return detectCount > 0;
        }

        //通过tag判断是否为目标，如果在范围内则返回true否则返回false 圆形范围
        public bool DetectInCircleByTag(string targetTag, Vector2 point, float radius)
        {
            ClearColliderResults();

            int detectedNum = Physics2D.OverlapCircleNonAlloc(point, radius, _detectColliders);

            return CompareTag(targetTag, detectedNum);
        }


        //通过tag判断是否为目标 方形范围检测 
        public bool DetectInBoxByTag(string targetTag, Vector2 point, Vector2 boxRange, float angle = 0f)
        {
            ClearColliderResults();

            int detectedNum = Physics2D.OverlapBoxNonAlloc(point, boxRange, angle, _detectColliders);

            return CompareTag(targetTag, detectedNum);
        }

        //通过Layer判断是否为目标，如果在范围内则返回true否则返回false 圆形范围
        public bool DetectInCircleByLayer(LayerMask targetLayer, Vector2 point, float radius)
        {
            ClearColliderResults();

            int detectedNum = Physics2D.OverlapCircleNonAlloc(point, radius, _detectColliders, targetLayer);

            return detectedNum > 0;
        }


        //通过Layer判断是否为目标 方形范围检测 
        public bool DetectInBoxByLayer(LayerMask targetLayer, Vector2 point, Vector2 boxRange, float angle = 0f)
        {
            ClearColliderResults();

            int detectedNum = Physics2D.OverlapBoxNonAlloc(point, boxRange, angle, _detectColliders, targetLayer);

            return detectedNum > 0;
        }

        public bool DetectInRay(Vector2 origin, Vector2 dir, float distance)
        {
            ClearRayHitResults();

            int detectedNum = Physics2D.RaycastNonAlloc(origin, dir, _detectRay, distance);

            return detectedNum > 0;
        }

        public bool DetectInRayWithTag(Vector2 origin, Vector2 dir, float distance, string tag)
        {
            if (DetectInRay(origin, dir, distance))
            {
                for (int i = 0; i < _detectRay.Length; i++)
                {
                    if (_detectRay[i].collider && _detectRay[i].collider.CompareTag(tag))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool DetectInRayWithLayerMask(Vector2 origin, Vector2 dir, float distance, LayerMask mask)
        {
            ClearRayHitResults();

            int detectedNum = Physics2D.RaycastNonAlloc(origin, dir, _detectRay, distance, mask);

            return detectedNum > 0;
        }

        public bool DetectInArea(Vector2 pointA, Vector2 pointB, float distance)
        {
            ClearColliderResults();
            int detectedNum = Physics2D.OverlapAreaNonAlloc(pointA, pointB, _detectColliders);
            return detectedNum > 0;
        }


        //清空_overlapResults
        private void ClearColliderResults()
        {
            for (int i = 0; i < _maxDetectNum; i++)
            {
                _detectColliders[i] = null;
            }
        }

        private void ClearRayHitResults()
        {
            for (int i = 0; i < _maxDetectNum; i++)
            {
                _detectRay[i] = default;
            }
        }

        private bool CompareTag(string targetTag, int detectedNum)
        {
            if (detectedNum > 0)
            {
                for (int i = 0; i < _detectColliders.Length; i++)
                {
                    if (_detectColliders[i] && _detectColliders[i].CompareTag(targetTag))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}