using System.Collections;
using TMPro;
using UnityEngine;

namespace ZiercCode.Old.Helper
{
    /// <summary>
    /// 计算帧数
    /// </summary>
    public class FPSCalculator : MonoBehaviour
    {
        public TextMeshProUGUI fpsUI;
        public float updateInterval;

        private float fps = 0f;
        private float deltaTime = 0f;

        private float timer = 0f;

        private void Start()
        {
            
        }

        private IEnumerator CalculateFPS()
        {
            while (true)
            {
                timer += Time.deltaTime;
                FPSCalculate();
                if (timer > updateInterval)
                {
                    fpsUI.text = Mathf.RoundToInt(fps).ToString();
                    timer = 0f;
                }

                yield return null;
            }
        }


        private void FPSCalculate()
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.05f;
            fps = 1f / deltaTime;
        }
    }
}