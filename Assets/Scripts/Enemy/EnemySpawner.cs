using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner enemySpawnerInstance;
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] float minDistanceFromPlayer;
    [SerializeField] float maxDistanceFromPlayer;
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
        int chosenLocation = 0;

        while (!locationFound)
        {
            chosenLocation = (Random.Range(0, spawnPoints.Count));

            if (Vector3.Distance(spawnPoints[chosenLocation].position, GameObject.Find("Player").transform.position) > minDistanceFromPlayer && Vector3.Distance(spawnPoints[chosenLocation].position, GameObject.Find("Player").transform.position) < maxDistanceFromPlayer)
                locationFound = true;
        }

        enemiesOnScene.Add(ObjectPooler.objectPoolerInstance.SpawnObject("Enemy", spawnPoints[chosenLocation].position, Quaternion.identity));
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
