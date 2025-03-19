using UnityEngine;

namespace ZiercCode._DungeonGame.GameEntry
{
    public class GameExit : MonoBehaviour
    {
        //游戏退出事件
        private void OnApplicationQuit()
        {
            SceneComponent.Instance.UnloadAllScenes("GameEntry");
        }
    }
}