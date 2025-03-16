using NaughtyAttributes.Scripts.Core.DrawerAttributes_SpecialCase;
using UnityEngine;

namespace ZiercCode._DungeonGame.Seed
{
    public class SeedTest : MonoBehaviour
    {
        [SerializeField] private string seed;
        private System.Random _random;

        [Button("生成10个数字")]
        private void GenerateRandomInt()
        {
            int seedHash = seed.GetHashCode();
            _random = new System.Random(seedHash);

            for (int i = 0; i < 10; i++)
            {
                Debug.Log(_random.Next(0, 100));
            }
        }
    }
}