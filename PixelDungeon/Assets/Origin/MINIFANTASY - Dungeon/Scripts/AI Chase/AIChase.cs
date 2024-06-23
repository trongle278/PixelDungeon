using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    public GameObject player;
    public int moveSpeed = 6;
    public float chaseDistance = 5f;
    public float attackDistance = 1.5f; // Distance within which the enemy will attack
    public float randomMoveSpeed = 4f;
    public float randomMoveInterval = 0.5f;
    public float restInterval = 1f;
    public LayerMask obstacleLayer;
    public float minTimeBetweenSteps = 0.3f; // Minimum time between each step sound

    private Vector2 randomDirection;
    private float randomMoveTimer;
    private float restTimer;
    private bool isResting;
    private bool isDead;

    private float distance;
    private Rigidbody2D rb;
    private Animator animator;
    private Enemy enemy;
    private EnemyAttack enemyAttack;
    private float lastStepTime; // Time of the last step sound

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
        enemyAttack = GetComponent<EnemyAttack>();

        rb.freezeRotation = true;
        ChangeRandomDirection();
        isResting = false;
        animator.SetBool("isMoving", true);

        lastStepTime = Time.time;
    }

    void Update()
    {
        if (isDead || enemyAttack.IsAttacking) return; // Prevent movement while attacking or dead

        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < attackDistance)
        {
            AttackPlayer();
        }
        else if (distance < chaseDistance)
        {
            ChasePlayer();
        }
        else
        {
            RandomMove();
        }
    }

    void ChasePlayer()
    {
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
        animator.SetBool("isMoving", true);
        UpdateFacingDirection(direction.x);

        // Play walking sound effect if enough time has passed since last step
        if (Time.time >= lastStepTime + minTimeBetweenSteps)
        {
            AudioManager.Instance.PlaySFX("EnemyWalk");
            lastStepTime = Time.time;
        }
    }

    void AttackPlayer()
    {
        rb.velocity = Vector2.zero; // Stop moving to attack
        animator.SetBool("isMoving", false);
        enemyAttack.Attack();
    }

    void RandomMove()
    {
        if (isResting)
        {
            restTimer -= Time.deltaTime;
            if (restTimer <= 0)
            {
                isResting = false;
                ChangeRandomDirection();
                animator.SetBool("isMoving", true);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
        }
        else
        {
            randomMoveTimer -= Time.deltaTime;

            if (randomMoveTimer <= 0)
            {
                isResting = true;
                restTimer = restInterval;
                animator.SetBool("isMoving", false);
                return;
            }

            rb.MovePosition(rb.position + randomDirection * randomMoveSpeed * Time.deltaTime);
            UpdateFacingDirection(randomDirection.x);
            animator.SetBool("isMoving", true);
        }
    }

    void ChangeRandomDirection()
    {
        randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        randomMoveTimer = randomMoveInterval;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == obstacleLayer)
        {
            ChangeRandomDirection();
        }
    }

    public void Die()
    {
        isDead = true;
        animator.SetTrigger("DieTrigger");
        Destroy(gameObject, 1.2f);
    }

    private void UpdateFacingDirection(float directionX)
    {
        bool isFacingRight = directionX > 0;
        transform.localScale = new Vector3(isFacingRight ? Mathf.Abs(transform.localScale.x) : -Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        enemy.UpdateDirection(isFacingRight);
    }
}
