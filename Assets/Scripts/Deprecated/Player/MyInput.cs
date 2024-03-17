using UnityEngine;

/// <summary>
/// AI或者玩家的输入
/// </summary>
[System.Obsolete]
public abstract class MyInput : MonoBehaviour {
  
    public Vector2 moveDir { get; protected set; }
    public Vector2 viewPos { get; protected set; }
    protected abstract bool IsEventBeSet();
    protected abstract void InitEvent();
}
