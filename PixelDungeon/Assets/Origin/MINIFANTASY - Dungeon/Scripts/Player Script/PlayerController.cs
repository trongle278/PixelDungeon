using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public int moveSpeed = 5;

    public float collisionOffset = 0.05f;

    public ContactFilter2D movementFilter;

    Rigidbody2D rb;

    Animator animator;

    Vector2 movementInput;

    SpriteRenderer spriteRenderer;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();


    public float minTimeBetweenSteps = 0.3f;
    float timeSinceLastStep = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb.freezeRotation = true; // Khóa trục Z
    }

    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            bool success = TryMove(movementInput);

            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));

                if (!success)
                {
                    success = TryMove(new Vector2(movementInput.y, 0));
                }
            }

            animator.SetBool("isMoving", success);

            // Phát âm thanh nếu đủ thời gian giữa các bước
            if (success && Time.time >= timeSinceLastStep + minTimeBetweenSteps)
            {
                AudioManager.Instance.PlaySFX("Walk");
                timeSinceLastStep = Time.time;
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        // Set direction of sprite to movement direction
        if (movementInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (movementInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private bool TryMove(Vector2 direction)
    {
        int count = rb.Cast(
               movementInput,
               movementFilter,
               castCollisions,
               moveSpeed * Time.fixedDeltaTime + collisionOffset);

        if (count == 0)
        {
            rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    public void OnFire()
    {
        animator.SetTrigger("swordAttack");
    }
}
