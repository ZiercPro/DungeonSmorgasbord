using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

namespace ZiercCode.Runtime.Component
{
    public class ScaleShakeFeedBack : MonoBehaviour
    {
        [Header("Config")] [SerializeField] private Transform animatorTransform;
        [SerializeField] private float duration = 1f;
        [SerializeField] private float strength = 1f;

        private Tweener _shakeTweener;

        public void StartShake()
        {
            _shakeTweener = animatorTransform.DOShakeScale(duration, strength).SetEase(Ease.Flash);
            _shakeTweener.SetAutoKill(true);
        }
    }
}