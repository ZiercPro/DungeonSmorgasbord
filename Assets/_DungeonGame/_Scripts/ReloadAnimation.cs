using DG.Tweening;
using NaughtyAttributes.Scripts.Core.DrawerAttributes_SpecialCase;
using UnityEngine;
using UnityEngine.UI;

namespace ZiercCode._DungeonGame._Scripts
{
    public class ReloadAnimation : MonoBehaviour
    {
        [SerializeField]
        private Image[] points;

        [SerializeField]
        private GameObject animator;

        [SerializeField]
        private float circleRadius = .3f; //半径

        [SerializeField]
        private float scaleDuration = 0.2f;

        [SerializeField]
        private float scaleRate = 1.2f;

        private bool _isPlaying;

        private void Awake()
        {
            SetupSphere();
        }


        [Button("重置位置")]
        //将所有小正方形围成一个圆形
        private void SetupSphere()
        {
            float degreePiece = 360f / points.Length;
            for (int i = 0; i < points.Length; i++)
            {
                float currentDegree = degreePiece * i;
                Quaternion rotation = Quaternion.AngleAxis(currentDegree, transform.forward);
                points[i].rectTransform.rotation = rotation;
                points[i].rectTransform.anchoredPosition = points[i].transform.right * circleRadius;
                points[i].color = Color.clear;
            }

            animator.SetActive(false);
        }

        public void PlayReloadAnimation(float duration)
        {
            if (_isPlaying)
            {
                return;
            }

            for (int i = 0; i < points.Length; i++)
            {
                points[i].color = Color.clear;
            }

            animator.SetActive(true);

            float durationPiece = duration / points.Length;

            _isPlaying = true;

            SphereFade(0, durationPiece);
        }


        private void SphereFade(int index, float fadeTime)
        {
            points[index].DOColor(Color.white, fadeTime).OnComplete(() =>
            {
                float scale = points[index].rectTransform.localScale.x;
                int scaleIndex = index;
                points[scaleIndex].rectTransform.DOScale(scale * scaleRate, scaleDuration).SetEase(Ease.OutBounce)
                    .OnComplete(() =>
                    {
                        points[scaleIndex].rectTransform.DOScale(scale * 1f, scaleDuration).SetEase(Ease.InBounce);
                    });
                index++;
                if (index < points.Length)
                {
                    SphereFade(index, fadeTime);
                }
                else
                {
                    animator.SetActive(false);
                    _isPlaying = false;
                }
            });
        }
    }
}