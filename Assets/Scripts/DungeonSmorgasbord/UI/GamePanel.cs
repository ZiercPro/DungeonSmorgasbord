using DG.Tweening;
using System;
using TMPro;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
using ZiercCode.Core.UI;
using ZiercCode.DungeonSmorgasbord.Manager;
using ZiercCode.Old.Component;
using ZiercCode.Old.Manager;

namespace ZiercCode.DungeonSmorgasbord.UI
{
    public class GamePanel : BaseUIInputActionPanel
    {
        private const string Path = "Prefabs/UI/Panel/GameMenu";
        public GamePanel() : base(new UIType(Path)) { }

        private Action _healthBarInitAction;
        private Action<int> _coinUpdateAction;
        private Action<int> _levelUpdateAction;

        private Tweener _coinGetShake;

        public override void OnEnter()
        {
            base.OnEnter();
            //设置回退事件
            SetAction(GetBackInputAction(), context => PanelManager.Push(new PausePanel()));

            //设置视图事件
            SetAction(GetViewInputAction(), context => PanelManager.Push(new HeroAttributesPanel()));

            _levelUpdateAction = level =>
            {
                LocalizeStringEvent levelText = UITool.GetComponentInChildrenUI<LocalizeStringEvent>("Level");
                levelText.StringReference.Arguments = new object[] { level };
                levelText.StringReference.RefreshString();
            };
            _coinUpdateAction = coin =>
            {
                LocalizeStringEvent coinText = UITool.GetComponentInChildrenUI<LocalizeStringEvent>("Coin");
                coinText.StringReference.Arguments = new object[] { coin };
                coinText.StringReference.RefreshString();
                if (coin != 0 && (_coinGetShake == null || !_coinGetShake.IsActive()))
                {
                    _coinGetShake = coinText.transform.DOShakePosition(0.8f, 20f, 20).SetAutoKill(true);
                }
            };
            //生命值
            UITool.GetComponentInChildrenUI<Slider>("HealthBar").minValue = 0;
            UITool.GetComponentInChildrenUI<Slider>("HealthBar").maxValue =
                GameManager.playerTrans.GetComponentInChildren<Health>().GetMaxHealth();
            UITool.GetComponentInChildrenUI<Slider>("HealthBar").value =
                GameManager.playerTrans.GetComponentInChildren<Health>().GetCurrentHealth();
            UITool.GetComponentInChildrenUI<TextMeshProUGUI>("HealthBarMax").text =
                GameManager.playerTrans.GetComponentInChildren<Health>().GetMaxHealth().ToString();
            UITool.GetComponentInChildrenUI<TextMeshProUGUI>("HealthBarCurrent").text = GameManager.playerTrans
                .GetComponentInChildren<Health>().GetCurrentHealth().ToString();

            GameManager.playerTrans.GetComponent<Health>().HealthChanged += args =>
            {
                UITool.GetComponentInChildrenUI<Slider>("HealthBar").maxValue = args.MaxHealth;
            };
            GameManager.playerTrans.GetComponent<Health>().HealthChanged += args =>
            {
                UITool.GetComponentInChildrenUI<Slider>("HealthBar").value = args.CurrentHealth;
            };
            GameManager.playerTrans.GetComponent<Health>().HealthChanged += args =>
            {
                UITool.GetComponentInChildrenUI<TextMeshProUGUI>("HealthBarMax").text = args.MaxHealth.ToString();
            };
            GameManager.playerTrans.GetComponent<Health>().HealthChanged += args =>
            {
                UITool.GetComponentInChildrenUI<TextMeshProUGUI>("HealthBarCurrent").text =
                    args.CurrentHealth.ToString();
            };

            // BattleManager.Instance.OnLevelChange += _levelUpdateAction;
            GameManager.playerTrans.GetComponent<Old.Hero.Hero>().CoinPack.CoinChanged += _coinUpdateAction;
            UITool.GetComponentInChildrenUI<TextMeshProUGUI>("FPS").enabled =
                DataManager.SettingsData.FPSOn;
        }

        public override void OnResume()
        {
            base.OnResume();
            UITool.GetComponentInChildrenUI<TextMeshProUGUI>("FPS").enabled =
                DataManager.SettingsData.FPSOn;
        }

        public override void OnExit()
        {
            base.OnExit();
            GameManager.playerTrans.GetComponent<Old.Hero.Hero>().CoinPack.CoinChanged -= _coinUpdateAction;
            // BattleManager.Instance.OnLevelChange -= _levelUpdateAction;
            UIManager.DestroyUI(UIType);
        }
    }
}