using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] private WaveData[] waves;
    private int _currentWaveIndex = 0;
    private WaveData currentWave => waves[_currentWaveIndex];


    private float _spawnTimer;
    private int _spawnCounter;

    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private ObjectPooler enemy1Pool;
    [SerializeField] private ObjectPooler enemy2Pool;
    [SerializeField] private ObjectPooler enemy3Pool;

    private Dictionary<EnemyType, ObjectPooler> _poolDictionary;

    private void Awake()
    {
        _poolDictionary = new Dictionary<EnemyType, ObjectPooler>()
        {
            {EnemyType.Enemy1, enemy1Pool },
            {EnemyType.Enemy2, enemy2Pool },
            {EnemyType.Enemy3, enemy3Pool },
        };
    }
    void Update()
    {
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer <= 0 && _spawnCounter < currentWave.enemiesPerWave)
        {
            _spawnTimer = currentWave.spawnInterval;
            SpawnEnemy();
            _spawnCounter++;
        }
        else if (_spawnCounter >= currentWave.enemiesPerWave)
        {
            _currentWaveIndex++;
            _spawnCounter = 0;
        }
    }

    private void SpawnEnemy()
    {
        if (_poolDictionary.TryGetValue(currentWave.enemyType, out var pool))
        {
            GameObject spawnedObject = pool.GetPooledObject();
            spawnedObject.transform.position = transform.position;
            spawnedObject.SetActive(true);
        }
       

    }

}
