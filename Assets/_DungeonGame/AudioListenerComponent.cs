using UnityEngine;

namespace ZiercCode
{
    public class AudioListenerComponent : MonoBehaviour //更新audioListener的位置
    {
        private Camera _mainCamera;

        private void GetMainCamera()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (_mainCamera == null) GetMainCamera();
            else transform.position = _mainCamera.transform.position;
        }
    }
}