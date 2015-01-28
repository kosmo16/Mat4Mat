using Framework.Events;
using UnityEngine;

namespace Systems
{
    /// <summary>
    /// System do odtwarzania muzyki w tle.
    /// </summary>
    public class MusicSystem : Framework.Core.System
    {
        [Range(0.0f, 1.0f)]
        public float volume;
        public AudioClip mainMusic;

        private AudioListener listener;
        private AudioSource source;
        private AudioClip playingAudioClip;

        public override void Initialize()
        {
            listener = GameObject.FindObjectOfType<AudioListener>() as AudioListener;
            source = listener.audio;
            source.volume = volume;
        }

        public override void OnUpdate()
        {
            if (playingAudioClip == null)
            {
                playingAudioClip = mainMusic;
                source.loop = true;
                source.clip = mainMusic;
                source.Play();
            }
        }
    }
}
