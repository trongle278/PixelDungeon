using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (EnemyManager.instance.AllEnemiesDefeated())
            {
                // go to next level
                SceneController.instance.NextLevel();
            }
            else
            {
                // show some message or effect to indicate that not all enemies are defeated
                Debug.Log("You must defeat all enemies before proceeding to the next level!");
            }
        }
    }
}