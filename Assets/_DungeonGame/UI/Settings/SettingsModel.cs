using RMC.Core.Observables;
using RMC.Mini.Model;

namespace ZiercCode._DungeonGame.UI.Settings
{
    public class SettingsModel : BaseModel
    {
        public bool SettingChanged; //设置是否修改

        public Observable<float> MasterVolume; //主音量
        public Observable<float> MusicVolume; //音乐音量
        public Observable<float> EnvironmentVolume; //环境音量
        public Observable<float> SfxVolume; //音效音量
        public Observable<bool> FpsOn; //开启fpx显示
        public Observable<int> Language; //语言

        public SettingsModel()
        {
            MasterVolume = new Observable<float>();
            EnvironmentVolume = new Observable<float>();
            MusicVolume = new Observable<float>();
            SfxVolume = new Observable<float>();
            FpsOn = new Observable<bool>();
            Language = new Observable<int>();
        }
    }
}