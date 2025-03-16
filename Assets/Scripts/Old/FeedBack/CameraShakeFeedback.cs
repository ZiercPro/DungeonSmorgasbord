using Cinemachine;
using System.Collections;
using UnityEngine;

namespace ZiercCode.Old.FeedBack
{
    public class CameraShakeFeedback : MonoBehaviour
    {
        [SerializeField] private float intensity = 1f; //强度
        [SerializeField] private float frequence = 1f; //频率
        [SerializeField] private float duration = 0.2f; //持续时间

        [SerializeField] private NoiseSettings noiseSettings; //抖动噪声设置

        private CinemachineVirtualCamera _virtualCamera;

        private void Awake()
        {
            _virtualCamera = FindAnyObjectByType<CinemachineVirtualCamera>();
        }

        public void StartShake()
        {
            StartCoroutine(Shake());
        }

        private IEnumerator Shake()
        {
            CinemachineBasicMultiChannelPerlin perlinChannel =
                _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            if (!perlinChannel)
                perlinChannel = _virtualCamera.AddCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            perlinChannel.m_NoiseProfile = noiseSettings;

            perlinChannel.m_AmplitudeGain = intensity;
            perlinChannel.m_FrequencyGain = frequence;
            for (float i = 0; i < duration; i += Time.deltaTime)
            {
                yield return null;
            }

            perlinChannel.m_AmplitudeGain = 0f;
            perlinChannel.m_FrequencyGain = 0f;
        }
    }
}