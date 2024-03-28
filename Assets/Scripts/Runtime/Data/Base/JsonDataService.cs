using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

namespace Runtime.Data.Base
{
    
public class JsonDataService : IDataService
{
    public bool SaveData<T>(string relativePath, T data, bool encrypted)
    {
        string path = Application.persistentDataPath + relativePath;

        try
        {
            if (File.Exists(path))
            {
                Debug.Log("File exists!Deleting old file and writing a new file to replace it");
                File.Delete(path);
            }
            else
            {
                Debug.Log("Writing file for the first time!");
            }

            using FileStream fileStream = File.Create(path); //使用using可以自动释放资源 不需要在finally中手动释放filestream
            fileStream.Close();
            File.WriteAllText(path, JsonConvert.SerializeObject(data));
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Unable to save data due to:{e.Message};{e.StackTrace}");
            return false;
        }
    }

    public T LoadData<T>(string relativePath, bool encrypted)
    {
        string path = Application.persistentDataPath + relativePath;

        if (!File.Exists(path))
        {
            Debug.LogError($"Can not find file {path}!");
            throw new FileNotFoundException();
        }
        else
        {
            try
            {
                T data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
                return data;
            }
            catch (Exception e)
            {
                Debug.LogError($"Can not load data due to:{e.Message};{e.StackTrace}");
                throw e; //重新抛出 方便调用方法出问题时 进行处理 提示玩家等等
            }
        }
    }
}
}
