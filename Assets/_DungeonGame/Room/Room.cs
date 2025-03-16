using UnityEngine;
using ZiercCode.Event;

namespace ZiercCode._DungeonGame.Room
{
    public class Room : MonoBehaviour
    {
        [SerializeField] private Transform playerEnterPosition;

        public void EnterNextRoom()
        {
            EventBus.Invoke(new RoomEvent.EnterNextRoom());
        }

        public Vector2 GetPlayerEnterPosition()
        {
            return playerEnterPosition.position;
        }
    }
}