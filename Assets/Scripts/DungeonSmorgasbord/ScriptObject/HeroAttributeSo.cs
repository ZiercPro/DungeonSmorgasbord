using UnityEngine;
using ZiercCode.Core.Extend;
using ZiercCode.DungeonSmorgasbord.Weapon;

namespace ZiercCode.DungeonSmorgasbord.ScriptObject
{
    /// <summary>
    /// 英雄属性 
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptObject/Attributes/Hero", fileName = "Hero")]
    public class HeroAttributeSo : CreatureAttributesSo
    {
        /// <summary>
        /// 武器伤害率数据表
        /// </summary>
        [SerializeField] private TextAsset weaponDamageDataFile;

        /// <summary>
        /// 武器伤害率字典
        /// </summary>
        public EditableDictionary<WeaponType, float> weaponDamageRate;

        //脚本被调用，或者在inspector界面变更时，会被调用（仅在编辑器中调用
        private void OnValidate()
        {
            if (!weaponDamageDataFile) return;

            weaponDamageRate = new EditableDictionary<WeaponType, float>();
            //按行分割 会多出一行空串
            string[] allLines = weaponDamageDataFile.text.Split('\n');
            //每行按照 , 分割
            for (int i = 1; i < allLines.Length - 1; i++)
            {
                string[] value = allLines[i].Split(',');
                weaponDamageRate.Add(WeaponType.Melee, float.Parse(value[0]));
                weaponDamageRate.Add(WeaponType.Magic, float.Parse(value[1]));
                weaponDamageRate.Add(WeaponType.Special, float.Parse(value[2]));
                weaponDamageRate.Add(WeaponType.Remotely, float.Parse(value[3]));
            }
        }
    }
}