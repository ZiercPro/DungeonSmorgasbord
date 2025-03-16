using UnityEngine;
using ZiercCode._DungeonGame.Room;
using ZiercCode.Event;
using ZiercCode.GameTools_2D;
using ZiercCode.Utilities;

namespace ZiercCode._DungeonGame.CrimsonVault
{
    public class CrimsonVaultSceneManager : MonoBehaviour
    {
        [SerializeField] private RandomRoom roomBuilder;
        [SerializeField] private CameraLerpToPointer cameraLerpToPointer;

        [SerializeField] private EditableDictionary<RandomRoom.RoomType, GameObject> roomDic;

        private int _currentRoomIndex;
        private GameObject[] _roomInstances;
        private RandomRoom.RoomInfo[] _roomInfos;
        private Room.Room[] _roomManagers;

        private GameObject _player;

        private EventsGroup _eventsGroup = new EventsGroup();

        private void OnEnable()
        {
            _eventsGroup.AddListener<PlayerEvent.PlayerSpawned>(OnPlayerSpawned);
            _eventsGroup.AddListener<RoomEvent.EnterNextRoom>(OnEnterNextRoom);
        }

        private void OnDisable()
        {
            _eventsGroup.RemoveAllListener();
        }


        // //重新生成该场景
        // private void ReStart()
        // {
        //     Destroy(_player);
        //
        //     _roomInfos = roomBuilder.SpawnRoomList();
        //     for (int i = 0; i < _roomInstances.Length; i++)
        //     {
        //         Destroy(_roomInstances[i]);
        //     }
        //
        //     _roomInstances = new GameObject[_roomInfos.Length];
        //     _roomManagers = new Room.Room[_roomInfos.Length];
        //
        //     _currentRoomIndex = 0;
        //
        //     InitRoomInstance();
        //
        //     ActiveRoom();
        // }

        private void Start()
        {
            roomBuilder.Initialize("default");
            _roomInfos = roomBuilder.SpawnRoomList();

            _roomInstances = new GameObject[_roomInfos.Length];
            _roomManagers = new Room.Room[_roomInfos.Length];

            _currentRoomIndex = 0;

            InitRoomInstance();

            ActiveRoom();
        }

        //初始化房间实例
        private void InitRoomInstance()
        {
            if (_roomInfos == null) return;

            for (int i = 0; i < _roomInfos.Length; i++)
            {
                GameObject newRoom =
                    Instantiate(roomDic[_roomInfos[i].myType], transform.position, Quaternion.identity);
                newRoom.SetActive(false);
                _roomManagers[i] = newRoom.GetComponent<Room.Room>();
                _roomInstances[i] = newRoom;
            }
        }

        private void OnPlayerSpawned(IEventArgs args)
        {
            if (args is PlayerEvent.PlayerSpawned playerSpawned)
            {
                cameraLerpToPointer.SetCameraTarget(playerSpawned.PlayerObject.transform);
                _player = playerSpawned.PlayerObject;
            }
        }

        private void ActiveRoom()
        {
            _roomInstances[_currentRoomIndex].SetActive(true);
        }

        private void OnEnterNextRoom(IEventArgs args)
        {
            if (args is RoomEvent.EnterNextRoom)
            {
                _roomInstances[_currentRoomIndex].SetActive(false);
                _currentRoomIndex++;
                _roomInstances[_currentRoomIndex].SetActive(true);

                _player.transform.position = _roomManagers[_currentRoomIndex].GetPlayerEnterPosition();
            }
        }
    }
}