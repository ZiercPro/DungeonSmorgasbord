using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// 帮忙开启协程的单例
/// </summary>
public class MyCoroutineTool : SingletonIns<MyCoroutineTool>
{
    private List<Coroutine> m_Coroutines;

    private void Awake()
    {
        m_Coroutines = new List<Coroutine>();
    }
    private void OnDisable()
    {
        StopAllCor();
    }
    /// <summary>
    /// 开启一个协程
    /// </summary>
    public Coroutine StartMyCor(IEnumerator cor)
    {
        Coroutine newCor = StartCoroutine(cor);
        m_Coroutines.Add(newCor);
        return newCor;
    }

    /// <summary>
    /// 关闭一个协程
    /// </summary>
    public void StopMyCor(Coroutine cor)
    {
        m_Coroutines.Remove(cor);
        StopCoroutine(cor);
    }

    /// <summary>
    /// 关闭所有协程
    /// </summary>
    public void StopAllCor()
    {
        foreach (Coroutine cor in m_Coroutines)
        {
            StopCoroutine(cor);
        }
    }
}
