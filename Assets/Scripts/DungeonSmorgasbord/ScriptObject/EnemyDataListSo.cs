using System.Collections.Generic;
using UnityEngine;
using ZiercCode.DungeonSmorgasbord.ScriptObject;

namespace ZiercCode
{
    [CreateAssetMenu(menuName = "ScriptObject/EnemyDataList",fileName = "EnemyDataList")]
    public class EnemyDataListSo : ScriptableObject
    {
        public List<EnemyAttributeSo> enemyAttributeSoList;
    }
}
