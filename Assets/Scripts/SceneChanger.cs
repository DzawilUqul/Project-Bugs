using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string nextSceneName; // Nama scene berikutnya
    public string previousSceneName; // Nama scene sebelumnya

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Tentukan arah gerakan player
            float playerX = collision.transform.position.x;
            float triggerX = transform.position.x;

            if (playerX < triggerX && !string.IsNullOrEmpty(nextSceneName))
            {
                // Player bergerak ke kanan, pindah ke scene berikutnya
                SceneManager.LoadScene(nextSceneName);
            }
            else if (playerX > triggerX && !string.IsNullOrEmpty(previousSceneName))
            {
                // Player bergerak ke kiri, pindah ke scene sebelumnya
                SceneManager.LoadScene(previousSceneName);
            }
            else
            {
                Debug.LogWarning("No scene to load!");
            }
        }
    }
}
