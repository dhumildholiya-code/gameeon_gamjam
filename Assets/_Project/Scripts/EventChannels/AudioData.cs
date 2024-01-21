using UnityEngine;

namespace GamJam.EventChannel
{
    [CreateAssetMenu(fileName = "Audio Clip", menuName = "Audio/Audio Clip")]
    public class AudioData : ScriptableObject
    {
        [SerializeField]
        private AudioEventChannel _channel;

        public AudioClip clip;
        public float volume;
        public Vector2 pitchRange;

        public void Play()
        {
            _channel.Invoke(this);
        }
    }
}
