using UnityEngine;
using System.Collections.Generic;
/// <summary>
/// ��������UI��Ϣ �����Դ���������UI
/// </summary>
public class UIManager
{
    /// <summary>
    /// ��������UI��Ϣ���ֵ䣬ÿһ��UI��Ϣ�����Ӧһ��GameObject
    /// </summary>
    private Dictionary<UIType, GameObject> uiDic;

    public UIManager()
    {
        if (uiDic == null)
            uiDic = new Dictionary<UIType, GameObject>();

    }

    /// <summary>
    /// ��ȡһ��UI����
    /// </summary>
    /// <param name="type"> UI��Ϣ </param>
    /// <returns></returns>
    public GameObject GetSingleUI(UIType type)
    {
        GameObject parent = GameObject.Find("Canvas");
        if (!parent)
        {
            Debug.LogError("Canvas������!");
            return null;
        }

        if (uiDic.ContainsKey(type))
            return uiDic[type];

        GameObject ui = GameObject.Instantiate(Resources.Load<GameObject>(type.Path), parent.transform);
        ui.name = type.Name;

        uiDic.Add(type, ui);

        return ui;
    }

    /// <summary>
    /// ����һ��UI����
    /// </summary>
    public void DestroyUI(UIType type)
    {

        if (uiDic.ContainsKey(type))
        {
            GameObject.Destroy(uiDic[type]);
            uiDic.Remove(type);
        }
    }
}
