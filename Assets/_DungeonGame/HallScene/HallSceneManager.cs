using UnityEngine;
using ZiercCode._DungeonGame._Scripts;
using ZiercCode._DungeonGame.Config;
using ZiercCode._DungeonGame.Player;
using ZiercCode.GameTools_2D;


namespace ZiercCode._DungeonGame.HallScene
{
    //处理Hall场景中特定的逻辑
    public class HallSceneManager : MonoBehaviour
    {
        [SerializeField]
        private BaseDoor gameStartDoor;

        [SerializeField]
        private BaseDoor testRoomDoor;

        [Space]
        //--//
        [SerializeField]
        private CameraLerpToPointer virtualCamera;

        [SerializeField]
        private PlayerSpawner defaultPlayerSpawner;

        [SerializeField]
        private PlayerSpawner testRoomPlayerSpawner;

        //[SerializeField] private SceneChangeEffect.SceneChangeEffect sceneChangeEffect;

        //private EventsGroup _eventsGroup = new EventsGroup();

        private void OnEnable()
        {
            // _eventsGroup.AddListener<PlayerEvent.PlayerSpawned>(OnPlayerSpawned);
            //绑定场景中门的事件
            gameStartDoor.onPlayerEnter.AddListener(EnterGame);
            testRoomDoor.onPlayerEnter.AddListener(EnterTestRoom);

            //绑定场景转换事件
            SceneComponent.Instance.onSceneLoaded.AddListener(EnterHall);
        }

        private void OnDisable()
        {
            //_eventsGroup.RemoveAllListener();

            gameStartDoor.onPlayerEnter.RemoveListener(EnterGame);
            testRoomDoor.onPlayerEnter.RemoveListener(EnterTestRoom);

            SceneComponent.Instance.onSceneLoaded.RemoveListener(EnterHall);
        }

        private void Start()
        {
            // AudioPlayer.Instance.PlayEnvironmentSfx("Audio_Environment_Hall_0");
        }

        private void EnterHall(string sceneName)
        {
            if (sceneName == "Scene_MainMenu" || sceneName == "Scene_GameEntry")
            {
                virtualCamera.SetCameraTarget(defaultPlayerSpawner.SpawnPlayerWithWeapon(
                        ConfigComponent.Instance.GameConfig.CurrentPlayerName,
                        ConfigComponent.Instance.GameConfig.CurrentWeaponName)
                    .transform);
            }
            else if (sceneName == "Scene_TestRoom")
            {
                virtualCamera.SetCameraTarget(testRoomPlayerSpawner.SpawnPlayerWithWeapon(
                        ConfigComponent.Instance.GameConfig.CurrentPlayerName,
                        ConfigComponent.Instance.GameConfig.CurrentWeaponName)
                    .transform);
            }
        }

        private void EnterGame()
        {
            // SceneComponent.Instance.LoadScene("Scene_CrimsonVault", true);
        }

        private void EnterTestRoom()
        {
            SceneComponent.Instance.LoadScene("Scene_TestRoom", true);
        }
    }
}