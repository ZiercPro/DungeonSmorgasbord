namespace ZiercCode._DungeonGame.HallScene
{
    //可开关接口
    public interface IOpenClose
    {
        public bool IsClosed { get; }
        public void Open();
        public void Close();
    }
}