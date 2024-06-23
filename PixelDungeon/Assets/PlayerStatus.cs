using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Healthbar healthBar;
    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health doesn't go below 0
        healthBar.SetHealth(currentHealth);

        // Play damage audio
        AudioManager.Instance.PlaySFX("TakeDamage");

        if (currentHealth > 0)
        {
            animator.SetTrigger("TakeDamage");
        }
        else
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player died!");
        animator.SetTrigger("Die");
        // Optionally, delay the destruction to allow the death animation to play
        StartCoroutine(DestroyAfterAnimation());
    }

    IEnumerator DestroyAfterAnimation()
    {
        // Assuming the death animation is 1 second long, adjust this value as needed
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }
}
