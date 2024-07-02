using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Slider healthBarSlider;
    private Animator animator;
    private AIChase aiChase;
    private bool isDead = false;
    private bool isTakingDamage = false;
    public float staggerTime = 0.5f;
    private bool isFacingRight = true;


    void Start()
    {
        currentHealth = maxHealth;
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = currentHealth;
        animator = GetComponent<Animator>();
        aiChase = GetComponent<AIChase>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead || isTakingDamage) return;
        currentHealth -= damage;
        Debug.Log("Damage Taken: " + damage + " Current Health: " + currentHealth);

        // Play the damage sound effect here
        AudioManager.Instance.PlaySFX("EnemyTakeDamage");

        StartCoroutine(TakeDamageCoroutine());
        if (currentHealth <= 0)
        {
            Die();
        }
        UpdateHealthBar();
    }

    private IEnumerator TakeDamageCoroutine()
    {
        isTakingDamage = true;
        animator.SetTrigger("TakeDamageTrigger");
        aiChase.enabled = false;
        yield return new WaitForSeconds(staggerTime);
        aiChase.enabled = true;
        isTakingDamage = false;
    }

    void UpdateHealthBar()
    {
        healthBarSlider.value = currentHealth;
    }

    void Die()
    {
        isDead = true;
        Debug.Log("Enemy Died");

        // Play death audio
        AudioManager.Instance.PlaySFX("EnemyDie");

        animator.SetTrigger("DieTrigger");
        aiChase.Die();
        Destroy(gameObject, 2f);

        // Notify the enemy manager
        EnemyManager.instance.EnemyDefeated();
    }

    public void UpdateDirection(bool facingRight)
    {
        if (isFacingRight != facingRight)
        {
            isFacingRight = facingRight;
            FlipHealthBar();
        }
    }

    private void FlipHealthBar()
    {
        Vector3 currentScale = healthBarSlider.transform.localScale;
        currentScale.x *= -1;
        healthBarSlider.transform.localScale = currentScale;
    }
}