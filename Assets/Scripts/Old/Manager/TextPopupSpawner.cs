using TMPro;
using UnityEngine;
using ZiercCode.Core.Pool;
using ZiercCode.Core.System;
using ZiercCode.DungeonSmorgasbord.Extend;

namespace ZiercCode.Old.Manager
{
    public class TextPopupSpawner : USingletonComponentDestroy<TextPopupSpawner>
    {
        [SerializeField] private GameObject intPopupTemp;

        protected override void Awake()
        {
            base.Awake();
            PoolManager.Instance.CreatePool(intPopupTemp.name, CreatFunc, GetFunc, ReleaseFunc, DestroyFunc, false, 50,
                100);
        }

        public GameObject InitPopupText(Transform position, Color textColor, int amount)
        {
            return InitPopupText(position, textColor, amount.ToString());
        }

        public GameObject InitPopupText(Transform position, Color textColor, string text)
        {
            GameObject newP = PoolManager.Instance.GetPoolObject(intPopupTemp.name, position, Quaternion.identity);
            newP.GetComponent<TextMeshPro>().color = textColor;
            newP.GetComponent<TextMeshPro>().text = text;
            newP.GetComponent<TextPopupAnimation>()
                .Popup(popup => PoolManager.Instance.ReleasePoolObject(intPopupTemp.name, popup));
            return newP;
        }

        private GameObject CreatFunc()
        {
            GameObject newPopup = Instantiate(intPopupTemp);
            newPopup.SetActive(false);
            return newPopup;
        }

        private void GetFunc(GameObject popup)
        {
            popup.SetActive(true);
        }

        private void ReleaseFunc(GameObject popup)
        {
            PoolManager.Instance.DefaultReleaseFunc(intPopupTemp.name, popup);
        }

        private void DestroyFunc(GameObject popup)
        {
            Destroy(popup);
        }
    }
}