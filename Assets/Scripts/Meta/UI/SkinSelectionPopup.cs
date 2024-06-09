using System.Collections.Generic;
using LetsLeap.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LetsLeap.Meta.UI
{
    public sealed class SkinSelectionPopup : MonoBehaviour
    {
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private Transform _skinsParent;
        [SerializeField] private SkinsConfig _skinsConfig;
        [SerializeField] private Skin _skinPrefab;
        [SerializeField] private TextMeshProUGUI _focusedSkinNameText;
        [SerializeField] private TextMeshProUGUI _focusedSkinDescriptionText;
        [SerializeField] private float _speedSnap;
        
        private int _skinsCount;
        private List<float> _snapPositions;
        private List<Skin> _skins;
        private Skin _currentSkin;

        private float _targetHorizontalNormalizedPosition;
        
        private void Start()
        {
            _skins = new List<Skin>();
            
            CreateBorder();
            foreach (var skinData in _skinsConfig.SkinData)
            {
                var skin = Instantiate(_skinPrefab, _skinsParent);
                skin.SetSkinData(skinData);
                skin.OnSkinSelected += ApplySkin;
                
                if (skinData.IsSelected)
                {
                    _currentSkin = skin;
                }
                
                _skins.Add(skin);
            }
            CreateBorder();

            _skinsCount = _skinsConfig.SkinData.Count + 2;
            _snapPositions = new List<float>();

            var position = 0f;
            var step = 1f / (_skinsCount - 1);

            for (var i = 0; i < _skinsCount; i++)
            {
                _snapPositions.Add(position);
                position += step;
            }

            _scrollRect.horizontalNormalizedPosition = step;
            _targetHorizontalNormalizedPosition = step;
        }

        private void ApplySkin(Skin skin)
        {
            _currentSkin.SetSelectState(false);
            _currentSkin = skin;
            _currentSkin.SetSelectState(true);

            foreach (var skinData in _skinsConfig.SkinData)
            {
                skinData.IsSelected = skinData.Name == _currentSkin.SkinData.Name;
            }
            
            MetaScreen.Instance.SetSkinIcon(_currentSkin.SkinData.Icon);
        }

        private void CreateBorder()
        {
            var border = Instantiate(_skinPrefab, _skinsParent);
            border.SetSkinData(new SkinData());
        }

        private void Update()
        {
            SnapPosition();
            UpdatePosition();
        }

        private void SnapPosition()
        {
            if (Input.GetMouseButtonUp(0))
            {
                return;
            }

            var position = _scrollRect.horizontalNormalizedPosition;
            var minDiff = 1f;
            var focusedIndex = 0;
            
            _targetHorizontalNormalizedPosition = 1;

            for (var i = 0; i < _snapPositions.Count; i++)
            {
                var snapPosition = _snapPositions[i];
                var diff = Mathf.Abs(position - snapPosition);

                if (diff > minDiff)
                {
                    continue;
                }
                
                minDiff = diff;
                focusedIndex = i;
                _targetHorizontalNormalizedPosition = snapPosition;
            }
            
            var step = 1f / (_skinsCount - 1);
            _targetHorizontalNormalizedPosition = Mathf.Clamp(_targetHorizontalNormalizedPosition, step, 1f - step);

            if (focusedIndex == 0 || focusedIndex == _snapPositions.Count - 1)
            {
                return;
            }
            
            _focusedSkinNameText.text = _skinsConfig.SkinData[focusedIndex - 1].Name;
            _focusedSkinDescriptionText.text = _skinsConfig.SkinData[focusedIndex - 1].Description;
        }

        private void UpdatePosition()
        {
            if (Input.GetMouseButton(0))
            {
                return;
            }
            
            _scrollRect.horizontalNormalizedPosition = Mathf.Lerp(
                _scrollRect.horizontalNormalizedPosition,
                _targetHorizontalNormalizedPosition, 
                _speedSnap * Time.deltaTime);
        }

        private void OnDestroy()
        {
            foreach (var skin in _skins)
            {
                skin.OnSkinSelected -= ApplySkin;
            }
        }
    }
}