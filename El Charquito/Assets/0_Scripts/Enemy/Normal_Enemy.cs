using UnityEngine;

public class Normal_Enemy : EnemyController
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
        WalkOrChase();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        Detector();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bubble"))
        {
            Die();
        }
    }
}
