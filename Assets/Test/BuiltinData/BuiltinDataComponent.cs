using UnityEngine;
using ZiercCode.Test.Base;
using ZiercCode.Test.Definition;

namespace ZiercCode.Test.BuiltinData
{
    public class BuiltinDataComponent : ZiercComponent
    {
        [SerializeField] private TextAsset builtInfoTextAsset;

        private BuiltInfo _builtInfo;

        public BuiltInfo BuiltInfo => _builtInfo;

        public void InitializeBuiltInfo()
        {
            if (builtInfoTextAsset == null || string.IsNullOrEmpty(builtInfoTextAsset.text))
            {
                Debug.LogError("无法找到构建信息文件!");
                return;
            }

            //todo 解析构建信息
            //_builtInfo = GameEntry.JsonService.ToObject<BuiltInfo>(builtInfoTextAsset.text);

            if (_builtInfo == null)
            {
                Debug.LogError("构建信息文件解析失败!");
            }
        }
    }
}