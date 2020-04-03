using UnityEngine;
using Zenject;

// A helper class to avoid having to inject the AudioManager everytime you want to call it
// Instead to access the current audiomanager use this class: AudioManagerFetcher.GetAudioManager().PlaySound()
namespace TanTanTools.Audio
{
    public class AudioManagerFetcher : MonoBehaviour
    {
        [Inject] private static IAudioManager m_audioManager;
        private static AudioManagerFetcher m_instance;

        [Inject]
        private void construct(IAudioManager a_audioManager)
        {
            m_audioManager = a_audioManager;
        }

        private void OnEnable()
        {
            if (m_instance != null)
            {
                Destroy(gameObject);
                return;
            }
            m_instance = this;
            DontDestroyOnLoad(gameObject);
        }

        protected static IAudioManager getAudioManager()
        {
            return m_audioManager;
        }

        public static IAudioManager GetAudioManager()
        {
            if (m_instance == null)
                return null;
            return getAudioManager();
        }
    }
}
