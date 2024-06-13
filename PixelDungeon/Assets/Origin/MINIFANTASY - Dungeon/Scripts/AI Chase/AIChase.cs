using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    public GameObject player;
    public int moveSpeed = 5; // Tốc độ di chuyển khi đuổi theo nhân vật
    public float chaseDistance = 5f; // Khoảng cách để bắt đầu đuổi theo nhân vật
    public float randomMoveSpeed = 4f; // Tốc độ di chuyển ngẫu nhiên
    public float randomMoveInterval = 0.5f; // Khoảng thời gian để thay đổi hướng di chuyển ngẫu nhiên
    public float restInterval = 1f; // Thời gian nghỉ giữa các lần di chuyển ngẫu nhiên
    public LayerMask obstacleLayer; // Layer của vật thể cản trở

    private Vector2 randomDirection;
    private float randomMoveTimer;
    private float restTimer;
    private bool isResting;
    private bool hitObstacle;

    private float distance;
    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Khóa xoay trục Z để tránh xoay animation 360 độ khi va chạm
        rb.freezeRotation = true;

        // Khởi tạo hướng di chuyển ngẫu nhiên ban đầu
        ChangeRandomDirection();
        isResting = false;

        // Khởi tạo animator với trạng thái đang di chuyển
        animator.SetBool("isMoving", true);
    }

    void Update()
    {
        // Tính toán khoảng cách giữa quái và nhân vật
        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < chaseDistance)
        {
            // Nếu nhân vật ở gần, đuổi theo nhân vật
            ChasePlayer();
        }
        else
        {
            // Nếu nhân vật không ở gần, di chuyển ngẫu nhiên
            RandomMove();
        }
    }

    void ChasePlayer()
    {
        // Tính toán hướng di chuyển về phía nhân vật
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        // Di chuyển quái về phía nhân vật
        rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);

        // Cập nhật Animator
        animator.SetBool("isMoving", true);

        // Lật sprite dựa trên hướng di chuyển
        if (direction.x > 0) // Di chuyển sang phải
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (direction.x < 0) // Di chuyển sang trái
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
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
                // Cập nhật Animator
                animator.SetBool("isMoving", true);
            }
            else
            {
                // Cập nhật Animator
                animator.SetBool("isMoving", false);
            }
        }
        else
        {
            // Giảm giá trị bộ đếm thời gian
            randomMoveTimer -= Time.deltaTime;

            if (randomMoveTimer <= 0)
            {
                // Bắt đầu nghỉ
                isResting = true;
                restTimer = restInterval;
                // Cập nhật Animator
                animator.SetBool("isMoving", false);
                return;
            }

            // Di chuyển quái theo hướng ngẫu nhiên với tốc độ giảm
            rb.MovePosition(rb.position + randomDirection * randomMoveSpeed * Time.deltaTime);

            // Lật sprite dựa trên hướng di chuyển
            if (randomDirection.x > 0) // Di chuyển sang phải
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (randomDirection.x < 0) // Di chuyển sang trái
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

            // Cập nhật Animator
            animator.SetBool("isMoving", true);
        }
    }

    void ChangeRandomDirection()
    {
        // Tạo một hướng ngẫu nhiên
        randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        // Đặt lại bộ đếm thời gian
        randomMoveTimer = randomMoveInterval;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == obstacleLayer)
        {
            // Nếu va chạm với vật thể cản trở, đổi hướng di chuyển ngẫu nhiên
            ChangeRandomDirection();
        }
    }
}