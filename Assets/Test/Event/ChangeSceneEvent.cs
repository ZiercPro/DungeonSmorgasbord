using System;

namespace ZiercCode.Test.Event
{
    public class ChangeSceneEvent
    {
        public class StartChangeSceneEvent : IEventArgs
        {
            public Type NextSceneProcedureType;
        }
    }
}