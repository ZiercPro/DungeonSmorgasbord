using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace ZiercCode
{
    public class AddressableLoadTest : MonoBehaviour
    {
        private void Start()
        {
            AsyncOperationHandle<Texture2D> load = Addressables.LoadAssetAsync<Texture2D>("Texture2D_Circle");
            load.Completed += loadHandler =>
            {
                GameObject obj = new GameObject();
                obj.AddComponent<SpriteRenderer>().sprite = Sprite.Create(loadHandler.Result,
                    new Rect(0f, 0f, loadHandler.Result.width, loadHandler.Result.height), new Vector2(0.5f, 0.5f));
            };
        }
    }
}