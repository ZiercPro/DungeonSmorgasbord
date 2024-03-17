using TMPro;
using UnityEngine;

public class DamagePopupSpawner : SingletonIns<DamagePopupSpawner> {
    public GameObject damagePopupTemp;
    public GameObject InitDamagePopup(Transform pos, Color textColor, int amount) {
        GameObject newP = Instantiate(damagePopupTemp, pos.position, Quaternion.identity);
        newP.GetComponent<TextMeshPro>().color = textColor;
        newP.GetComponent<TextMeshPro>().text = amount.ToString();
        newP.GetComponent<DamagePopup>().Popup();
        return newP;
    }
}
