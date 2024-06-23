using Gameplay;
using UnityEngine;

namespace LetsLeap.Game.Audio
{
    public sealed class AudioManager : MonoSingleton<AudioManager>
    {
        private const string MusicPrefsKey = "Music";
        private const string SoundPrefsKey = "Sound";
        
        [Header("Audio Sources")]
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _vfxSource;
        [SerializeField] private AudioSource _leapSource;

        [Header("Sound Data")]
        [SerializeField] private SoundData _uiClickSoundData;
        [SerializeField] private SoundData _scrollSkinsSoundData;
        [SerializeField] private SoundData _leapSoundData;
        [SerializeField] private SoundData _destroyCometSoundData;
        [SerializeField] private SoundData _crossLineSoundData;
        [SerializeField] private SoundData _nextStageSoundData;
        [SerializeField] private SoundData _applyAbilitySoundData;
        [SerializeField] private SoundData _gameOverSoundData;

        public bool IsMusicEnable
        {
            get => PlayerPrefs.GetInt(MusicPrefsKey, 1) == 1;
            set
            {
                PlayerPrefs.SetInt(MusicPrefsKey, value ? 1 : 0); 
                _musicSource.mute = !value;
            }
        }

        public bool IsSoundEnable
        {
            get => PlayerPrefs.GetInt(SoundPrefsKey, 1) == 1;
            set => PlayerPrefs.SetInt(SoundPrefsKey, value ? 1 : 0);
        }
        
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);

            _musicSource.mute = !IsMusicEnable;
        }

        public void SetMusicVolume(float volume)
        {
            _musicSource.volume = volume;
        }

        public void PlayUiClickSound()
        {
            PlayVfxSound(_uiClickSoundData);
        }

        public void PlayScrollSkinsSound()
        {
            PlayVfxSound(_scrollSkinsSoundData);
        }

        public void PlayLeapSound(float pitch)
        {
            if (!IsSoundEnable)
            {
                return;
            }
            
            _leapSource.pitch = pitch;
            _leapSource.PlayOneShot(_leapSoundData.Clip, _leapSoundData.Volume);
        }

        public void PlayDestroyCometSound()
        {
            PlayVfxSound(_destroyCometSoundData);
        }

        public void PlayCrossLineSound()
        {
            PlayVfxSound(_crossLineSoundData);
        }

        public void PlayNextStageSound()
        {
            PlayVfxSound(_nextStageSoundData);
        }

        public void PlayApplyAbilitySound()
        {
            PlayVfxSound(_applyAbilitySoundData);
        }

        public void PlayGameOverSound()
        {
            PlayVfxSound(_gameOverSoundData);
        }

        private void PlayVfxSound(SoundData soundData)
        {
            if (!IsSoundEnable)
            {
                return;
            }
            
            _vfxSource.PlayOneShot(soundData.Clip, soundData.Volume);
        }
    }
}