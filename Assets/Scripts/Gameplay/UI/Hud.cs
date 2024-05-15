using TMPro;
using UnityEngine;

namespace Gameplay
{
    public sealed class Hud : MonoSingleton<Hud>
    {
        [SerializeField] private TextMeshProUGUI _pointsAmountText;

        public void SetPointsAmount(int pointsAmount)
        {
            _pointsAmountText.SetText(pointsAmount.ToString());
        }
    }
}