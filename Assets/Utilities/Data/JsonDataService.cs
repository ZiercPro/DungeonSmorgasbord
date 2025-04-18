using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

namespace ZiercCode.Utilities.Data
{
    /// <summary>
    /// json数据服务
    /// </summary>
    public class JsonDataService : IDataService
    {
        public bool SaveData<T>(string relativePath, T data, bool encrypted)
        {
            string path = Environment.CurrentDirectory + relativePath;

            try
            {
                if (File.Exists(path))
                {
                    Debug.Log("文件存在，将创建新的文件覆盖原文件！");
                    File.Delete(path);
                }
                else
                {
                    Debug.Log("第一次创建文件！");
                }

                using FileStream fileStream = File.Create(path); //使用using可以自动释放资源 不需要在finally中手动释放filestream
                fileStream.Close();
                File.WriteAllText(path, JsonConvert.SerializeObject(data));
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"无法保存：{e.Message};{e.StackTrace}");
                return false;
            }
        }

        public bool LoadData<T>(string relativePath, out T outData, bool encrypted)
        {
            string path = Environment.CurrentDirectory + relativePath;

            if (!File.Exists(path))
            {
                Debug.LogWarning($"无法找到文件：{path}！");
                outData = default;
                return false;
            }
            else
            {
                try
                {
                    T data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
                    outData = data;
                    return true;
                }
                catch (Exception e)
                {
                    Debug.LogError($"无法加载：{e.Message};{e.StackTrace}");
                    outData = default;
                    return false;
                }
            }
        }
    }
}