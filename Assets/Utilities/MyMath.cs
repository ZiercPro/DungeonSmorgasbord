using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ZiercCode.Utilities
{
    /// <summary>
    /// 自己的数学类，用来进行数值的处理
    /// </summary>
    public static class MyMath
    {
        /// <summary>
        /// 传入一个0-1之间的数 判断以这个概率是否实现
        /// </summary>
        /// <param name="chance"></param>
        /// <returns></returns>
        public static bool ChanceToBool(float chance)
        {
            if (chance <= 0f) return false;
            float temp = Random.Range(0.0f, 1.0f);
            if (temp > chance) return false;
            return true;
        }

        /// <summary>
        /// 两点间距离比较,性能消耗较小  小于等于range返回true 大于返回false
        /// </summary>
        /// <param name="pos1">位置1</param>
        /// <param name="pos2">位置2</param>
        /// <param name="range">目标范围</param>
        /// <returns></returns>
        public static bool CompareDistanceWithRange(Vector3 pos1, Vector3 pos2, float range)
        {
            if ((pos1 - pos2).sqrMagnitude <= range * range) return true;
            return false;
        }

        /// <summary>
        /// 计算两点距离
        /// </summary>
        /// <param name="pos1">点1</param>
        /// <param name="pos2">点2</param>
        /// <returns>两点间距离</returns>
        public static float CalculateDistance(Vector3 pos1, Vector3 pos2)
        {
            return Vector3.Distance(pos1, pos2);
        }

        /// <summary>
        /// 获取范围内N个随机整数，不重复，能取到最大和最小
        /// </summary>
        /// <param name="min">最小范围</param>
        /// <param name="max">最大范围</param>
        /// <param name="num">随机数数量</param>
        /// <returns></returns>
        public static int[] GetRandomInts(int min, int max, int num)
        {
            int[] result;
            int temp;
            int i = 0;
            HashSet<int> hash;
            if (max < min)
            {
                Debug.LogError($"最小值{min}大于最大值{max}");
                return null;
            }

            if (num > max - min + 1)
            {
                Debug.LogError($"输入数值{num}超出了最大可获取量");
                return null;
            }

            if (num == max - min + 1)
            {
                result = new int[num];
                for (; i < num; i++)
                {
                    result[i] = min + i;
                }

                return result;
            }

            result = new int[num];

            hash = new HashSet<int>();

            while (hash.Count < num)
            {
                temp = Random.Range(min, max + 1);
                hash.Add(temp);
            }

            foreach (int value in hash)
            {
                result[i] = value;
                i++;
            }

            return result;
        }

        /// <summary>
        /// 获取范围内随机浮点数
        /// 包括最大和最小值
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        public static float GetRandom(float min, float max)
        {
            return Random.Range(min, max);
        }

        /// <summary>
        /// 获取范围内随机整数
        /// 包括最小值，不包括最大值
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        public static int GetRandom(int min, int max)
        {
            return Random.Range(min, max);
        }

        /// <summary>
        ///获取随机的单位化vector2，主要用于方向
        /// </summary>
        /// <returns></returns>
        public static Vector2 GetRandomVector2()
        {
            float x = Random.Range(-1f, 1f);
            float y = Random.Range(-1f, 1f);
            Vector2 result = new Vector2(x, y).normalized;
            return result;
        }


        /// <summary>
        /// 获取范围内的一个随机vector2
        /// </summary>
        /// <param name="range">范围包含从-range到range</param>
        /// <returns></returns>
        public static Vector2 GetRandomPos(Vector2 center, Vector2 range)
        {
            return new Vector2(center.x + GetRandom(-range.x, range.x), center.y + GetRandom(-range.y, range.y));
        }


        /// <summary>
        /// 从后向前遍历，并对链表进行操作，主要是应用于在遍历时，需要对链表进行调整的操作
        /// </summary>
        /// <param name="list">操作链表</param>
        /// <param name="action">操作</param>
        /// <typeparam name="T">链表储存对象</typeparam>
        public static void ForeachFromLast<T>(IList<T> list, Action<T> action)
        {
            if (list == null || list.Count <= 0)
            {
                Debug.LogWarning("链表为空");
                return;
            }

            for (int i = list.Count - 1; i > -1; i--)
            {
                action?.Invoke(list[i]);
            }
        }
    }
}