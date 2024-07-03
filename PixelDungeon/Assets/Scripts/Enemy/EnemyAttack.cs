using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject swordHitbox; // Reference to the sword's hitbox
    public float attackCooldown = 0.5f; // Reduced time between attacks for faster attack speed
    public float hitboxActiveTime = 0.2f; // Duration for which hitbox is active

    private Animator animator;
    private bool isAttacking;
    private float attackTimer;

    public bool IsAttacking => isAttacking; // Property to check if the enemy is attacking

    void Start()
    {
        animator = GetComponent<Animator>();
        isAttacking = false;
        attackTimer = 0;
        swordHitbox.SetActive(false); // Ensure hitbox is initially inactive
    }

    void Update()
    {
        if (isAttacking)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                isAttacking = false;
                animator.SetBool("isAttacking", false);
            }
        }
    }

    public void Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            attackTimer = attackCooldown;
            animator.SetBool("isAttacking", true);
            AudioManager.Instance.PlaySFX("EnemyAttack");
            StartCoroutine(ActivateHitbox());
        }
    }

    private IEnumerator ActivateHitbox()
    {        
        swordHitbox.SetActive(true);
        yield return new WaitForSeconds(hitboxActiveTime); // Active duration of the hitbox
        swordHitbox.SetActive(false);
    }
}
