using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Localization.Components;
using ZiercCode.Runtime.Component;
using ZiercCode.Runtime.Component.Hero;
using ZiercCode.Runtime.Manager;
using ZiercCode.Runtime.UI.Framework;
using ZiercCode.Runtime.Weapon;

namespace ZiercCode.Runtime.UI.Panel
{
    public class HeroAttributesPanel : BasePanel
    {
        public HeroAttributesPanel() : base(new UIType("Prefabs/UI/Panel/HeroAttributesPanel")) { }

        private PlayerInputAction _playerInputAction;
        private Action<InputAction.CallbackContext> _tabAction;

        public override void OnEnter()
        {
            _playerInputAction = new PlayerInputAction();
            //快捷键
            SetTabAction();
            //游戏暂停
            Time.timeScale = 0f;
            //属性显示
            LocalizeStringEvent maxHealth = UITool.GetComponentInChildrenUI<LocalizeStringEvent>("MaxHealth");
            maxHealth.StringReference.Arguments =
                new object[] { GameManager.playerTans.GetComponentInChildren<Health>().maxHealth };
            maxHealth.StringReference.RefreshString();
            LocalizeStringEvent moveSpeed = UITool.GetComponentInChildrenUI<LocalizeStringEvent>("MoveSpeed");
            moveSpeed.StringReference.Arguments =
                new object[] { GameManager.playerTans.GetComponentInChildren<Movement>().moveSpeed };
            moveSpeed.StringReference.RefreshString();
            LocalizeStringEvent meleeDamage = UITool.GetComponentInChildrenUI<LocalizeStringEvent>("MeleeDamage");
            meleeDamage.StringReference.Arguments = new object[]
            {
                GameManager.playerTans.GetComponentInChildren<HeroAttribute>()
                    .WeaponDamageRate[WeaponType.Melee]
            };
            meleeDamage.StringReference.RefreshString();
            LocalizeStringEvent remotelyDamage = UITool.GetComponentInChildrenUI<LocalizeStringEvent>("RemotelyDamage");
            remotelyDamage.StringReference.Arguments = new object[]
            {
                GameManager.playerTans.GetComponentInChildren<HeroAttribute>()
                    .WeaponDamageRate[WeaponType.Remotely]
            };
            remotelyDamage.StringReference.RefreshString();
            LocalizeStringEvent magicDamage = UITool.GetComponentInChildrenUI<LocalizeStringEvent>("MagicDamage");
            magicDamage.StringReference.Arguments = new object[]
            {
                GameManager.playerTans.GetComponentInChildren<HeroAttribute>()
                    .WeaponDamageRate[WeaponType.Magic]
            };
            magicDamage.StringReference.RefreshString();
            LocalizeStringEvent specialDamage = UITool.GetComponentInChildrenUI<LocalizeStringEvent>("SpecialDamage");
            specialDamage.StringReference.Arguments = new object[]
            {
                GameManager.playerTans.GetComponentInChildren<HeroAttribute>()
                    .WeaponDamageRate[WeaponType.Special]
            };
            specialDamage.StringReference.RefreshString();
            LocalizeStringEvent criticalChance = UITool.GetComponentInChildrenUI<LocalizeStringEvent>("CriticalChance");
            criticalChance.StringReference.Arguments = new object[]
            {
                GameManager.playerTans.GetComponentInChildren<HeroAttribute>().criticalChance
            };
            criticalChance.StringReference.RefreshString();
        }

        public override void OnExit()
        {
            //快捷键
            DeleteTabAction();
            //关闭面板
            UIManager.DestroyUI(UIType);
            //游戏继续
            Time.timeScale = 1f;
        }

        private void SetTabAction()
        {
            _tabAction = e => { PanelManager.Pop(); };
            _playerInputAction.ShortKey.Enable();
            _playerInputAction.ShortKey.View.performed += _tabAction;
        }

        private void DeleteTabAction()
        {
            _playerInputAction.ShortKey.View.performed -= _tabAction;
            _playerInputAction.ShortKey.Disable();
        }
    }
}