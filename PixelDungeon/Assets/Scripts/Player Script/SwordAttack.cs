using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public Collider2D swordCollider;
    public int damage = 20;
    public float attackDuration = 0.3f;
    public float attackOffset = 0.5f;  // Khoảng cách từ tâm nhân vật đến thanh kiếm

    private Vector3 originalPosition;

    private void Start()
    {
        swordCollider = GetComponent<Collider2D>();
        swordCollider.enabled = false;
        originalPosition = transform.localPosition;
    }

    public void Attack(bool isAttackingRight)
    {
        // Điều chỉnh vị trí của collider
        Vector3 attackPosition = originalPosition;
        attackPosition.x = isAttackingRight ? attackOffset : -attackOffset;
        transform.localPosition = attackPosition;

        swordCollider.enabled = true;
        Invoke("StopAttack", attackDuration);
    }

    private void StopAttack()
    {
        swordCollider.enabled = false;
        transform.localPosition = originalPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
        else if (collision.CompareTag("Boss"))
        {
            Boss boss = collision.GetComponent<Boss>();
            if (boss != null)
            {
                boss.TakeDamage(damage);
            }
        }
    }
}
