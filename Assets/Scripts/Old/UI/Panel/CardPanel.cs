using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
using ZiercCode.Core.UI;
using ZiercCode.Runtime.Card;
using ZiercCode.Runtime.Helper;
using ZiercCode.Runtime.Hero;
using ZiercCode.Runtime.Manager;

namespace ZiercCode.Runtime.UI.Panel
{
    public class CardPanel : BaseAnimationPanel
    {
        public static readonly string path = "Prefabs/UI/Panel/CardPanel";
        public CardPanel() : base(new UIType(path)) { }
        private PlayerInputManager _playerInputManager;
        private HeroInputManager _heroInputManager;
        private Coroutine _timeSlowCoroutine;

        public override void OnEnter()
        {
            BanHeroInput();
            _timeSlowCoroutine = MyCoroutineTool.Instance.StartCoroutine(TimeSlow());
            InAnimate(1.5f);
            Time.timeScale = 0f;
            CreateCard();
        }


        public override void OnPause()
        {
            MyCoroutineTool.Instance.StopCoroutine(_timeSlowCoroutine);
        }

        public override void OnExit()
        {
            MyCoroutineTool.Instance.StopCoroutine(_timeSlowCoroutine);
            Time.timeScale = 1f;
            OutAnimate(0.5f);
            UIManager.DestroyUI(UIType);
            ReleaseHeroInput();
            //游戏继续
        }

        public override void InAnimate(float animationTime)
        {
            float start = 0f;
            CanvasGroup cGroup = UITool.GetOrAddComponent<CanvasGroup>();
            cGroup.interactable = true;
            cGroup.alpha = start;
            cGroup.DOFade(1f, animationTime).SetUpdate(true);
        }

        public override void OutAnimate(float animationTime)
        {
            CanvasGroup cGroup = UITool.GetOrAddComponent<CanvasGroup>();
            cGroup.interactable = false;
            cGroup.DOFade(0f, animationTime).SetUpdate(true);
        }

        /// <summary>
        /// 生成卡片
        /// </summary>
        private void CreateCard()
        {
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
                    Cards.GetCards()[temp].cardEffect(GameManager.playerTrans);
                    PanelManager.Pop();
                    Time.timeScale = 1f;
                });
                //卡片文本
                LocalizeStringEvent text = newCard.GetComponent<LocalizeStringEvent>();
                text.StringReference.Arguments = new object[]
                {
                    LocaleManager.GetCardText(Cards.GetCards()[cardIndex[i]].id)
                };
                text.StringReference.RefreshString();
            }
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

        //禁用玩家输入
        private void BanHeroInput()
        {
            if (GameManager.playerTrans)
            {
                _heroInputManager = GameManager.playerTrans.GetComponent<HeroInputManager>();
                _heroInputManager.enabled = false;
            }
        }

        //放行玩家输入
        private void ReleaseHeroInput()
        {
            _heroInputManager.enabled = true;
            _heroInputManager = null;
        }
    }
}