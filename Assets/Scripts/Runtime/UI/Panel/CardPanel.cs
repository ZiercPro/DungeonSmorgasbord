using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using UnityEngine.Localization.Components;

public class CardPanel : BasePanel
{
    public static readonly string path = "Prefabs/UI/Panel/CardPanel";
    public CardPanel() : base(new UIType(path)) { }

    private Coroutine _timeSlowCor;

    public override void OnEnter()
    {
        _timeSlowCor = MyCoroutineTool.Instance.StartCoroutine(TimeSlow());
        float start = 0f;
        CanvasGroup cGroup = UITool.GetOrAddComponent<CanvasGroup>();
        cGroup.interactable = true;
        cGroup.alpha = start;

        cGroup.DOFade(1f, 1.5f).SetUpdate(true).OnComplete(() =>
        {
            GameRoot.Instance.Pause();
            int[] cardIndex = MyMath.GetRandomRange(0, Cards.GetCards().Count - 1, 3);
            for (int i = 0; i < 3; i++)
            {
                int temp = cardIndex[i];
                RectTransform cardContainer = UITool.GetComponentInChildrenUI<RectTransform>("CardContainer");
                GameObject newCard = UIManager.GetSingleUI(new UIType("Prefabs/UI/Card/Card"));
                newCard.transform.SetParent(cardContainer.transform);
                newCard.transform.SetLocalPositionAndRotation(new Vector3((i - 1) * 800, 0f, 0f), Quaternion.identity);
                newCard.GetComponentInChildren<Button>().onClick.AddListener(() =>
                {
                    Cards.GetCards()[temp].cardEffect(GameManager.playerTans);
                    PanelManager.Pop();
                    GameRoot.Instance.Play();
                });
                //卡片文本
                LocalizeStringEvent text = newCard.GetComponent<LocalizeStringEvent>();
                switch (GameRoot.Instance.settingsData.Language)
                {
                    case 0: //中文
                        text.StringReference.Arguments = new object[]
                        {
                            GameRoot.Instance.LanguageManager.GetCardTextData()
                                .customTextTable[Cards.GetCards()[temp].id].Chinese
                        };
                        text.StringReference.RefreshString();
                        break;
                    case 1: //English
                        text.StringReference.Arguments = new object[]
                        {
                            GameRoot.Instance.LanguageManager.GetCardTextData()
                                .customTextTable[Cards.GetCards()[temp].id].English
                        };
                        text.StringReference.RefreshString();
                        break;
                    default: break;
                }
            }
        });
    }

    public override void OnPause()
    {
        MyCoroutineTool.Instance.StopCoroutine(_timeSlowCor);
    }

    public override void OnExit()
    {
        MyCoroutineTool.Instance.StopCoroutine(_timeSlowCor);
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