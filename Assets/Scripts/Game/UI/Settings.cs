using System;
using Gameplay;
using LetsLeap.Game.Audio;
using UnityEngine;

namespace LetsLeap.Game.UI
{
    public sealed class Settings : MonoSingleton<Settings>
    {
        [SerializeField] private GameObject _musicMuteSign;
        [SerializeField] private GameObject _soundMuteSign;
        
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
            gameObject.SetActive(false);
        }

        private void Start()
        {
            _musicMuteSign.gameObject.SetActive(!AudioManager.Instance.IsMusicEnable);
            _soundMuteSign.gameObject.SetActive(!AudioManager.Instance.IsSoundEnable);
        }

        public void OnMusicButtonPressed()
        {
            AudioManager.Instance.IsMusicEnable = !AudioManager.Instance.IsMusicEnable;
            _musicMuteSign.gameObject.SetActive(!AudioManager.Instance.IsMusicEnable);
            
            AudioManager.Instance.PlayUiClickSound();
        }

        public void OnSoundButtonPressed()
        {
            AudioManager.Instance.IsSoundEnable = !AudioManager.Instance.IsSoundEnable;
            _soundMuteSign.gameObject.SetActive(!AudioManager.Instance.IsSoundEnable);
            
            AudioManager.Instance.PlayUiClickSound();
        }

        public void OnCloseButtonPressed()
        {
            gameObject.SetActive(false);
            AudioManager.Instance.PlayUiClickSound();
        }
    }
}