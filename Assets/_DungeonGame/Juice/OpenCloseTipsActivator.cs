using UnityEngine;
using ZiercCode._DungeonGame.HallScene;

namespace ZiercCode._DungeonGame.Juice
{
    public class OpenCloseTipsActivator : MonoBehaviour
    {
        [SerializeField] private GameObject tipsPrefab;

        private IOpenClose _iOpenClose;

        private void Awake()
        {
            _iOpenClose = GetComponent<IOpenClose>();
        }

        private void FixedUpdate()
        {
            if (_iOpenClose.IsClosed && tipsPrefab.activeInHierarchy)
            {
                tipsPrefab.SetActive(false);
            }
            else if (!_iOpenClose.IsClosed && !tipsPrefab.activeInHierarchy)
            {
                tipsPrefab.SetActive(true);
            }
        }
    }
}