using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using ZiercCode.Runtime.Helper;
using ZiercCode.Runtime.Weapon;

namespace ZiercCode.Runtime.ScriptObject
{
    /// <summary>
    /// 英雄属性 为了方便初始化 利用了可编辑物品
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptObj/Attributes/Hero", fileName = "Hero")]
    public class HeroAttributeSo : AttributesBaseSo
    {
        //信息文件
        [SerializeField] private TextAsset weaponDamageDataFile;
        [field: SerializeField] public EditableDictionary<WeaponType, float> WeaponDamageRate { get; private set; }
        public float criticalChance;


        //脚本被调用，或者在inspector界面变更时，会被调用（仅在编辑器中调用
        private void OnValidate()
        {
            if (!weaponDamageDataFile) return;

            WeaponDamageRate = new EditableDictionary<WeaponType, float>();
            //按行分割 会多出一行空串
            string[] allLines = weaponDamageDataFile.text.Split('\n');
            //每行按照 , 分割
            for (int i = 1; i < allLines.Length - 1; i++)
            {
                string[] value = allLines[i].Split(',');
                WeaponDamageRate.Add(WeaponType.Melee, float.Parse(value[0]));
                WeaponDamageRate.Add(WeaponType.Magic, float.Parse(value[1]));
                WeaponDamageRate.Add(WeaponType.Special, float.Parse(value[2]));
                WeaponDamageRate.Add(WeaponType.Remotely, float.Parse(value[3]));
            }
        }
    }
}