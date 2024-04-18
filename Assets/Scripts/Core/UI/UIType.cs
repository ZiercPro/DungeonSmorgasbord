namespace ZiercCode.Core.UI
{
    /// <summary>
    ///储存单个UI的信息
    /// </summary>
    public class UIType
    {
        /// <summary>
        /// UI名字
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// UI预制件的相对于Resources文件夹的路径
        /// </summary>
        public string Path { get; private set; }

        public UIType(string path)
        {
            Path = path;
            Name = path.Substring(path.LastIndexOf('/') + 1);
        }
    }
}