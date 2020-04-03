namespace TanTanTools.Audio
{
    public interface IAudioManager
    {
        void PlayAudio(ScriptableAudioAsset a_audioAsset);
        void PlayMusic(ScriptableAudioAsset a_audioAsset);
        void StopAllMusic();
    }
}
