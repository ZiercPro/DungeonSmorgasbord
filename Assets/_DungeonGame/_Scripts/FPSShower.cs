using TMPro;
using UnityEngine;
using ZiercCode.Utilities;

namespace ZiercCode._DungeonGame._Scripts
{
    /// <summary>
    /// 帧数显示器
    /// 负责计算和显示帧数
    /// </summary>
    public class FPSShower : USingleton<FPSShower>
    {
        [SerializeField]
        private TextMeshProUGUI fpsText;

        [SerializeField]
        private float fpsUpdateInterval = 0.5f; //fps更新间隔

        private bool _showFps;
        private int _frameCount;
        private float _updateTimer;

        private int _currentFPS;

        public void SetFPS(bool active)
        {
            _showFps = active;
            fpsText.gameObject.SetActive(active);
        }

        private void Update()
        {
            if (!_showFps)
            {
                return;
            }

            _updateTimer += Time.unscaledDeltaTime;
            _frameCount++;

            if (_updateTimer >= fpsUpdateInterval)
            {
                _currentFPS = Mathf.RoundToInt(_frameCount / fpsUpdateInterval);
                fpsText.SetText(_currentFPS.ToString());

                _frameCount = 0;
                _updateTimer = 0f;
            }
        }
    }
}