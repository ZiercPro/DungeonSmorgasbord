using System.Collections.Generic;
using UnityEngine;
using ZiercCode._DungeonGame.Room;

namespace ZiercCode._DungeonGame
{
    [CreateAssetMenu(menuName = "ScriptableObject/RoomGenerateConfig", fileName = "RoomGenerateConfig")]
    public class RoomGenerateConfig : ScriptableObject
    {
        public Vector2Int roomLengthRange;//生成的房间长度范围
        public List<RandomRoom.RoomInfo> roomInfos; //要生成的房间信息
    }
}