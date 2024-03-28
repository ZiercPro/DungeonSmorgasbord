using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using UnityEngine.Localization.Components;

namespace Runtime.UI.Panel
{
    using Helper;
    using Manager;
    using Card;
    using UIFramework;

    public class CardPanel : BasePanel
    {
        public static readonly string path = "Prefabs/UI/Panel/CardPanel";
        public CardPanel() : base(new UIType(path)) { }

        private Coroutine _timeSlowCoroutine;

        public override void OnEnter()
        {
            _timeSlowCoroutine = MyCoroutineTool.Instance.StartCoroutine(TimeSlow());
            float start = 0f;
            CanvasGroup cGroup = UITool.GetOrAddComponent<CanvasGroup>();
            cGroup.interactable = true;
            cGroup.alpha = start;

            cGroup.DOFade(1f, 1.5f).SetUpdate(true).OnComplete(() =>
            {
                GameRoot.Instance.Pause();
                int[] cardIndex = MyMath.GetRandomInts(0, Cards.GetCards().Count - 1, 3);
                for (int i = 0; i < 3; i++)
                {
                    int temp = cardIndex[i];
                    RectTransform cardContainer = UITool.GetComponentInChildrenUI<RectTransform>("CardContainer");
                    GameObject newCard = UIManager.GetSingleUI(new UIType("Prefabs/UI/Card/Card"));
                    newCard.transform.SetParent(cardContainer.transform);
                    newCard.transform.SetLocalPositionAndRotation(new Vector3((i - 1) * 800, 0f, 0f),
                        Quaternion.identity);
                    newCard.GetComponentInChildren<Button>().onClick.AddListener(() =>
                    {
                        Cards.GetCards()[temp].cardEffect(GameManager.playerTans);
                        PanelManager.Pop();
                        GameRoot.Instance.Play();
                    });
                    //卡片文本
                    LocalizeStringEvent text = newCard.GetComponent<LocalizeStringEvent>();
                    text.StringReference.Arguments = new object[]
                    {
                        GameRoot.Instance.LanguageManager.GetCardText(Cards.GetCards()[cardIndex[i]].id)
                    };
                    text.StringReference.RefreshString();
                }
            });
        }

        public override void OnPause()
        {
            MyCoroutineTool.Instance.StopCoroutine(_timeSlowCoroutine);
        }

        public override void OnExit()
        {
            MyCoroutineTool.Instance.StopCoroutine(_timeSlowCoroutine);
            GameRoot.Instance.Play();
            CanvasGroup cGroup = UITool.GetOrAddComponent<CanvasGroup>();
            cGroup.interactable = false;
            cGroup.DOFade(0f, 0.5f).OnComplete(() =>
            {
                UIManager.DestroyUI(UIType);
            });
            UIManager.DestroyUI(UIType);
            //游戏继续
        }

        private IEnumerator TimeSlow()
        {
            float t = 1f;
            while (true)
            {
                t -= Time.deltaTime;
                Time.timeScale = t;
                if (t <= 0.1f) break;
                yield return null;
            }
        }
    }
}