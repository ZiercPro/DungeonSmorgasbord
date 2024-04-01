using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZRuntime.Manager;

namespace ZRuntime
{

    /// <summary>
    /// 异步加载工具
    /// </summary>
    public class AsyncLoadTool : MonoBehaviour
    {
        private Coroutine _loadingCoroutine; //加载场景协程

        /// <summary>
        /// 异步加载方法
        /// </summary>
        /// <param name="slider">进度条</param>
        /// <param name="text">进度</param>
        /// <param name="newSceneState">场景状态</param>
        /// <param name="newSceneName">场景名</param>
        public void AsyncLoad(Slider slider, TextMeshProUGUI text, SceneState newSceneState, string newSceneName)
        {
            _loadingCoroutine = StartCoroutine(LoadScene(slider, text, newSceneState, newSceneName));
        }

        private IEnumerator LoadScene(Slider slider, TextMeshProUGUI text, SceneState newSceneState,
            string newSceneName)
        {
            float t = 0f;
            while (t < 2f)
            {
                t += Time.deltaTime;
                slider.value = t * 0.5f;
                text.text = ((int)(t * 50)).ToString() + '%';
                yield return null;
            }

            GameRoot.Instance.SceneSystem.SetScene(newSceneState);
        }
    }
}