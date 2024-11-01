using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public TextMeshProUGUI waveCounterText;
    public List<EnemySpawnPoint> spawnPoints;
    public int enemiesAddedPerWave = 2; // Number of enemies to add per wave
    private List<GameObject> activeEnemies = new List<GameObject>();
    private int currentWave;

    private void Start()
    {
        Debug.Log("WaveManager Start: Initializing wave system.");
        currentWave = 1;
        UpdateWaveCounterUI();
        StartNextWave();
    }

    public void NextWave()
    {
        Debug.Log("Wave completed. Starting next wave");
        currentWave++;
        UpdateWaveCounterUI();
        StartNextWave();
    }

    private void StartNextWave()
    {
        Debug.Log("Starting Wave: " + currentWave);

        // Clear the previous list of active enemies (if there are any left over from previous waves)
        activeEnemies.Clear();

        // Spawn enemies at each spawn point
        foreach (EnemySpawnPoint spawnPoint in spawnPoints)
        {
            spawnPoint.numEnemies += enemiesAddedPerWave; // Increment the number of enemies per wave
            for (int i = 0; i < spawnPoint.numEnemies; i++)
            {
                GameObject enemy = spawnPoint.SpawnEnemy(); // Adjust SpawnEnemy() to return the instantiated enemy
                activeEnemies.Add(enemy); // Add the spawned enemy to the list
            }
        }
    }

    public void EnemyDefeated(GameObject enemy)
    {
        // Remove the defeated enemy from the list
        Debug.Log("Enemy defeated. Removing from activeEnemies list.");
        activeEnemies.Remove(enemy);
        Debug.Log("Active enemies left: " + activeEnemies.Count);

        // Check if all enemies are defeated
        if (activeEnemies.Count <= 0)
        {
            NextWave();
        }
    }


    private void UpdateWaveCounterUI()
    {
        waveCounterText.text = "Wave: " + currentWave;
    }
}