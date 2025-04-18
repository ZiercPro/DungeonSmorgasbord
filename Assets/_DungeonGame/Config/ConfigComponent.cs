using ZiercCode._DungeonGame._Scripts;
using ZiercCode.Utilities;
using ZiercCode.Utilities.Data;

namespace ZiercCode._DungeonGame.Config
{
    public class ConfigComponent : USingleton<ConfigComponent>
    {
        private JsonDataService _jsonDataService;

        public GameConfig GameConfig;
        public GameSettings GameSettings;

        public void Initialize()
        {
            _jsonDataService = new JsonDataService();
        }

        public void LoadGameSettings()
        {
            //不成功返回默认值
            _jsonDataService.LoadData(GameSettings.SETTING_DATA_PATH, out GameSettings, false);
        }

        public void SaveGameSettings(GameSettings settings)
        {
            GameSettings = settings;
            _jsonDataService.SaveData(GameSettings.SETTING_DATA_PATH, GameSettings, false);
        }

        public void LoadGameSave()
        {
            bool success = _jsonDataService.LoadData(GameConfig.SAVE_FILE_PATH, out GameConfig, false);
            if (!success)
            {
                //不成功则从默认路径加载
                _jsonDataService.LoadData(GameConfig.DEFAULT_SAVE_CONFIG_PATH, out GameConfig, false);
            }
        }

        public void SaveGame()
        {
            _jsonDataService.SaveData(GameConfig.SAVE_FILE_PATH, GameConfig, false);
        }
    }
}