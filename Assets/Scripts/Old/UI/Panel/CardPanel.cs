using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
using ZiercCode.Core.UI;
using ZiercCode.Old.Card;
using ZiercCode.Old.Helper;
using ZiercCode.Old.Hero;
using ZiercCode.Old.Manager;

namespace ZiercCode.Old.UI.Panel
{
    public class CardPanel : BaseAnimationPanel
    {
        private static readonly string Path = "Prefabs/UI/Panel/CardPanel";
        public CardPanel() : base(new UIType(Path)) { }
        private HeroInputManager _heroInputManager;
        private Coroutine _timeSlowCoroutine;

        public override void OnEnter()
        {
            _timeSlowCoroutine = MyCoroutineTool.Instance.StartCoroutine(TimeSlow());
            base.OnEnter();
            BanPlayerInput();
            CreateCard();
        }

        public override void OnPause()
        {
            MyCoroutineTool.Instance.StopCoroutine(_timeSlowCoroutine);
        }

        public override void OnResume()
        {
            _timeSlowCoroutine = MyCoroutineTool.Instance.StartCoroutine(TimeSlow());
        }

        public override void OnExit()
        {
            base.OnExit();
            MyCoroutineTool.Instance.StopCoroutine(_timeSlowCoroutine);
            Time.timeScale = 1f;
            ReleasePlayerInput();
            UIManager.DestroyUI(UIType);
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

        /// <summary>
        /// 缓慢降低时间比率的协程
        /// </summary>
        /// <returns></returns>
        private IEnumerator TimeSlow()
        {
            float currentScale = Time.timeScale;
            while (true)
            {
                currentScale -= Time.deltaTime;
                Time.timeScale = currentScale;
                if (currentScale <= 0.01f) break;
                yield return null;
            }

            Time.timeScale = 0f;
        }
    }
}