using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace ZiercCode._DungeonGame.Player
{
    public class PlayerSpawner : MonoBehaviour
    {
        public GameObject SpawnPlayer()
        {
            AsyncOperationHandle<GameObject> load = Addressables.LoadAssetAsync<GameObject>("Player_Gawain");
            load.WaitForCompletion();
            GameObject player = Instantiate(load.Result);
            player.transform.position = transform.position;
            return player;
        }
    }
}