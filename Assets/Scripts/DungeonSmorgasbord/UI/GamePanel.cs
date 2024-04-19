using DG.Tweening;
using System;
using TMPro;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
using ZiercCode.Core.UI;
using ZiercCode.Old.Component;
using ZiercCode.Old.Manager;

namespace ZiercCode.DungeonSmorgasbord.UI
{
    public class GamePanel : BaseInputActionPanel
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
            _healthBarInitAction = () =>
            {
                UITool.GetComponentInChildrenUI<Slider>("HealthBar").minValue = 0;
                UITool.GetComponentInChildrenUI<Slider>("HealthBar").maxValue =
                    GameManager.playerTrans.GetComponentInChildren<Health>().maxHealth;
                UITool.GetComponentInChildrenUI<Slider>("HealthBar").value =
                    GameManager.playerTrans.GetComponentInChildren<Health>().currentHealth;
                UITool.GetComponentInChildrenUI<TextMeshProUGUI>("HealthBarMax").text =
                    GameManager.playerTrans.GetComponentInChildren<Health>().maxHealth.ToString();
                UITool.GetComponentInChildrenUI<TextMeshProUGUI>("HealthBarCurrent").text = GameManager.playerTrans
                    .GetComponentInChildren<Health>().currentHealth.ToString();
                GameManager.playerTrans.GetComponent<Health>().MaxHealthChanged += max =>
                {
                    UITool.GetComponentInChildrenUI<Slider>("HealthBar").maxValue = max;
                };
                GameManager.playerTrans.GetComponent<Health>().CurrentHealthChanged += current =>
                {
                    UITool.GetComponentInChildrenUI<Slider>("HealthBar").value = current;
                };
                GameManager.playerTrans.GetComponent<Health>().MaxHealthChanged += max =>
                {
                    UITool.GetComponentInChildrenUI<TextMeshProUGUI>("HealthBarMax").text = max.ToString();
                };
                GameManager.playerTrans.GetComponent<Health>().CurrentHealthChanged += current =>
                {
                    UITool.GetComponentInChildrenUI<TextMeshProUGUI>("HealthBarCurrent").text = current.ToString();
                };
            };
            BattleManager.Instance.OnLevelChange += _levelUpdateAction;
            GameManager.playerTrans.GetComponent<Health>().InitializeEnded += _healthBarInitAction;
            GameManager.playerTrans.GetComponent<Old.Hero.Hero>().CoinPack.CoinChanged += _coinUpdateAction;
            UITool.GetComponentInChildrenUI<TextMeshProUGUI>("FPS").enabled = ConfigManager.SettingsData.FPSOn;
        }

        public override void OnPause()
        {
            base.OnPause();
            GameManager.playerTrans.GetComponent<Health>().InitializeEnded -= _healthBarInitAction;
        }

        public override void OnResume()
        {
            base.OnResume();
            UITool.GetComponentInChildrenUI<TextMeshProUGUI>("FPS").enabled = ConfigManager.SettingsData.FPSOn;
            GameManager.playerTrans.GetComponent<Health>().InitializeEnded += _healthBarInitAction;
        }

        public override void OnExit()
        {
            base.OnExit();
            GameManager.playerTrans.GetComponent<Health>().InitializeEnded -= _healthBarInitAction;
            GameManager.playerTrans.GetComponent<Old.Hero.Hero>().CoinPack.CoinChanged -= _coinUpdateAction;
            BattleManager.Instance.OnLevelChange -= _levelUpdateAction;
            UIManager.DestroyUI(UIType);
        }
    }
}