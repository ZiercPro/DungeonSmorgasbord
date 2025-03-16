using TMPro;
using UnityEngine;
using ZiercCode.DungeonSmorgasbord.Extend;
using ZiercCode.ObjectPool;
using ZiercCode.Utilities;

namespace ZiercCode
{
    public class TextPopup : USingleton<TextPopup>
    {
        [SerializeField] private GameObject popupPrefab;

        public void Init()
        {
            PoolManager.Instance.Register("textPopup", popupPrefab);
        }

        public void InitPopupText(Vector3 startPosition, Color textColor, int amount)
        {
            InitPopupText(startPosition, textColor, amount.ToString());
        }

        public void InitPopupText(Vector3 startPosition, Color textColor, string text)
        {
            GameObject obj = (GameObject)PoolManager.Instance.Get("textPopup");
            obj.transform.position = startPosition;
            obj.transform.rotation = Quaternion.identity;
            obj.GetComponent<TextMeshPro>().color = textColor;
            obj.GetComponent<TextMeshPro>().text = text;
            obj.GetComponent<TextPopupAnimation>()
                .Popup(() => PoolManager.Instance.Release("textPopup", obj));
        }
    }
}