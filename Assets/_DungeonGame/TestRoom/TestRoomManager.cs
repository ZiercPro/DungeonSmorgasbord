using UnityEngine;
using ZiercCode._DungeonGame._Scripts;
using ZiercCode._DungeonGame.Config;
using ZiercCode._DungeonGame.Player;

namespace ZiercCode._DungeonGame.TestRoom
{
    public class TestRoomManager : MonoBehaviour
    {
        [SerializeField]
        private TriggerArea exit;

        [Space]
        [SerializeField]
        private PlayerSpawner playerSpawner;

        private void OnEnable()
        {
            exit.onPlayerEnter.AddListener(Exit);
        }

        private void OnDisable()
        {
            exit.onPlayerEnter.RemoveListener(Exit);
        }

        private void Start()
        {
            playerSpawner.SpawnPlayerWithWeapon(
                ConfigComponent.Instance.GameConfig.CurrentPlayerName,
                ConfigComponent.Instance.GameConfig.CurrentWeaponName);
        }

        private void Exit()
        {
            SceneComponent.Instance.LoadScene("Scene_Hall", true);
        }
    }
}