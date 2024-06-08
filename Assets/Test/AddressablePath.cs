using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ZiercCode.Test
{
    public class AddressablePath : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log(Addressables.BuildPath);
            Debug.Log(Addressables.RuntimePath);
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}