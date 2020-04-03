using UnityEngine;

namespace TanTanTools.Audio
{
    [CreateAssetMenu(menuName = "AudioAsset", fileName = "AudioSource_{name}")]
    public class ScriptableAudioAsset : ScriptableObject
    {
        public AudioClip Clip;
        [MinTo(0f, 2f)]
        public Vector2 PitchRange = new Vector2(1f, 1f);
        [MinTo(0f, 2f)]
        public Vector2 Volume = new Vector2(1f, 1f);
    }
}
