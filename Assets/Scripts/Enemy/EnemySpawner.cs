using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //Spawner Variables
    [SerializeField]
    private GameObject _enemyPrefab;
    //minimum time for enemys to spawn, in seconds
    [SerializeField]
    private float _minimumSpawnTime;
    //max time for enemys to spawn
    [SerializeField]
    private float _maximumSpawnTime;
    //time before initial spawn
    private float _timeUntilSpawn;

    void Awake()
    {
        SetTimeUntilSpawn();
    }

    void Update()
    {
        //setting initial spawn time 
        _timeUntilSpawn -= Time.deltaTime;

        if (_timeUntilSpawn <= 0)
        {
            Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
            SetTimeUntilSpawn();
        }
    }

    private void SetTimeUntilSpawn()
    {
        _timeUntilSpawn = Random.Range(_minimumSpawnTime, _maximumSpawnTime);
    }
}
