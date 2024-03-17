using Cinemachine;
using UnityEngine;
using System.Collections;

public class CameraShakeFeedback : MonoBehaviour
{
    [SerializeField] private float intensity = 1f; //强度
    [SerializeField] private float frequence = 1f; //频率
    [SerializeField] private float duration = 0.2f; //持续时间

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