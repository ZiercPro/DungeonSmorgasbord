using DG.Tweening;
using UnityEngine;
using ZiercCode.Utilities;

namespace ZiercCode._DungeonGame.NPC.TextBubble
{
    public class TextBubble : MonoBehaviour
    {
        [SerializeField] private EditableDictionary<string, GameObject> bubbles;

        public void ShowTextBubble(string text)
        {
            if (bubbles.Contain(text))
            {
                if (!bubbles[text].activeInHierarchy)
                {
                    bubbles[text].SetActive(true);
                    bubbles[text].transform.DOScale(new Vector3(1f, 1f, 1f), .5f);
                }
            }
            else
            {
                Debug.LogWarning($"{text}气泡不存在");
            }
        }

        public void HideTextBubble(string text)
        {
            if (bubbles.Contain(text))
            {
                if (bubbles[text].activeInHierarchy)
                {
                    bubbles[text].transform.DOScale(new Vector3(0f, 0f, 0f), .5f)
                        .OnComplete(() => bubbles[text].SetActive(false));
                }
            }
        }

        public string ShowRandomTextBubble()
        {
            int index = MyMath.GetRandom(0, bubbles.Count);
            string result = bubbles.GetKey(index);
            ShowTextBubble(result);
            return result;
        }
    }
}