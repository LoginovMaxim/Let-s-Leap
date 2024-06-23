using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public sealed class Hud : MonoSingleton<Hud>
    {
        [SerializeField] private TextMeshProUGUI _pointsAmountText;
        [SerializeField] private Image _splashWaveImage;

        public void SetPointsAmount(int pointsAmount)
        {
            _pointsAmountText.SetText(pointsAmount.ToString());
        }

        public void SplashWave()
        {
            //_splashWaveImage.color = new Color(1f, 1f, 1f, 0.66f);
            //_splashWaveImage.DOFade(0f, 0.33f).SetEase(Ease.InExpo);
        }
    }
}