using UnityEngine;
using ZiercCode.Runtime.Helper;
using ZiercCode.Runtime.Weapon;

namespace ZiercCode.Runtime.ScriptObject
{


    /// <summary>
    /// 英雄属性 为了方便初始化 利用了可编辑物品
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptObj/Attributes/Hero", fileName = "Hero")]
    public class HeroAttributeSO : AttributesBaseSO
    {
        //信息文件
        public TextAsset weaponDamageDataFile;
        public SerDictionary<WeaponType, float> weaponDamageRate;
        public float criticalChance;

        private void OnEnable()
        {
            if (!weaponDamageDataFile) return;

            weaponDamageRate = new SerDictionary<WeaponType, float>();
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

        //脚本被调用，或者在inspector界面变更时，会被调用（仅在编辑器中调用
        private void OnValidate()
        {
            if (!weaponDamageDataFile) return;

            weaponDamageRate = new SerDictionary<WeaponType, float>();
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