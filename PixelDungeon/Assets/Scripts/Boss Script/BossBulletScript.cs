using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletScript : MonoBehaviour
{
    private GameObject player;
    private GameObject env;
    private Rigidbody2D rb;
    private Animator animator;
    public float force;
    private float timer;
    private bool isColliding = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        env = GameObject.FindGameObjectWithTag("Environment");
        animator = GetComponent<Animator>();

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 10)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isColliding) return;  // Prevent multiple triggers
        isColliding = true;

        Debug.Log($"Collision detected with: {other.gameObject.tag} (Object: {other.gameObject.name})");


        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStatus playerStatus = other.gameObject.GetComponent<PlayerStatus>();
            if (playerStatus != null)
            {
                playerStatus.TakeDamage(20);
            }
            StartCoroutine(TriggerBlowAnimation());
        }
        else if (other.gameObject.CompareTag("Environment"))
        {
            StartCoroutine(TriggerBlowAnimation());
        }
    }

    private IEnumerator TriggerBlowAnimation()
    {
        animator.SetTrigger("BlowTrigger");
        rb.velocity = Vector2.zero;  // Stop the fireball's movement
        rb.isKinematic = true;       // Make the fireball kinematic to stop any physics interactions
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
}
