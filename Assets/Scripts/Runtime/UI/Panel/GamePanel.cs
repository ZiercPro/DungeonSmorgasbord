using DG.Tweening;
using System;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
using ZiercCode.Runtime.Component;
using ZiercCode.Runtime.Manager;
using ZiercCode.Runtime.UI.Framework;

namespace ZiercCode.Runtime.UI.Panel
{
    public class GamePanel : BasePanel
    {
        private const string Path = "Prefabs/UI/Panel/GameMenu";
        public GamePanel() : base(new UIType(Path)) { }

        private Action _healthBarInitAction;
        private Action<int> _coinUpdateAction;
        private Action<int> _levelUpdateAction;
        private Action<InputAction.CallbackContext> _escAction;
        private Action<InputAction.CallbackContext> _tabAction;

        private PlayerInputAction _playerInputAction;

        private Tweener _coinGetShake;

        public override void OnEnter()
        {
            _playerInputAction = new PlayerInputAction();
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
                    GameManager.playerTans.GetComponentInChildren<Health>().maxHealth;
                UITool.GetComponentInChildrenUI<Slider>("HealthBar").value =
                    GameManager.playerTans.GetComponentInChildren<Health>().currentHealth;
                UITool.GetComponentInChildrenUI<TextMeshProUGUI>("HealthBarMax").text =
                    GameManager.playerTans.GetComponentInChildren<Health>().maxHealth.ToString();
                UITool.GetComponentInChildrenUI<TextMeshProUGUI>("HealthBarCurrent").text = GameManager.playerTans
                    .GetComponentInChildren<Health>().currentHealth.ToString();
                GameManager.playerTans.GetComponent<Health>().MaxHealthChanged += max =>
                {
                    UITool.GetComponentInChildrenUI<Slider>("HealthBar").maxValue = max;
                };
                GameManager.playerTans.GetComponent<Health>().CurrentHealthChanged += current =>
                {
                    UITool.GetComponentInChildrenUI<Slider>("HealthBar").value = current;
                };
                GameManager.playerTans.GetComponent<Health>().MaxHealthChanged += max =>
                {
                    UITool.GetComponentInChildrenUI<TextMeshProUGUI>("HealthBarMax").text = max.ToString();
                };
                GameManager.playerTans.GetComponent<Health>().CurrentHealthChanged += current =>
                {
                    UITool.GetComponentInChildrenUI<TextMeshProUGUI>("HealthBarCurrent").text = current.ToString();
                };
            };
            SetEscEvent();
            BattleManager.Instance.OnLevelChange += _levelUpdateAction;
            GameManager.playerTans.GetComponent<Health>().InitializeEnded += _healthBarInitAction;
            GameManager.playerTans.GetComponent<Hero.Hero>().CoinPack.CoinChanged += _coinUpdateAction;
            UITool.GetComponentInChildrenUI<TextMeshProUGUI>("FPS").enabled = DataManager.SettingsData.FPSOn;
        }

        public override void OnPause()
        {
            DeleteEscEvent();
            GameManager.playerTans.GetComponent<Health>().InitializeEnded -= _healthBarInitAction;
        }

        public override void OnResume()
        {
            SetEscEvent();
            UITool.GetComponentInChildrenUI<TextMeshProUGUI>("FPS").enabled = DataManager.SettingsData.FPSOn;
            GameManager.playerTans.GetComponent<Health>().InitializeEnded += _healthBarInitAction;
        }

        public override void OnExit()
        {
            GameManager.playerTans.GetComponent<Health>().InitializeEnded -= _healthBarInitAction;
            GameManager.playerTans.GetComponent<Hero.Hero>().CoinPack.CoinChanged -= _coinUpdateAction;
            BattleManager.Instance.OnLevelChange -= _levelUpdateAction;
            DeleteEscEvent();
            UIManager.DestroyUI(UIType);
        }

        private void SetEscEvent()
        {
            _escAction = e => { PanelManager.Push(new PausePanel()); };
            _playerInputAction.ShortKey.Enable();
            _playerInputAction.ShortKey.Back.performed += _escAction;
        }

        private void DeleteEscEvent()
        {
            _playerInputAction.ShortKey.Back.performed -= _escAction;
            _playerInputAction.ShortKey.Disable();
        }
        
    }
}