using UnityEngine;
using UnityEngine.Localization.Components;
using ZiercCode.Core.UI;
using ZiercCode.DungeonSmorgasbord.Component;
using ZiercCode.DungeonSmorgasbord.Weapon;
using ZiercCode.Old.Manager;

namespace ZiercCode.DungeonSmorgasbord.UI
{
    public class HeroAttributesPanel : BaseUIAnimationPanel
    {
        public HeroAttributesPanel() : base(new UIType("Prefabs/UI/Panel/HeroAttributesPanel")) { }

        public override void OnEnter()
        {
            base.OnEnter();
            //设置back
            SetAction(GetBackInputAction(), context => PanelManager.Pop());
            //设置view
            SetAction(GetViewInputAction(), context => PanelManager.Pop());
            //禁用玩家输入
            BanPlayerInput();
            //游戏暂停
            Time.timeScale = 0f;
            //属性显示
            ShowAttributes();
        }

        public override void OnExit()
        {
            base.OnExit();
            //放行玩家输入
            ReleasePlayerInput();
            //游戏继续
            Time.timeScale = 1f;
            //关闭面板
            UIManager.DestroyUI(UIType);
        }

        private void ShowAttributes()
        {
            if (!GameManager.playerTrans)
            {
                Debug.LogError("英雄组件不存在");
                return;
            }

            HeroAttribute attribute = GameManager.playerTrans.GetComponentInChildren<HeroAttribute>();
            //最大生命值
            LocalizeStringEvent maxHealth = UITool.GetComponentInChildrenUI<LocalizeStringEvent>("maxHealth");
            maxHealth.StringReference.Arguments = new object[] { attribute.AttributesData.maxHealth };
            maxHealth.StringReference.RefreshString();
            //移动速度
            LocalizeStringEvent moveSpeed = UITool.GetComponentInChildrenUI<LocalizeStringEvent>("MoveSpeed");
            moveSpeed.StringReference.Arguments = new object[] { attribute.AttributesData.moveSpeed };
            moveSpeed.StringReference.RefreshString();
            //近战攻击伤害
            LocalizeStringEvent meleeDamage = UITool.GetComponentInChildrenUI<LocalizeStringEvent>("MeleeDamage");
            meleeDamage.StringReference.Arguments =
                new object[] { attribute.AttributesData.weaponDamageRate[WeaponType.Melee] };
            meleeDamage.StringReference.RefreshString();
            //远程攻击伤害
            LocalizeStringEvent remotelyDamage = UITool.GetComponentInChildrenUI<LocalizeStringEvent>("RemotelyDamage");
            remotelyDamage.StringReference.Arguments =
                new object[] { attribute.AttributesData.weaponDamageRate[WeaponType.Remotely] };
            remotelyDamage.StringReference.RefreshString();
            //魔法伤害
            LocalizeStringEvent magicDamage = UITool.GetComponentInChildrenUI<LocalizeStringEvent>("MagicDamage");
            magicDamage.StringReference.Arguments =
                new object[] { attribute.AttributesData.weaponDamageRate[WeaponType.Magic] };
            magicDamage.StringReference.RefreshString();
            //特殊伤害
            LocalizeStringEvent specialDamage = UITool.GetComponentInChildrenUI<LocalizeStringEvent>("SpecialDamage");
            specialDamage.StringReference.Arguments =
                new object[] { attribute.AttributesData.weaponDamageRate[WeaponType.Special] };
            specialDamage.StringReference.RefreshString();
            //暴击率
            LocalizeStringEvent criticalChance = UITool.GetComponentInChildrenUI<LocalizeStringEvent>("CriticalChance");
            criticalChance.StringReference.Arguments = new object[] { attribute.AttributesData.criticalChance };
            criticalChance.StringReference.RefreshString();
        }
    }
}