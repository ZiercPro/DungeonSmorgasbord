using TMPro;
using UnityEngine;
using ZiercCode.Core.Pool;
using ZiercCode.Core.Utilities;
using ZiercCode.DungeonSmorgasbord.Extend;

namespace ZiercCode.Old.Manager
{
    public class TextPopupSpawner : USingletonComponentDestroy<TextPopupSpawner>
    {
        [SerializeField] private PoolObjectSo intPopupTemp;
        [SerializeField] private PoolObjectSpawner spawner;

        public GameObject InitPopupText(Transform position, Color textColor, int amount)
        {
            return InitPopupText(position, textColor, amount.ToString());
        }

        public GameObject InitPopupText(Transform position, Color textColor, string text)
        {
            SpawnHandle handle = spawner.SpawnPoolObject(intPopupTemp, position, Quaternion.identity);
            GameObject obj = handle.GetObject();
            obj.GetComponent<TextMeshPro>().color = textColor;
            obj.GetComponent<TextMeshPro>().text = text;
            obj.GetComponent<TextPopupAnimation>()
                .Popup(() => handle.Release());
            return obj;
        }
    }
}