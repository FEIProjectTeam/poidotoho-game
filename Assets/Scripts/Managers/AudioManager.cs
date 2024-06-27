using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        public static event Action OnAudioToggleMute;

        public static AudioManager Instance { get; private set; }

        [SerializeField]
        private AudioSource _musicSource;
        private List<Sound> _musicSounds;

        public void PlayMusic(string musicName)
        {
            var s = _musicSounds.Find(sound => sound.name == musicName);
            if (s == null)
                return;
            _musicSource.clip = s.clip;
            _musicSource.Play();
        }

        public void ToggleMute()
        {
            _musicSource.mute = !_musicSource.mute;
            OnAudioToggleMute?.Invoke();
        }

        public bool IsAudioMuted()
        {
            return _musicSource.mute;
        }

        private void Start()
        {
            _musicSounds = new List<Sound>
            {
                new() { name = "theme", clip = Resources.Load<AudioClip>("Audio/theme") }
            };
            PlayMusic("theme");
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
        }
    }
}
