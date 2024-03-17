using TMPro;
using UnityEngine;
using Runtime.FeedBack;

public class TextPopupSpawner : SingletonIns<TextPopupSpawner> {
    public GameObject damagePopupTemp;
    //对象池
    
    public GameObject InitPopupText(Transform pos, Color textColor, int amount) {
        GameObject newP = Instantiate(damagePopupTemp, pos.position, Quaternion.identity);
        newP.GetComponent<TextMeshPro>().color = textColor;
        newP.GetComponent<TextMeshPro>().text = amount.ToString();
        newP.GetComponent<PopupText>().Popup();
        return newP;
    }
    public GameObject InitPopupText(Transform pos, Color textColor, string text) {
        GameObject newP = Instantiate(damagePopupTemp, pos.position, Quaternion.identity);
        newP.GetComponent<TextMeshPro>().color = textColor;
        newP.GetComponent<TextMeshPro>().text = text;
        newP.GetComponent<PopupText>().Popup();
        return newP;
    }
}
