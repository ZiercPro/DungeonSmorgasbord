using UnityEngine;
using UnityEngine.Events;

namespace ZiercCode._DungeonGame.SceneChangeEffect
{
    public class FunctionTest : MonoBehaviour
    {
        [SerializeField] private KeyCode pressKey;

        public UnityEvent onKeyPressed;

        private void Update()
        {
            if (Input.GetKeyDown(pressKey))
            {
                onKeyPressed.Invoke();
            }
        }
    }
}