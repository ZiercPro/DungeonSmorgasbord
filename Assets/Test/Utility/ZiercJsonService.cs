using Newtonsoft.Json;
using UnityEngine;

namespace ZiercCode.Test.Utility
{
    /// <summary>
    /// json服务 用于json字符串和数据类的相互转换
    /// </summary>
    public static class ZiercJsonService
    {
        public static string ToJsonText<T>(T data)
        {
            string r = JsonConvert.SerializeObject(data);
            if (string.IsNullOrEmpty(r))
            {
                Debug.LogError("序列化失败!");
                return r;
            }
            else
            {
                return r;
            }
        }

        public static T ToObject<T>(string json)
        {
            T r = JsonConvert.DeserializeObject<T>(json);
            if (r == null)
            {
                Debug.LogError("数据解析失败!");
                return default;
            }
            else
            {
                return r;
            }
        }
    }
}