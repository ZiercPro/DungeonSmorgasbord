using DG.Tweening;
using System.Linq;
using UnityEngine;
using ZiercCode.Utilities;

namespace ZiercCode._DungeonGame.NPC.TextBubble
{
    /// <summary>
    /// NPC表情气泡
    /// </summary>
    public class EmojiBubble : MonoBehaviour
    {
        [SerializeField]
        private EditableDictionary<string, GameObject> bubbles;

        public void ShowTextBubble(string text)
        {
            if (bubbles.ToDictionary.ContainsKey(text))
            {
                if (!bubbles.ToDictionary[text].activeInHierarchy)
                {
                    bubbles.ToDictionary[text].SetActive(true);
                    bubbles.ToDictionary[text].transform.DOScale(new Vector3(1f, 1f, 1f), .5f)
                        .SetEase(Ease.OutBounce);
                }
            }
            else
            {
                Debug.LogWarning($"{text}气泡不存在");
            }
        }

        public void HideTextBubble(string text)
        {
            if (bubbles.ToDictionary.ContainsKey(text))
            {
                if (bubbles.ToDictionary[text].activeInHierarchy)
                {
                    bubbles.ToDictionary[text].transform.DOScale(new Vector3(0f, 0f, 0f), .5f).SetEase(Ease.InBounce)
                        .OnComplete(() => bubbles.ToDictionary[text].SetActive(false));
                }
            }
        }

        public string ShowRandomTextBubble()
        {
            int index = MyMath.GetRandom(0, bubbles.ToDictionary.Count);
            string result = bubbles.ToDictionary.Keys.ToList()[index];
            ShowTextBubble(result);
            return result;
        }
    }
}