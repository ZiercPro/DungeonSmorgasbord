using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace ZiercCode._DungeonGame.Player
{
    public class PlayerSpawner : MonoBehaviour
    {
        public GameObject SpawnPlayer(string playerName)
        {
            AsyncOperationHandle<GameObject> load = Addressables.LoadAssetAsync<GameObject>(playerName);
            load.WaitForCompletion();
            GameObject player = Instantiate(load.Result);
            load.Release();
            player.transform.position = transform.position;
            return player;
        }

        public GameObject SpawnPlayerWithWeapon(string playerName, string weaponName)
        {
            GameObject newPlayer = SpawnPlayer(playerName);

            AsyncOperationHandle<GameObject> load = Addressables.LoadAssetAsync<GameObject>(weaponName);
            load.WaitForCompletion();
            GameObject newWeapon = Instantiate(load.Result);
            load.Release();
            newPlayer.GetComponent<Player_Base>().SetWeapon(newWeapon.transform);

            return newPlayer;
        }
    }
}