using UnityEngine;

namespace ZiercCode._DungeonGame.HallScene
{
    public class RangeDetect
    {
        private int _maxDetectNum; //检测碰撞体最大数量
        private Collider2D[] _detectColliders;

        public RangeDetect(int maxDetectNum)
        {
            _maxDetectNum = maxDetectNum;
            _detectColliders = new Collider2D[_maxDetectNum];
        }

        public Collider2D[] GetColliders()
        {
            return _detectColliders;
        }

        public bool DetectInBox(Vector2 point, Vector2 boxRange, float angle = 0f)
        {
            ClearResults();

            int detectCount = Physics2D.OverlapBoxNonAlloc(point, boxRange, angle, _detectColliders);

            return detectCount > 0;
        }

        //检测是否碰到碰撞体 圆形范围
        public bool DetectInCircle(Vector2 point, float radius)
        {
            ClearResults();

            int detectCount = Physics2D.OverlapCircleNonAlloc(point, radius, _detectColliders);

            return detectCount > 0;
        }

        //通过tag判断是否为目标，如果在范围内则返回true否则返回false 圆形范围
        public bool DetectByTagInCircle(string targetTag, Vector2 point, float radius)
        {
            ClearResults();

            int detectedNum = Physics2D.OverlapCircleNonAlloc(point, radius, _detectColliders);

            return CompareTag(targetTag, detectedNum);
        }


        //通过tag判断是否为目标 方形范围检测 
        public bool DetectByTagInBox(string targetTag, Vector2 point, Vector2 boxRange, float angle = 0f)
        {
            ClearResults();

            int detectedNum = Physics2D.OverlapBoxNonAlloc(point, boxRange, angle, _detectColliders);

            return CompareTag(targetTag, detectedNum);
        }

        //通过Layer判断是否为目标，如果在范围内则返回true否则返回false 圆形范围
        public bool DetectByLayerInCircle(LayerMask targetLayer, Vector2 point, float radius)
        {
            ClearResults();

            int detectedNum = Physics2D.OverlapCircleNonAlloc(point, radius, _detectColliders);

            return CompareLayer(targetLayer, detectedNum);
        }


        //通过Layer判断是否为目标 方形范围检测 
        public bool DetectByLayerInBox(LayerMask targetLayer, Vector2 point, Vector2 boxRange, float angle = 0f)
        {
            ClearResults();

            int detectedNum = Physics2D.OverlapBoxNonAlloc(point, boxRange, angle, _detectColliders);

            return CompareLayer(targetLayer, detectedNum);
        }


        //清空_overlapResults
        private void ClearResults()
        {
            for (int i = 0; i < _maxDetectNum; i++)
            {
                _detectColliders[i] = null;
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

        private bool CompareLayer(LayerMask targetLayer, int detectedNum)
        {
            if (detectedNum > 0)
            {
                for (int i = 0; i < _detectColliders.Length; i++)
                {
                    if (_detectColliders[i].gameObject.layer.Equals(targetLayer))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}