using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// ��æ����Э�̵ĵ���
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
    /// ����һ��Э��
    /// </summary>
    public Coroutine StartMyCor(IEnumerator cor)
    {
        Coroutine newCor = StartCoroutine(cor);
        m_Coroutines.Add(newCor);
        return newCor;
    }

    /// <summary>
    /// �ر�һ��Э��
    /// </summary>
    public void StopMyCor(Coroutine cor)
    {
        m_Coroutines.Remove(cor);
        StopCoroutine(cor);
    }

    /// <summary>
    /// �ر�����Э��
    /// </summary>
    public void StopAllCor()
    {
        foreach (Coroutine cor in m_Coroutines)
        {
            StopCoroutine(cor);
        }
    }
}
