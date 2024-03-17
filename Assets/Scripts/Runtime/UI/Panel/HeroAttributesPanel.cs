using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.Components;

public class HeroAttributesPanel : BasePanel
{
    public HeroAttributesPanel() : base(new UIType("Prefabs/UI/Panel/HeroAttributesPanel")) { }

    private UnityAction _Pop;
    private UnityAction _Push;

    public override void OnEnter()
    {
        //快捷键
        GameRoot.Instance.OnTab.RemoveAllListeners();
        _Pop = () => { PanelManager.Pop(); };
        _Push = () => { PanelManager.Push(new HeroAttributesPanel()); };
        GameRoot.Instance.OnTab.AddListener(_Pop);
        //游戏暂停
        GameRoot.Instance.Pause();
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
                .weaponDamageRate[WeaponType.Melee]
        };
        meleeDamage.StringReference.RefreshString();
        LocalizeStringEvent remotelyDamage = UITool.GetComponentInChildrenUI<LocalizeStringEvent>("RemotelyDamage");
        remotelyDamage.StringReference.Arguments = new object[]
        {
            GameManager.playerTans.GetComponentInChildren<HeroAttribute>()
                .weaponDamageRate[WeaponType.Remotely]
        };
        remotelyDamage.StringReference.RefreshString();
        LocalizeStringEvent magicDamage = UITool.GetComponentInChildrenUI<LocalizeStringEvent>("MagicDamage");
        magicDamage.StringReference.Arguments = new object[]
        {
            GameManager.playerTans.GetComponentInChildren<HeroAttribute>()
                .weaponDamageRate[WeaponType.Magic]
        };
        magicDamage.StringReference.RefreshString();
        LocalizeStringEvent specialDamage = UITool.GetComponentInChildrenUI<LocalizeStringEvent>("SpecialDamage");
        specialDamage.StringReference.Arguments = new object[]
        {
            GameManager.playerTans.GetComponentInChildren<HeroAttribute>()
                .weaponDamageRate[WeaponType.Special]
        };
        specialDamage.StringReference.RefreshString();
        // LocalizeStringEvent meleeRange = UITool.GetComponentInChildrenUI<LocalizeStringEvent>("AttackRange");
        // meleeRange.StringReference.Arguments = new object[] { GameManager.playerTans.GetComponentInChildren<Weapon>().attackRange };
        // meleeRange.StringReference.RefreshString();
        // LocalizeStringEvent meleeSpeed = UITool.GetComponentInChildrenUI<LocalizeStringEvent>("AttackSpeed");
        // meleeSpeed.StringReference.Arguments = new object[] { GameManager.playerTans.GetComponentInChildren<Weapon>().attackSpeed };
        // meleeSpeed.StringReference.RefreshString();
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
        GameRoot.Instance.OnTab.RemoveListener(_Pop);
        GameRoot.Instance.OnTab.RemoveListener(_Push);
        //关闭面板
        UIManager.DestroyUI(UIType);
        //游戏继续
        GameRoot.Instance.Play();
    }
}