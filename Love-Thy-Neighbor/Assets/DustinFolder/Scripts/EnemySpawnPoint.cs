using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    public int numEnemies = 5;
    public GameObject SpawnEnemy()
    {
        int randomIndex = Random.Range(0, enemyPrefabs.Count);
        return Instantiate(enemyPrefabs[randomIndex], transform.position, Quaternion.identity);
    }


}
