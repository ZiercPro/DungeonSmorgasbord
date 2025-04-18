using UnityEngine;
using ZiercCode._DungeonGame._Scripts;
using ZiercCode._DungeonGame.Config;
using ZiercCode.Management;
using ZiercCode.ObjectPool;

namespace ZiercCode._DungeonGame.GameEntry
{
    public class GameExit : MonoBehaviour
    {
        //游戏退出事件
        private void OnApplicationQuit()
        {
            GameState.Instance.PauseGame();
            //保存游戏
            ConfigComponent.Instance.SaveGame();

            PoolManager.Instance.DisposeAll();
            SceneComponent.Instance.UnloadAllScenes("GameEntry");
        }
    }
}