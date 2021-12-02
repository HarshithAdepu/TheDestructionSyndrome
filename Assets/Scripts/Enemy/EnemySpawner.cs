using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner enemySpawnerInstance;
    [SerializeField] List<Transform> corners;
    [SerializeField] float spawnInterval, spawnCheckRadius;
    [SerializeField] int enemyCap;
    [SerializeField] int enemiesInWave;
    [SerializeField] Text enemyStats;
    int enemiesSpawned, enemiesKilled;
    List<GameObject> enemiesOnScene;
    float timer;
    bool locationFound;
    void Start()
    {
        enemySpawnerInstance = this;
        enemiesOnScene = new List<GameObject>();
        enemiesSpawned = 0;
        enemiesKilled = 0;
        enemyStats.text = "T/K/L: " + enemiesInWave + "/" + enemiesKilled + "/" + (enemiesInWave - enemiesKilled);
    }
    void Update()
    {
        if (timer > spawnInterval && enemiesSpawned < enemiesInWave && enemiesOnScene.Count < enemyCap)
        {
            SpawnEnemy();
            enemiesSpawned++;
            timer = 0;
        }
        timer += Time.deltaTime;

    }
    void SpawnEnemy()
    {
        locationFound = false;
        Vector2 chosenLocation = Vector2.zero;
        //Collider2D[] nearby;

        while (!locationFound)
        {
            chosenLocation = new Vector2(Random.Range(corners[0].position.x, corners[1].position.x), Random.Range(corners[0].position.y, corners[1].position.y));

            if (Vector3.Distance(chosenLocation, GameObject.Find("Player").transform.position) > 5f)
            {
                locationFound = true;
            }
        }
        enemiesOnScene.Add(ObjectPooler.objectPoolerInstance.SpawnObject("Enemy", chosenLocation, Quaternion.identity));
        locationFound = false;
    }
    public void EnemyDied(GameObject enemy)
    {
        enemiesOnScene.Remove(enemy);
        enemiesKilled++;
        enemyStats.text = "T/K/L: " + enemiesInWave + "/" + enemiesKilled + "/" + (enemiesInWave - enemiesKilled);
    }
}
