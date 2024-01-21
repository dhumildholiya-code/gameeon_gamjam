using GamJam.EventChannel;
using UnityEngine;

namespace GamJam
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField]
        private AudioEventChannel[] _channels;
        [SerializeField]
        private AudioSource[] _sources;
        [SerializeField]
        private bool _test;

        private void Start()
        {
            foreach (var channel in _channels)
            {
                channel.Register(PlaySound);
            }
        }

        private void OnDestroy()
        {
            foreach (var channel in _channels)
            {
                channel.Unregister(PlaySound);
            }
        }

        private void PlaySound(AudioData data)
        {
            foreach (var source in _sources)
            {
                if (source.isPlaying) { continue; }
                source.volume = data.volume;
                source.pitch = Random.Range(data.pitchRange.x, data.pitchRange.y);
                source.clip = data.clip;
                source.Play();
                break;
            }
        }
    }
}
