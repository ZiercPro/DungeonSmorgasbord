using RMC.Mini;
using RMC.Mini.View;
using UnityEngine;

namespace ZiercCode._DungeonGame.SceneChangeEffect
{
    public class SceneChangeAnimationView : MonoBehaviour, IView
    {
        public void Dispose()
        {
            //TODO 在此释放托管资源
        }

        public void Initialize(IContext context)
        {
            
        }

        public void RequireIsInitialized()
        {
            
        }

        public bool IsInitialized { get; }

        public IContext Context { get; }
    }
}