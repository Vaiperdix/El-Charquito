using UnityEngine;

public class Obstacle : ObstacleController
{
    private void Awake()
    {
        Awaking();
    }

    private void Start()
    {
        Starting();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Hell"))
        {
            Respawn();
        }
    }
}
