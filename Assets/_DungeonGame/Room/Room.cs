using UnityEngine;

namespace ZiercCode._DungeonGame.Room
{
    public class Room : MonoBehaviour
    {
        [SerializeField]
        private Transform playerEnterPosition;

        public void EnterNextRoom()
        {
            //todo
            // EventBus.Invoke(new RoomEvent.EnterNextRoom());
        }

        public Vector2 GetPlayerEnterPosition()
        {
            return playerEnterPosition.position;
        }
    }
}