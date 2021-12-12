using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner enemySpawnerInstance;
    [SerializeField] List<Transform> corners;
    [SerializeField] float distanceFromPlayer;
    [SerializeField] int enemyCap;
    [SerializeField] Text waveNumber, enemiesInWaveText, enemiesLeft, kills, nextWaveTimerText;
    [SerializeField] Wave firstWave;
    Wave currentWave;
    int enemiesInWave, enemiesSpawned, enemiesKilled, totalEnemiesKilled;
    List<GameObject> enemiesOnScene;
    float timer = 0, spawnInterval, nextWaveTimer;
    bool locationFound, waveInProgress;
    void Start()
    {
        enemySpawnerInstance = this;
        currentWave = firstWave;
        enemiesOnScene = new List<GameObject>();
        enemiesSpawned = 0;
        enemiesKilled = 0;
        enemiesInWave = firstWave.numberOfEnemies;
        spawnInterval = firstWave.spawnInterval;
        waveInProgress = true;
        nextWaveTimerText.enabled = false;

        UpdateStats();
    }
    void Update()
    {
        if (waveInProgress)
        {
            if (timer > spawnInterval && enemiesSpawned < enemiesInWave && enemiesOnScene.Count < enemyCap)
            {
                SpawnEnemy();
                enemiesSpawned++;
                timer = 0;
            }
            timer += Time.deltaTime;
        }
        else
        {
            nextWaveTimerText.text = "Next Wave in " + Mathf.Ceil(nextWaveTimer);
            nextWaveTimer -= Time.deltaTime;
        }
    }
    void SpawnEnemy()
    {
        locationFound = false;
        Vector2 chosenLocation = Vector2.zero;

        while (!locationFound)
        {
            chosenLocation = new Vector2(Random.Range(corners[0].position.x, corners[1].position.x), Random.Range(corners[0].position.y, corners[1].position.y));

            if (Vector3.Distance(chosenLocation, GameObject.Find("Player").transform.position) > distanceFromPlayer)
                locationFound = true;
        }

        enemiesOnScene.Add(ObjectPooler.objectPoolerInstance.SpawnObject("Enemy", chosenLocation, Quaternion.identity));
        locationFound = false;
    }
    public void EnemyDied(GameObject enemy)
    {
        enemiesOnScene.Remove(enemy);
        enemiesKilled++;
        totalEnemiesKilled++;
        UpdateStats();
        if (enemiesKilled == enemiesInWave)
        {
            waveInProgress = false;
            nextWaveTimerText.enabled = true;
            currentWave = WaveGenerator.waveGeneratorInstance.GenerateWave(currentWave);
            Invoke("StartWave", currentWave.timeBetweenWaves);
            nextWaveTimer = currentWave.timeBetweenWaves;
        }
    }

    void StartWave()
    {
        nextWaveTimerText.enabled = false;
        waveInProgress = true;
        enemiesKilled = 0;
        enemiesSpawned = 0;
        enemiesInWave = currentWave.numberOfEnemies;
        spawnInterval = firstWave.spawnInterval;
        UpdateStats();
    }
    void UpdateStats()
    {
        waveNumber.text = "Wave Number: " + currentWave.waveNumber;
        enemiesInWaveText.text = "Enemies In Wave: " + enemiesInWave;
        enemiesLeft.text = "Enemies Left In Wave:" + (enemiesInWave - enemiesKilled);
        kills.text = "Total Kills: " + totalEnemiesKilled;
    }
}
