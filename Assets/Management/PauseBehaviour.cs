using UnityEngine;

namespace ZiercCode.Management
{
    public class PauseBehaviour : MonoBehaviour
    {
        private void Update()
        {
            if (!GameState.Instance.IsPaused) PauseUpdate();
        }

        private void LateUpdate()
        {
            if (!GameState.Instance.IsPaused) PauseLateUpdate();
        }

        private void FixedUpdate()
        {
            if (!GameState.Instance.IsPaused) PauseFixedUpdate();
        }


        //会被暂停状态影响的update
        protected virtual void PauseUpdate()
        {
        }

        protected virtual void PauseLateUpdate()
        {
        }

        protected virtual void PauseFixedUpdate()
        {
        }
    }
}