using NaughtyAttributes;
using NaughtyAttributes.Scripts.Core.DrawerAttributes;
using UnityEngine;


namespace ZiercCode.Old.ScriptObject
{
    [CreateAssetMenu(menuName = "ScriptableObject/BattleData", fileName = "BattleData")]
    public class BattleDataSo : ScriptableObject
    {
        /// <summary>
        /// 战斗数据
        /// 储存所有层战斗数据
        /// </summary>
        [Expandable] public BattleDataPerLevelSo[] battleDataList;
    }
}