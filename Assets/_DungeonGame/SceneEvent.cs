using ZiercCode.Event;

namespace ZiercCode._DungeonGame
{
    public class SceneEvent
    {
        public class ChangeSceneEvent : IEventArgs
        {
            //public Type NextSceneProcedureType;
            public string NextSceneName;
        }
    }
}