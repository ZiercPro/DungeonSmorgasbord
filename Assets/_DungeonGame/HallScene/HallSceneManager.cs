using UnityEngine;
using ZiercCode._DungeonGame.Player;
using ZiercCode.Event;
using ZiercCode.GameTools_2D;

namespace ZiercCode._DungeonGame.HallScene
{
    public class HallSceneManager : MonoBehaviour
    {
        [SerializeField] private CameraLerpToPointer virtualCamera;
        [SerializeField] private PlayerSpawner playerSpawner;

        [SerializeField] private SceneChangeEffect.SceneChangeEffect sceneChangeEffect;

        private EventsGroup _eventsGroup = new EventsGroup();

        private void OnEnable()
        {
            _eventsGroup.AddListener<PlayerEvent.PlayerSpawned>(OnPlayerSpawned);
        }

        private void OnDisable()
        {
            _eventsGroup.RemoveAllListener();
        }

        private void Start()
        {
            virtualCamera.SetCameraTarget(playerSpawner.SpawnPlayer().transform);
            // AudioPlayer.Instance.PlayEnvironmentSfx("Audio_Environment_Hall_0");
        }

        private void OnPlayerSpawned(IEventArgs args)
        {
            if (args is PlayerEvent.PlayerSpawned playerSpawnedArgs)
                virtualCamera.SetCameraTarget(playerSpawnedArgs.PlayerObject.transform);
        }

        public void EnterGame()
        {
            SceneComponent.Instance.LoadScene("Scene_CrimsonVault", true);
        }

        public void EnterTestRoom()
        {
        }
    }
}