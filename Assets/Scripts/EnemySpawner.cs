using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; 
    public Transform[] spawnPoints;   
    private bool hasSpawned = false;  
    private int currentEnemyIndex = 0; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Cek jika player masuk ke area trigger dan enemy belum di-spawn
        if (collision.CompareTag("Player") && !hasSpawned)
        {
            SpawnEnemies();
            hasSpawned = true; 
        }
    }

    private void SpawnEnemies()
    {
        if (enemyPrefabs.Length > 0 && spawnPoints.Length > 0)
        {
            // Loop melalui semua spawn points
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                
                GameObject enemyPrefab = enemyPrefabs[currentEnemyIndex % enemyPrefabs.Length];
                
                Instantiate(enemyPrefab, spawnPoints[i].position, spawnPoints[i].rotation);
                Debug.Log(enemyPrefab.name + " spawned at " + spawnPoints[i].position);

                currentEnemyIndex++;
            }
        }
        else
        {
            Debug.LogError("EnemyPrefabs or SpawnPoints are not assigned!");
        }
    }
}
