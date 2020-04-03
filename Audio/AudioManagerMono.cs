using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace TanTanTools.Audio
{
    public class AudioManagerMono : MonoBehaviour, IAudioManager
    {
        private AudioMixerGroup m_effectMixer;
        private AudioMixerGroup m_musicMixer;
        private Dictionary<ScriptableAudioAsset, AudioSource> m_assetToSource;
        private bool m_isInitialized = false;

        public void SetAudioMixerGroups(AudioMixerGroup a_effectMixer, AudioMixerGroup a_musicMixer)
        {
            m_effectMixer = a_effectMixer;
            m_musicMixer = a_musicMixer;
        }

        private void OnEnable()
        {
            TryInitialize();
            DontDestroyOnLoad(gameObject);
        }

        private void TryInitialize()
        {
            if (m_isInitialized)
                return;
            m_assetToSource = new Dictionary<ScriptableAudioAsset, AudioSource>();
            m_isInitialized = true;
        }

        private AudioSource setupSource(ScriptableAudioAsset a_asset)
        {
            AudioSource newSource = this.gameObject.AddComponent<AudioSource>();
            newSource.playOnAwake = false;
            newSource.clip = a_asset.Clip;
            m_assetToSource.Add(a_asset, newSource);
            return newSource;
        }

        public void PlayAudio(ScriptableAudioAsset a_audioAsset)
        {
            TryInitialize();
            AudioSource audioSource = null;
            if (!m_assetToSource.ContainsKey(a_audioAsset))
                audioSource = setupSource(a_audioAsset);
            else
                audioSource = m_assetToSource[a_audioAsset];
            Vector2 pitchRange = a_audioAsset.PitchRange;
            float randomPitch = Random.Range(pitchRange.x, pitchRange.y);
            audioSource.pitch = randomPitch;

            Vector2 volumeRange = a_audioAsset.Volume;
            float randomVolume = Random.Range(volumeRange.x, volumeRange.y);
            audioSource.volume = randomVolume;
            audioSource.loop = false;
            audioSource.outputAudioMixerGroup = m_effectMixer;
            // For now interupt
            audioSource.Stop();
            audioSource.Play();
        }

        public void PlayMusic(ScriptableAudioAsset a_audioAsset)
        {
            TryInitialize();
            AudioSource audioSource = null;
            if (!m_assetToSource.ContainsKey(a_audioAsset))
                audioSource = setupSource(a_audioAsset);
            else
                audioSource = m_assetToSource[a_audioAsset];

            StopAllMusic();
            // Sets looping to true
            audioSource.loop = true;
            audioSource.outputAudioMixerGroup = m_musicMixer;

            Vector2 pitchRange = a_audioAsset.PitchRange;
            float randomPitch = Random.Range(pitchRange.x, pitchRange.y);
            audioSource.pitch = randomPitch;

            Vector2 volumeRange = a_audioAsset.Volume;
            float randomVolume = Random.Range(volumeRange.x, volumeRange.y);
            audioSource.volume = randomVolume;

            // For now interupt
            audioSource.Stop();
            audioSource.Play();
        }

        public void StopAllMusic()
        {
            foreach (KeyValuePair<ScriptableAudioAsset, AudioSource> audioPair in m_assetToSource)
            {
                if (audioPair.Value.loop)
                    audioPair.Value.Stop();
            }
        }
    }
}
