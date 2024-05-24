using NaughtyAttributes.Scripts.Core.DrawerAttributes;
using NaughtyAttributes.Scripts.Core.MetaAttributes;
using NaughtyAttributes.Scripts.Core.ValidatorAttributes;
using System;
using UnityEngine;
using ZiercCode.DungeonSmorgasbord.ScriptObject;

namespace ZiercCode.Old.ScriptObject
{
    /// <summary>
    /// 每波战斗数据
    /// </summary>
    [CreateAssetMenu(fileName = "Level 1", menuName = "ScriptableObject/BattleDataPerLevel")]
    public class BattleDataPerLevelSo : ScriptableObject
    {
        /// <summary>
        /// 要生成的敌人
        /// </summary>
        public EnemyAndNum[] enemyToSpawn;

        /// <summary>
        /// 波次
        /// </summary>
        [MinValue(1)] public int waves;

        private bool _showWaveInternal => waves > 1;

        /// <summary>
        /// 波次间隔
        /// </summary>
        [ShowIf("_showWaveInternal")] public int waveInterval;

        [Serializable]
        public struct EnemyAndNum
        {
            public EnemyAttributeSo enemyAttributeSo;
            public int num;
        }
    }
}