using TMPro;
using UnityEngine;
using ZiercCode.Old.Basic;
using ZiercCode.Old.UI;

namespace ZiercCode.Old.Manager
{

    public class TextPopupSpawner : USingletonComponentDestroy<TextPopupSpawner>
    {
        [SerializeField] private GameObject damagePopupTemp;

        //对象池
        public GameObject InitPopupText(Vector3 pos, Color textColor, int amount)
        {
            GameObject newP = Instantiate(damagePopupTemp, pos, Quaternion.identity);
            newP.GetComponent<TextMeshPro>().color = textColor;
            newP.GetComponent<TextMeshPro>().text = amount.ToString();
            newP.GetComponent<PopupText>().Popup();
            return newP;
        }

        public GameObject InitPopupText(Vector3 pos, Color textColor, string text)
        {
            GameObject newP = Instantiate(damagePopupTemp, pos, Quaternion.identity);
            newP.GetComponent<TextMeshPro>().color = textColor;
            newP.GetComponent<TextMeshPro>().text = text;
            newP.GetComponent<PopupText>().Popup();
            return newP;
        }
    }
}