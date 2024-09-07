using System;

namespace ZiercCode.Test.Event
{
    public class SceneEvent
    {
        public class ChangeSceneEvent : IEventArgs
        {
            public Type NextSceneProcedureType;
            public string NextSceneName;
        }
    }
}