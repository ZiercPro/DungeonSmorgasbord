using UnityEngine;
using ZiercCode.Audio;

namespace ZiercCode._DungeonGame.MainMenu
{
    public class MainMenuSceneManager : MonoBehaviour
    {
        private void OnEnable()
        {
            AudioPlayer.Instance.PlayMusic("Audio_Music_MainMenu_0");
        }

        private void OnDisable()
        {
            AudioPlayer.Instance.StopMusic();
        }
    }
}