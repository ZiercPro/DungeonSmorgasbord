using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using ZiercCode._DungeonGame._Scripts.AttackSystem;
using ZiercCode._DungeonGame._Scripts.WeaponClass;
using ZiercCode.Utilities;

namespace ZiercCode._DungeonGame._Scripts.Editor
{
    public class WeaponConfigTool : EditorWindow
    {
        [SerializeField]
        private VisualTreeAsset uiTree;

        [MenuItem("DungeonTools/WeaponConfigTool")]
        public static void ShowWindow()
        {
            var win = GetWindow<WeaponConfigTool>();
            GUIContent title = new GUIContent("WeaponConfigTool");
            Vector2 size = new Vector2(600, 800);
            win.titleContent = title;
            win.minSize = size;
            win.minSize = size;
        }


        private void CreateGUI()
        {
            uiTree.CloneTree(rootVisualElement);

            Button readConfigButton = rootVisualElement.Q<Button>("ReadConfig");

            readConfigButton.RegisterCallback<ClickEvent>(ReadConfigButtonPressed);
        }

        private void ReadConfigButtonPressed(ClickEvent e)
        {
            TextField configFilePath = rootVisualElement.Q<TextField>("ConfigFilePath");
            TextField weaponFilePath = rootVisualElement.Q<TextField>("WeaponFilePath");
            string configPath = configFilePath.value;
            string weaponPath = weaponFilePath.value;

            TextAsset configFile = AssetDatabase.LoadAssetAtPath<TextAsset>(configPath);

            string[] lines = configFile.text.Split('\n');
            for (int i = 2; i < lines.Length; i++)
            {
                string[] rows = lines[i].Split(',');
                if (rows[1].Equals("")) break;

                string weaponPackageName = rows[3];
                GameObject weapon = AssetDatabase.LoadAssetAtPath<GameObject>($"{weaponPath}/{weaponPackageName}.prefab");
                if (!weapon) continue;
                BaseWeapon baseWeapon = weapon.GetComponent<BaseWeapon>();
                if (!baseWeapon) continue;
                Debug.Log(weaponPackageName);
                baseWeapon.weaponName = rows[2];
                //配置伤害
                baseWeapon.damage.dictionaryList.Clear();
                float.TryParse(rows[4], out float damage);
                baseWeapon.damage.dictionaryList.Add(
                    new EditableDictionary<DamageType, float>.EditableDictionaryItem<DamageType, float>(
                        DamageType.Fire, damage));
                float.TryParse(rows[5], out damage);
                baseWeapon.damage.dictionaryList.Add(
                    new EditableDictionary<DamageType, float>.EditableDictionaryItem<DamageType, float>(
                        DamageType.Ice, damage));
                float.TryParse(rows[6], out damage);
                baseWeapon.damage.dictionaryList.Add(
                    new EditableDictionary<DamageType, float>.EditableDictionaryItem<DamageType, float>(
                        DamageType.Wood, damage));
                float.TryParse(rows[7], out damage);
                baseWeapon.damage.dictionaryList.Add(
                    new EditableDictionary<DamageType, float>.EditableDictionaryItem<DamageType, float>(
                        DamageType.Voice, damage));
                float.TryParse(rows[8], out damage);
                baseWeapon.damage.dictionaryList.Add(
                    new EditableDictionary<DamageType, float>.EditableDictionaryItem<DamageType, float>(
                        DamageType.Electric, damage));
                float.TryParse(rows[9], out damage);
                baseWeapon.damage.dictionaryList.Add(
                    new EditableDictionary<DamageType, float>.EditableDictionaryItem<DamageType, float>(
                        DamageType.Poison, damage));
                float.TryParse(rows[10], out damage);
                baseWeapon.damage.dictionaryList.Add(
                    new EditableDictionary<DamageType, float>.EditableDictionaryItem<DamageType, float>(
                        DamageType.Wind, damage));
                float.TryParse(rows[11], out damage);
                baseWeapon.damage.dictionaryList.Add(
                    new EditableDictionary<DamageType, float>.EditableDictionaryItem<DamageType, float>(
                        DamageType.Void, damage));
                //击退
                baseWeapon.hitForce = float.Parse(rows[12]);
                //暴击率
                baseWeapon.criticalChance = float.Parse(rows[13]);
                //暴击倍率
                baseWeapon.criticalDamageRate = float.Parse(rows[14]);
                //触发几率
                baseWeapon.triggerChance = float.Parse(rows[15]);
                //伤害衰减
                baseWeapon.damageReductionByDistance = float.Parse(rows[16]);
                //射速
                baseWeapon.shootSpeed = float.Parse(rows[17]);
                //攻击距离
                baseWeapon.shootDistance = float.Parse(rows[18]);
                //弹匣容量
                baseWeapon.magazineCapacity = int.Parse(rows[19]);
                //装填时间
                baseWeapon.reloadTime = float.Parse(rows[20]);
                //精准度
                baseWeapon.accuracy = float.Parse(rows[21]);
                //射弹数量
                baseWeapon.projectileNumPerShoot = float.Parse(rows[22]);
                //弹速
                baseWeapon.projectileSpeed = float.Parse(rows[23]);
                //射弹大小
                baseWeapon.projectileSize = float.Parse(rows[24]);

                EditorUtility.SetDirty(weapon);
            }

            AssetDatabase.SaveAssets();
            Debug.Log("读取完毕!");
        }
    }
}