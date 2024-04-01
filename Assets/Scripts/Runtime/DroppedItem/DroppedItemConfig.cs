using NaughtyAttributes;
using UnityEngine;

namespace ZRuntime
{


    [System.Serializable]
    public class DroppedItemConfig
    {
        [SerializeField] [Range(0, 100)] private int dropChance;
        [SerializeField] private GameObject droppedItemTemp;
        [SerializeField] private bool haveItemNumRange;

        [HideIf("haveItemNumRange")] [AllowNesting] [SerializeField]
        private int itemNum;

        [ShowIf("haveItemNumRange")] [AllowNesting] [SerializeField]
        private int minNum;

        [ShowIf("haveItemNumRange")] [AllowNesting] [SerializeField]
        private int maxNum;

        /// <summary>
        /// 生成掉落物
        /// </summary>
        /// <param name="position">生成位置</param>
        /// <param name="bursForce">物体随机弹开力的大小</param>
        public void InstantiateItem(Transform position, float bursForce)
        {
            bool willDrop = MyMath.ChanceToBool(dropChance / 100f);
            if (!willDrop) return;
            Rigidbody2D r2d;
            if (haveItemNumRange)
            {
                if (minNum > maxNum)
                {
                    Debug.LogError("最小值大于最大值");
                    return;
                }

                int finalNum = Random.Range(minNum, maxNum);
                for (int i = 0; i < finalNum; i++)
                {
                    GameObject newItem =
                        GameObject.Instantiate(droppedItemTemp, position.transform.position, Quaternion.identity);
                    r2d = newItem.GetComponent<Rigidbody2D>();
                    r2d.AddForce(MyMath.GetRandomVector2() * bursForce, ForceMode2D.Impulse);
                }
            }
            else
            {
                for (int i = 0; i < itemNum; i++)
                {
                    GameObject newItem =
                        GameObject.Instantiate(droppedItemTemp, position.transform.position, Quaternion.identity);
                    r2d = newItem.GetComponent<Rigidbody2D>();
                    r2d.AddForce(MyMath.GetRandomVector2() * bursForce, ForceMode2D.Impulse);
                }
            }
        }
    }
}