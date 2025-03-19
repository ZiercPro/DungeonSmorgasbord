using ZiercCode.Utilities;
using ZiercCode.Utilities.Data;

namespace ZiercCode._DungeonGame.Config
{
    public class ConfigComponent : USingleton<ConfigComponent>
    {
        private JsonDataService _jsonDataService;
        private GameSettings _gameSettings;
        public GameSettings GameSettings => _gameSettings;

        public void Initialize()
        {
            _jsonDataService = new JsonDataService();
        }

        public void LoadGameSettings()
        {
            _gameSettings = _jsonDataService.LoadData<GameSettings>(GameSettings.SETTING_DATA_PATH, false);
        }

        public void SaveGameSettings(GameSettings settings)
        {
            _gameSettings = settings;
            _jsonDataService.SaveData(GameSettings.SETTING_DATA_PATH, _gameSettings, false);
        }
    }
}