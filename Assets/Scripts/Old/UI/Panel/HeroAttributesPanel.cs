using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization.Components;
using ZiercCode.Core.UI;
using ZiercCode.Old.Component;
using ZiercCode.Old.Component.Hero;
using ZiercCode.Old.Hero;
using ZiercCode.Old.Manager;
using ZiercCode.Old.Weapon;

namespace ZiercCode.Old.UI.Panel
{
    public class HeroAttributesPanel : BasePanel
    {
        public HeroAttributesPanel() : base(new UIType("Prefabs/UI/Panel/HeroAttributesPanel")) { }

        private HeroInputManager _heroInputManager;
        private PlayerInputAction _playerInputAction;
        private Action<InputAction.CallbackContext> _tabAction;

        public override void OnEnter()
        {
            //禁用玩家输入
            BanPlayerInput();
            _playerInputAction = new PlayerInputAction();
            //快捷键
            // SetTabAction();
            //游戏暂停
            Time.timeScale = 0f;
            //属性显示
            LocalizeStringEvent maxHealth = UITool.GetComponentInChildrenUI<LocalizeStringEvent>("MaxHealth");
            maxHealth.StringReference.Arguments =
                new object[] { GameManager.playerTrans.GetComponentInChildren<Health>().maxHealth };
            maxHealth.StringReference.RefreshString();
            LocalizeStringEvent moveSpeed = UITool.GetComponentInChildrenUI<LocalizeStringEvent>("MoveSpeed");
            moveSpeed.StringReference.Arguments =
                new object[] { GameManager.playerTrans.GetComponentInChildren<Movement>().MoveSpeed };
            moveSpeed.StringReference.RefreshString();
            LocalizeStringEvent meleeDamage = UITool.GetComponentInChildrenUI<LocalizeStringEvent>("MeleeDamage");
            meleeDamage.StringReference.Arguments = new object[]
            {
                GameManager.playerTrans.GetComponentInChildren<HeroAttribute>()
                    .WeaponDamageRate[WeaponType.Melee]
            };
            meleeDamage.StringReference.RefreshString();
            LocalizeStringEvent remotelyDamage = UITool.GetComponentInChildrenUI<LocalizeStringEvent>("RemotelyDamage");
            remotelyDamage.StringReference.Arguments = new object[]
            {
                GameManager.playerTrans.GetComponentInChildren<HeroAttribute>()
                    .WeaponDamageRate[WeaponType.Remotely]
            };
            remotelyDamage.StringReference.RefreshString();
            LocalizeStringEvent magicDamage = UITool.GetComponentInChildrenUI<LocalizeStringEvent>("MagicDamage");
            magicDamage.StringReference.Arguments = new object[]
            {
                GameManager.playerTrans.GetComponentInChildren<HeroAttribute>()
                    .WeaponDamageRate[WeaponType.Magic]
            };
            magicDamage.StringReference.RefreshString();
            LocalizeStringEvent specialDamage = UITool.GetComponentInChildrenUI<LocalizeStringEvent>("SpecialDamage");
            specialDamage.StringReference.Arguments = new object[]
            {
                GameManager.playerTrans.GetComponentInChildren<HeroAttribute>()
                    .WeaponDamageRate[WeaponType.Special]
            };
            specialDamage.StringReference.RefreshString();
            LocalizeStringEvent criticalChance = UITool.GetComponentInChildrenUI<LocalizeStringEvent>("CriticalChance");
            criticalChance.StringReference.Arguments = new object[]
            {
                GameManager.playerTrans.GetComponentInChildren<HeroAttribute>().criticalChance
            };
            criticalChance.StringReference.RefreshString();
        }

        public override void OnExit()
        {
            //快捷键
            //DeleteTabAction();
            //关闭面板
            UIManager.DestroyUI(UIType);
            //放行玩家输入
            //  ReleaseHeroInput();
            //游戏继续
            Time.timeScale = 1f;
        }

        // private void SetTabAction()
        // {
        //     _tabAction = e => { PanelManager.PopAll(); };
        //     _playerInputAction.HeroControl.Enable();
        //     _playerInputAction.HeroControl.View.performed += _tabAction;
        // }
        //
        // private void DeleteTabAction()
        // {
        //     _playerInputAction.HeroControl.View.performed -= _tabAction;
        //     _playerInputAction.HeroControl.Disable();
        // }
    }
}