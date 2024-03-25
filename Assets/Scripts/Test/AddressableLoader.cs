using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableLoader : MonoBehaviour
{
    [SerializeField] private AssetLabelReference audioLabel;
    [Space] [SerializeField] private AudioClip menuBgmClip;
    [Space] [SerializeField] private AssetReference menuBgmClipReference;

    [Space, TextArea(2, 6), SerializeField]
    private string menuBgmPath;

    private List<AudioClip> _testList;
    private AsyncOperationHandle<AudioClip> _singleHandle;
    private AsyncOperationHandle<IList<AudioClip>> _handle;

    // Start is called before the first frame update
    private void Awake()
    {
        _testList = new List<AudioClip>();
    }

    void Start()
    {
        // _handle = Addressables.LoadAssetsAsync<AudioClip>(audioLabel, audioClip =>
        // {
        //     Debug.Log(audioClip.name);
        //     _testList.Add(audioClip);
        // });
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void Check()
    {
        if (_handle.IsDone)
        {
            foreach (var clip in _testList)
            {
                if (clip.Equals(menuBgmClip))
                {
                    Debug.Log("get " + clip.name);
                }
            }
        }
    }
}