using System.Collections.Generic;
using UnityEngine;

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
    public static bool IsEffected(float chance)
    {
        if (chance <= 0f) return false;
        float temp = Random.Range(0.0f, 1.0f);
        if (temp > chance) return false;
        return true;
    }

    /// <summary>
    /// 两点间距离比较,性能消耗较小  小于range返回true 大于返回false
    /// </summary>
    /// <param name="pos1">位置1</param>
    /// <param name="pos2">位置2</param>
    /// <param name="range">目标范围</param>
    /// <returns></returns>
    public static bool DistanceCpsL(Vector3 pos1, Vector3 pos2, float range)
    {
        if ((pos1 - pos2).sqrMagnitude <= range * range) return true;
        return false;
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
    ///获取随机的单位化vector2
    /// </summary>
    /// <returns></returns>
    public static Vector2 GetRandomVector2()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        Vector2 result = new Vector2(x, y).normalized;
        return result;
    }
}