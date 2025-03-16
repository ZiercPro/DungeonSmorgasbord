using UnityEngine;
using ZiercCode.Audio;
using ZiercCode.Event;

namespace ZiercCode._DungeonGame
{
    public class MainMenuSceneManager : MonoBehaviour
    {
        private EventsGroup _eventsGroup = new EventsGroup();

        private void OnEnable()
        {
            _eventsGroup.AddListener<SceneEvent.ChangeSceneEvent>(OnSceneChange);
            AudioPlayer.Instance.PlayMusic("Audio_Music_MainMenu_0");
        }

        private void OnDisable()
        {
            AudioPlayer.Instance.StopMusic();
            _eventsGroup.RemoveAllListener();
        }

        private void OnSceneChange(IEventArgs args)
        {
            if (args is SceneEvent.ChangeSceneEvent changeSceneEvent)
            {
                SceneComponent.Instance.LoadScene(changeSceneEvent.NextSceneName, true);
            }
        }
    }
}