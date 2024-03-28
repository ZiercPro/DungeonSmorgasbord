using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Runtime.UI
{
    public class ButtonShake : MonoBehaviour
    {
        private const float shakeStrength = 0.5f;
        private const float shakeDuration = 0.5f;
        private bool isShaking;

        private void OnDestroy()
        {
            StopAllCoroutines();
            isShaking = false;
        }

        private void OnDisable()
        {
            StopAllCoroutines();
            isShaking = false;
        }

        public void Shake()
        {
            if (isShaking) return;
            isShaking = true;
            StartCoroutine(StartShake());
        }

        IEnumerator StartShake()
        {
            transform.DOShakePosition(shakeDuration, shakeStrength).SetUpdate(true);
            transform.DOShakeRotation(shakeDuration, shakeStrength).SetUpdate(true);
            transform.DOShakeScale(shakeDuration, shakeStrength).SetUpdate(true);
            yield return new WaitForSecondsRealtime(shakeDuration);
            isShaking = false;
        }
    }
}