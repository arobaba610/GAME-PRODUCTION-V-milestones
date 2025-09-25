//using System.Collections;
//using UnityEngine;

//public class EnemySpawner : MonoBehaviour
//{
//    [Header("Enemy Prefabs")]
//    public GameObject[] enemyPrefabs;

//    [Header("Spawn Settings")]
//    public Transform[] spawnPoints;
//    public float timeBetweenWaves = 10f;
//    public float timeBetweenSpawns = 1f;
//    public int enemiesPerWave = 5;
//    public int maxEnemiesAlive = 20;

//    private int currentWave = 0;
//    private int enemiesAlive = 0;

//    void Start()
//    {
//        StartCoroutine(SpawnWaveRoutine());
//    }

//    IEnumerator SpawnWaveRoutine()
//    {
//        while (true)
//        {
//            yield return new WaitForSeconds(timeBetweenWaves);
//            currentWave++;
//            Debug.Log($"Wave {currentWave} started!");

//            for (int i = 0; i < enemiesPerWave; i++)
//            {
//                if (enemiesAlive >= maxEnemiesAlive)
//                    break;

//                SpawnEnemy();
//                yield return new WaitForSeconds(timeBetweenSpawns);
//            }

//            enemiesPerWave += 2; // Optional: increase difficulty
//        }
//    }

//    void SpawnEnemy()
//    {
//        int spawnIndex = Random.Range(0, spawnPoints.Length);
//        int enemyIndex = Random.Range(0, enemyPrefabs.Length);

//        GameObject enemy = Instantiate(enemyPrefabs[enemyIndex], spawnPoints[spawnIndex].position, Quaternion.identity);
//        enemiesAlive++;

//        // Optional: subscribe to enemy death to reduce alive count
//        EnemyPathFollowerJumping enemyScript = enemy.GetComponent<EnemyPathFollowerJumping>();
//        if (enemyScript != null)
//        {
//            enemyScript.OnDeath += () => enemiesAlive--;
//        }
//    }
//}
