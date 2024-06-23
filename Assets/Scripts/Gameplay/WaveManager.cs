using System.Collections.Generic;
using LetsLeap.Game;
using LetsLeap.Game.Audio;
using UnityEngine;

namespace Gameplay
{
    public sealed class WaveManager : MonoSingleton<WaveManager>
    {
        [SerializeField] private AbilitiesSpawner _abilitiesSpawner;
        [SerializeField] private List<WaveData> _wavesData;
        
        [Header("Configs")]
        [SerializeField] private GameplayConfig _gameplayConfig;

        [Header("Visual")]
        [SerializeField] private SpriteRenderer _background;
        [SerializeField] private Camera _camera;
        
        private float _waveChangeTime;
        private int _currentWaveNumber;

        public int CurrentWaveNumber => _currentWaveNumber;

        public int DebugWave;
        
        private void Start()
        {
#if UNITY_EDITOR
            _currentWaveNumber = DebugWave;
#endif
            
            UpdateNextWaveTime();
            
            var waveData = _wavesData[_currentWaveNumber];
            foreach (var spawner in waveData.Spawners)
            {
                spawner.UnPause();
            }

            _background.color = waveData.BackgoundColor;
            _camera.backgroundColor = waveData.CameraBackgroundColor;
        }

        private void Update()
        {
            if (GameManager.Instance.IsGamePaused)
            {
                return;
            }
            
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
            var previousWaveData = _wavesData[_currentWaveNumber];
            foreach (var spawner in previousWaveData.Spawners)
            {
                spawner.Pause();
            }

            _currentWaveNumber++;
            
            var waveData = _wavesData[_currentWaveNumber];
            foreach (var spawner in waveData.Spawners)
            {
                spawner.UnPause();
            }

            _background.color = waveData.BackgoundColor;
            _camera.backgroundColor = waveData.CameraBackgroundColor;
            
            if (_currentWaveNumber > Statistics.Instance.Stage)
            {
                Statistics.Instance.Stage = _currentWaveNumber;
            }
            
            _abilitiesSpawner.OnNextWaveStarted();
            
            Hud.Instance.SplashWave();
            AudioManager.Instance.PlayNextStageSound();

            var playerPosition = GameManager.Instance.Player.transform.position + 0.25f * Vector3.down;
            var scoreHitDisplay = PoolService.Instance.Spawn<DisplayTextCanvas>(playerPosition, Quaternion.identity, null);
            scoreHitDisplay.SetText("новый уровень");
        }

        private void UpdateNextWaveTime()
        {
            _waveChangeTime = Time.time + _gameplayConfig.WaveDuration * Mathf.Sqrt(_currentWaveNumber + 1);
        }
    }
}