using UnityEngine;
using UnityEngine.Events;
using ZiercCode.Utilities;

namespace ZiercCode.Management
{
    //游戏状态管理
    public class GameState : USingleton<GameState>
    {
        [HideInInspector]
        public UnityEvent onGamePause = new();

        [HideInInspector]
        public UnityEvent onGameResume = new();

        public bool IsPaused => _isPaused;

        private bool _isPaused;

        public void PauseGame()
        {
            if (_isPaused)
            {
                return;
            }

            Time.timeScale = 0;
            _isPaused = true;
            onGamePause?.Invoke();
        }

        public void ResumeGame()
        {
            if (!_isPaused)
            {
                return;
            }

            Time.timeScale = 1;
            _isPaused = false;
            onGameResume?.Invoke();
        }
    }
}