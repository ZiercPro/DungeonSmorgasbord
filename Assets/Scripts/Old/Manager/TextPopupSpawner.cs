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

        public void InitPopupText(Vector3 startPosition, Color textColor, int amount)
        {
            InitPopupText(startPosition, textColor, amount.ToString());
        }

        public void InitPopupText(Vector3 startPosition, Color textColor, string text)
        {
            SpawnHandle handle = spawner.SpawnPoolObject(intPopupTemp, startPosition, Quaternion.identity);
            GameObject obj = handle.GetObject();
            obj.GetComponent<TextMeshPro>().color = textColor;
            obj.GetComponent<TextMeshPro>().text = text;
            obj.GetComponent<TextPopupAnimation>()
                .Popup(() => handle.Release());
        }
    }
}