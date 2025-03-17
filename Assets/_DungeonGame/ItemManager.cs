using UnityEngine;
using UnityEngine.InputSystem;

namespace ZiercCode._DungeonGame
{
    public class ItemManager : MonoBehaviour //管理掉落物
    {
        [SerializeField] private GameObject item;
        [SerializeField] private Transform spawnPoint;

        private PlayerInputAction _playerInputAction;

        private void Awake()
        {
            _playerInputAction = new PlayerInputAction();
        }

        private void OnEnable()
        {
            _playerInputAction.HeroControl.Enable();
            _playerInputAction.HeroControl.Dash.started += SpawnItem;
        }

        private void OnDisable()
        {
            _playerInputAction.HeroControl.Dash.started -= SpawnItem;
            _playerInputAction.HeroControl.Disable();
        }

        private void SpawnItem(InputAction.CallbackContext context)
        {
            GameObject newItem = Instantiate(item, spawnPoint.position, Quaternion.identity);
        }
    }
}