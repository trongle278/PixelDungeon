using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwordHitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStatus playerStatus = other.GetComponent<PlayerStatus>();
            if (playerStatus != null)
            {
                playerStatus.TakeDamage(10); // Example damage value
            }
        }
    }
}
