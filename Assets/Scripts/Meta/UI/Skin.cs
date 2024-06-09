using System;
using LetsLeap.Game;
using UnityEngine;
using UnityEngine.UI;

namespace LetsLeap.Meta.UI
{
    public sealed class Skin : MonoBehaviour
    {
        public event Action<Skin> OnSkinSelected;
        
        [SerializeField] private Image _image;
        [SerializeField] private GameObject _lock;
        [SerializeField] private GameObject _selected;

        public SkinData SkinData { get; private set; }

        public void SetSkinData(SkinData skinData)
        {
            SkinData = skinData;
            
            _image.color = skinData.IsUnlocked ? Color.white : Color.black;
            _lock.SetActive(!skinData.IsUnlocked);
            _selected.SetActive(skinData.IsSelected);

            if (skinData.Icon == default)
            {
                _lock.SetActive(false);
                _selected.SetActive(false);
                _image.enabled = false;
                return;
            }
            
            _image.sprite = skinData.Icon;
        }

        public void SetSelectState(bool isSelected)
        {
            _selected.SetActive(isSelected);
        }

        public void OnSkinPressed()
        {
            if (!SkinData.IsUnlocked)
            {
                return;
            }
            
            OnSkinSelected?.Invoke(this);
        }
    }
}