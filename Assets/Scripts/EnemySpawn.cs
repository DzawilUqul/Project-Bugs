using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab enemy
    public Transform leftLimit;    // Titik batas kiri untuk enemy ini
    public Transform rightLimit;  // Titik batas kanan untuk enemy ini

    void Start()
    {
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        if (enemyPrefab != null)
        {
            // Spawn enemy di posisi spawn point ini
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

            // Set referensi leftLimit dan rightLimit ke script EnemyBehaviour
            EnemyBehaviour enemyBehaviour = enemy.GetComponent<EnemyBehaviour>();
            if (enemyBehaviour != null)
            {
                enemyBehaviour.leftLimit = leftLimit;
                enemyBehaviour.rightLimit = rightLimit;
            }
            else
            {
                Debug.LogWarning("EnemyBehaviour script not found on enemy!");
            }
        }
    }
}
