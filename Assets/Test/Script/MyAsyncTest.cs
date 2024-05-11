using NaughtyAttributes.Scripts.Core.DrawerAttributes_SpecialCase;
using System.Threading.Tasks;
using UnityEngine;
using ZiercCode.Old.Audio;

namespace ZiercCode.Test.Script
{
    public class MyAsyncTest : MonoBehaviour
    {
        private void Start()
        {
            AudioPlayer.Instance.PlayAudioAsync(AudioName.MenuBgm);
            Debug.Log("test");
        }
#if UNITY_EDITOR
        [Button("AsyncTest")]
        public void AsyncTest()
        {
            var result = Task.Run(TestAsyncMethod);
            Debug.Log("TestAsync outside");
        }

        [Button("NormalTest")]
        public void NormalTest()
        {
            int result = TestNormalMethod();
            Debug.Log("TestNormal" + result);
        }
#endif
        private int TestNormalMethod()
        {
            int result = 100;
            for (int i = 0; i < 100; i++)
            {
                Debug.Log(i);
            }

            return result;
        }

        private Task<int> TestAsyncMethod()
        {
            Task<int> reTask = Task.Run(() =>
            {
                int result = 100;
                for (int i = 0; i < 100; i++)
                {
                    Debug.Log(i);
                }

                return result;
            });
            Debug.Log("TestAsync inside");
            return reTask;
        }
    }
}