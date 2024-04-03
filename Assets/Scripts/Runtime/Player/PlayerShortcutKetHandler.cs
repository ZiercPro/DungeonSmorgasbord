using System;
using UnityEngine;
using ZiercCode.Runtime.Hero;

namespace ZiercCode.Runtime.Player
{
    /// <summary>
    /// 负责处理游戏中的快捷键
    /// </summary>
    [RequireComponent(typeof(HeroInputManager))]
    public class PlayerShortcutKetHandler : MonoBehaviour
    {
        private HeroInputManager _heroInputManager;

        
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _heroInputManager = GetComponent<HeroInputManager>();
        }

        private void Start()
        {
            _heroInputManager.SetShortKey(true);
        }
        
    }
}