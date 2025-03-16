using UnityEngine;
using ZiercCode.Event;

namespace ZiercCode._DungeonGame
{
    public class PlayerEvent
    {
        public class PlayerSpawned : IEventArgs
        {
            public GameObject PlayerObject;
        }
    }
}