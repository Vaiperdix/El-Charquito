using UnityEngine;

public class Enemy : EnemyController
{
    private void Awake()
    {
        Awaking();
    }

    private void Start()
    {
        Starting();
    }

    private void Update()
    {
        GuardOrChase();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        Detector();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bubble") || collision.collider.CompareTag("Hell"))
        {
            Die();
        }
    }
}
