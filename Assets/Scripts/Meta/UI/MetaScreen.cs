using System;
using Gameplay;
using LetsLeap.Game;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LetsLeap.Meta.UI
{
    public sealed class MetaScreen : MonoSingleton<MetaScreen>
    {
        [SerializeField] private SkinsConfig _skinsConfig;
        
        [Header("Statistics")]
        [SerializeField] private TextMeshProUGUI _recordValueText;
        [SerializeField] private TextMeshProUGUI _leapValueText;
        [SerializeField] private TextMeshProUGUI _multiplyValueText;
        [SerializeField] private TextMeshProUGUI _stageValueText;
        [SerializeField] private TextMeshProUGUI _skinProgressValueText;
        
        [Header("Skin")]
        [SerializeField] private Image _skinIcon;

        #region Stats
        
        private int _record;
        private int _leap;
        private int _multiply;
        private int _stage;
        private int _skinProgress;

        public int Record
        {
            get => _record;
            set
            {
                _record = value;
                _recordValueText.text = $"{_record}";
            }
        }

        public int Leap
        {
            get => _leap;
            set
            {
                _leap = value;
                _leapValueText.text = $"{_leap}М";
            }
        }

        public int Multiply
        {
            get => _multiply;
            set
            {
                _multiply = value;
                _multiplyValueText.text = $"Х{_multiply}";
            }
        }
        
        public int Stage
        {
            get => _stage;
            set
            {
                _stage = value;
                _stageValueText.text = $"{_stage}";
            }
        }
        
        public int SkinProgress
        {
            get => _skinProgress;
            set
            {
                _skinProgress = value;
                _skinProgressValueText.text = $"{_skinProgress}%";
            }
        }

        #endregion

        private void Start()
        {
            foreach (var skinData in _skinsConfig.SkinData)
            {
                if (!skinData.IsSelected)
                {
                    continue;
                }
                
                SetSkinIcon(skinData.Icon);
            }
            
            Record = Statistics.Instance.Record;
            Leap = Statistics.Instance.Leap;
            Multiply = Statistics.Instance.Multiply;
            Stage = Statistics.Instance.Stage;
            SkinProgress = Statistics.Instance.SkinProgress;
        }

        public void OnPlayButtonPressed()
        {
            SceneManager.LoadScene("Game");
        }

        public void SetSkinIcon(Sprite sprite)
        {
            _skinIcon.sprite = sprite;
        }
    }
}