using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public int maxHealth = 350;
    private int currentHealth;
    public Slider healthBarSlider;
    private Animator animator;
    private AIChase aiChase;
    private bool isDead = false;
    private bool isTakingDamage = false;
    public float staggerTime = 0.5f;
    private bool isFacingRight = true;
    public int healthLossThreshold = 50;  // Amount of HP loss to trigger charge
    private int nextChargeThreshold;  // Next health value to trigger charge
    private bool isCharging = false;

    public float chargeDelay = 2f; // Delay before actual charging action
    private Rigidbody2D rb;
    private bool isFlying = false;  // Track if the boss is flying

    public PolygonCollider2D groundCollider;  // Ground state collider
    public BoxCollider2D flyCollider;     // Flying state collider
    public BossPhase2Shooting bossPhase2Shooting;  // Reference to the phase 2 shooting script

    void Start()
    {
        currentHealth = maxHealth;
        nextChargeThreshold = currentHealth - healthLossThreshold;
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = currentHealth;
        animator = GetComponent<Animator>();
        aiChase = GetComponent<AIChase>();
        rb = GetComponent<Rigidbody2D>();
        bossPhase2Shooting = GetComponent<BossPhase2Shooting>();

        groundCollider.enabled = true;  // Ensure ground collider is enabled initially
        flyCollider.enabled = false;    // Ensure flying collider is disabled initially
        bossPhase2Shooting.enabled = false;  // Ensure phase 2 shooting script is disabled initially
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
        else if (currentHealth <= nextChargeThreshold)
        {
            StartCoroutine(StartChargingCoroutine());
            nextChargeThreshold -= healthLossThreshold;
        }

        // Trigger phase 2 if HP drops below 200
        if (!isFlying && currentHealth <= 200)
        {
            EnterFlyingPhase();
        }

        UpdateHealthBar();
    }

    private IEnumerator StartChargingCoroutine()
    {
        isCharging = true;
        animator.SetTrigger("ChargeTrigger");
        aiChase.enabled = false;
        rb.velocity = Vector2.zero;

        // Charging animation delay
        yield return new WaitForSeconds(chargeDelay);

        // Perform the actual charging action
        yield return new WaitForSeconds(2f);

        // Shoot fireball
        BossShooting bossShooting = GetComponent<BossShooting>();
        if (bossShooting != null)
        {
            Debug.Log("Calling ShootFireball"); // Add debug log to verify method call
            bossShooting.ShootFireball();
        }

        // End of charging
        isCharging = false;
        aiChase.enabled = true;
    }

    private IEnumerator TakeDamageCoroutine()
    {
        isTakingDamage = true;
        animator.SetTrigger("TakeDamageTrigger");
        aiChase.enabled = false;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(staggerTime);
        aiChase.enabled = true;
        isTakingDamage = false;
    }

    private void EnterFlyingPhase()
    {
        isFlying = true;
        aiChase.enabled = false;  // Disable ground movement
        rb.gravityScale = 0;      // Disable gravity if flying
        animator.SetBool("isFlying", true);  // Trigger fly animation
        groundCollider.enabled = false;  // Disable ground collider
        flyCollider.enabled = true;  // Enable fly collider
        bossPhase2Shooting.enabled = true;  // Enable phase 2 shooting script
    }

    void UpdateHealthBar()
    {
        healthBarSlider.value = currentHealth;
    }

    void Die()
    {
        isDead = true;
        Debug.Log("Boss Died");

        // Play death audio
        AudioManager.Instance.PlaySFX("EnemyDie");

        animator.SetTrigger("DieTrigger");
        aiChase.Die();
        Destroy(gameObject, 2f);

        // Notify the enemy manager
        EnemyManager.instance.EnemyDefeated();

        // Load the GameOver scene
        SceneManager.LoadScene("GameOver");
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
