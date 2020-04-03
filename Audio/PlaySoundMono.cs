using UnityEngine;
using Zenject;

namespace TanTanTools.Audio
{
    // Helper class if you need to invoke PlaySound from animation or UnityEvents
    public class PlaySoundMono : MonoBehaviour
    {
        [Inject] private IAudioManager m_audioManager;

        [Inject]
        private void construct(IAudioManager a_audioManager)
        {
            m_audioManager = a_audioManager;
        }

        public void PlaySound(ScriptableAudioAsset a_audioAsset)
        {
            m_audioManager.PlayAudio(a_audioAsset);
        }
    }
}
