using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatus : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Healthbar healthBar;
    public TextMeshProUGUI healthText;
    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        animator = GetComponent<Animator>();
        UpdateHealthText();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.SetHealth(currentHealth);
        UpdateHealthText();

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

    // Thêm hàm để cập nhật TextMeshPro
    void UpdateHealthText()
    {
        healthText.text = currentHealth.ToString() + "/" + maxHealth.ToString();
    }
}
