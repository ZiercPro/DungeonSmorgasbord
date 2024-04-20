using UnityEngine.Serialization;

namespace ZiercCode.Old.Helper
{
    /// <summary>
    /// 自定义文本的数据表
    /// 如果有新的语言，需要新添加字段
    /// </summary>
    [System.Serializable]
    public struct CustomTextTable
    {
        /// <summary>
        /// 中文文本
        /// </summary>
        public string chinese;

        /// <summary>
        /// 英文文本
        /// </summary>
        public string english;
    }
}