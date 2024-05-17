using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay
{
    public sealed class WaveManager : MonoBehaviour
    {
        [Header("Configs")]
        [SerializeField] private GameplayConfig _gameplayConfig;
        [SerializeField] private WavesConfig _wavesConfig;

        [Header("Visual")]
        [SerializeField] private SpriteRenderer _background;
        [SerializeField] private Camera _camera;

        private List<Spawner> _spawners;
        private Dictionary<SpawnerId, Spawner> _spawnersByIds;
        
        private float _waveChangeTime;
        private int _currentWaveNumber;
        
        private void Start()
        {
            _spawners = FindObjectsByType<Spawner>(FindObjectsSortMode.None).ToList();
            _spawnersByIds = new Dictionary<SpawnerId, Spawner>();
            
            foreach (var spawner in _spawners)
            {
                _spawnersByIds.Add(spawner.SpawnerId, spawner);
            }
            
            UpdateNextWaveTime();
        }

        private void Update()
        {
            if (_currentWaveNumber == _wavesConfig.WavesData.Count - 1)
            {
                return;
            }
            
            if (_waveChangeTime > Time.time)
            {
                return;
            }

            RunCurrenWaveSpawners();
            UpdateNextWaveTime();
        }

        private void RunCurrenWaveSpawners()
        {
            if (_currentWaveNumber > 0)
            {
                var previousWaveData = _wavesConfig.WavesData[_currentWaveNumber];
                foreach (var spawnerId in previousWaveData.SpawnerIds)
                {
                    if (_spawnersByIds.TryGetValue(spawnerId, out var spawner))
                    {
                        spawner.Pause();
                    }
                }
            }

            _currentWaveNumber++;
            
            var waveData = _wavesConfig.WavesData[_currentWaveNumber];
            foreach (var spawnerId in waveData.SpawnerIds)
            {
                if (_spawnersByIds.TryGetValue(spawnerId, out var spawner))
                {
                    spawner.UnPause();
                }
            }

            _background.color = waveData.BackgoundColor;
            _camera.backgroundColor = waveData.CameraBackgroundColor;
        }

        private void UpdateNextWaveTime()
        {
            _waveChangeTime = Time.time + _gameplayConfig.WaveDuration * _currentWaveNumber;
        }
    }
}