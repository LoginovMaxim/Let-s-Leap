using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public sealed class WaveManager : MonoBehaviour
    {
        [SerializeField] private List<WaveData> _wavesData;
        
        [Header("Configs")]
        [SerializeField] private GameplayConfig _gameplayConfig;

        [Header("Visual")]
        [SerializeField] private SpriteRenderer _background;
        [SerializeField] private Camera _camera;
        
        private float _waveChangeTime;
        private int _currentWaveNumber;
        
        private void Start()
        {
            UpdateNextWaveTime();
            
            var waveData = _wavesData[_currentWaveNumber];
            foreach (var spawner in waveData.Spawners)
            {
                spawner.UnPause();
            }
        }

        private void Update()
        {
            if (_currentWaveNumber == _wavesData.Count - 1)
            {
                return;
            }
            
            if (_waveChangeTime > Time.time)
            {
                return;
            }

            NextWave();
            UpdateNextWaveTime();
        }

        private void NextWave()
        {
            if (_currentWaveNumber > 0)
            {
                var previousWaveData = _wavesData[_currentWaveNumber];
                foreach (var spawner in previousWaveData.Spawners)
                {
                    spawner.Pause();
                }
            }

            _currentWaveNumber++;
            
            var waveData = _wavesData[_currentWaveNumber];
            foreach (var spawner in waveData.Spawners)
            {
                spawner.UnPause();
            }

            _background.color = waveData.BackgoundColor;
            _camera.backgroundColor = waveData.CameraBackgroundColor;

            Hud.Instance.SplashWave();
        }

        private void UpdateNextWaveTime()
        {
            _waveChangeTime = Time.time + _gameplayConfig.WaveDuration * Mathf.Sqrt(_currentWaveNumber + 1);
        }
    }
}