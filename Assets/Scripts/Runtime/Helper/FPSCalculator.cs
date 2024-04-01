using TMPro;
using UnityEngine;

namespace ZiercCode.Runtime.Helper
{
    /// <summary>
    /// 计算帧数
    /// </summary>
    public class FPSCalculator : MonoBehaviour
    {
        public TextMeshProUGUI fpsUI;
        public float updataInterval;

        private float fps = 0f;
        private float deltaTime = 0f;

        private float timer = 0f;

        private void Update()
        {
            timer += Time.deltaTime;
            FPSCaculate();
            if (timer > updataInterval)
            {
                fpsUI.text = Mathf.RoundToInt(fps).ToString();
                timer = 0f;
            }
        }

        private void FPSCaculate()
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.05f;
            fps = 1f / deltaTime;
        }
    }
}