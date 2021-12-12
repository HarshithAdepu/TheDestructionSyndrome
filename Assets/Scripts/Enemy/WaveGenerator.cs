using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public int waveNumber;
    public int numberOfEnemies;
    public int numberofThugs;
    public int numberofBrutes;
    public int numberofHenchmen;
    public Vector3Int distrubution;
    public float timeBetweenWaves;
    public float spawnInterval;
}

public class WaveGenerator : MonoBehaviour
{
    public static WaveGenerator waveGeneratorInstance;
    void Start()
    {
        waveGeneratorInstance = this;
    }
    public Wave GenerateWave(Wave lastWave)
    {
        Wave generatedWave = new Wave();
        generatedWave.waveNumber = lastWave.waveNumber + 1;
        generatedWave.numberOfEnemies = lastWave.numberOfEnemies + Random.Range(2, 6);
        generatedWave.distrubution = lastWave.distrubution;

        if (generatedWave.distrubution.x >= 20)
        {
            generatedWave.distrubution.x -= 2;
            generatedWave.distrubution.y++;
            generatedWave.distrubution.z++;
        }

        for (int i = 0; i < generatedWave.numberOfEnemies; i++)
        {
            int enemyChoice = Random.Range(0, (generatedWave.distrubution.x + generatedWave.distrubution.y + generatedWave.distrubution.z + 1));

            if (enemyChoice >= generatedWave.distrubution.x && enemyChoice < generatedWave.distrubution.y)
                generatedWave.numberofThugs++;
            else if (enemyChoice >= (generatedWave.distrubution.x + generatedWave.distrubution.y) && enemyChoice < (generatedWave.distrubution.x + generatedWave.distrubution.y + generatedWave.distrubution.z))
                generatedWave.numberofBrutes++;
            else
                generatedWave.numberofHenchmen++;
        }

        generatedWave.timeBetweenWaves = lastWave.timeBetweenWaves;
        if (generatedWave.timeBetweenWaves > 10)
            generatedWave.timeBetweenWaves--;

        return generatedWave;
    }
}
