using System.Collections.Generic;
using UnityEngine;

namespace ZiercCode.DungeonSmorgasbord.ScriptObject
{
    [CreateAssetMenu(menuName = "ScriptObject/EnemyDataList",fileName = "EnemyDataList")]
    public class EnemyDataListSo : ScriptableObject
    {
        public List<EnemyAttributeSo> enemyAttributeSoList;
    }
}
