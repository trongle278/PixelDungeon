using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwordHitbox : MonoBehaviour
{
    public int enemyattackdamage = 10;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Enemy attack: " + enemyattackdamage);
            PlayerStatus playerStatus = other.GetComponent<PlayerStatus>();
            if (playerStatus != null)
            {
                playerStatus.TakeDamage(enemyattackdamage); // Example damage value
            }
        }
    }
}
