﻿using ImmersiveVrToolsCommon.Runtime.Logging;
using UnityEngine;

namespace FastScriptReload.Scripts.Runtime
{
#if UNITY_EDITOR
    [UnityEditor.InitializeOnLoad]
#endif
    public static class LoggerScopedInitializer
    {
        static LoggerScopedInitializer()
        {
            Init();
        }
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        static void Init()
        {
            LoggerScoped.LogPrefix = "FSR: ";
        }
    }
}