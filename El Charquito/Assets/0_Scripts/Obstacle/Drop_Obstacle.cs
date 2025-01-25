using UnityEngine;

public class Drop_Obstacle : ObstacleController
{
    private void Awake()
    {
        Awaking();
    }

    private void Start()
    {
        Starting();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_obstacle_SO.ISDinamic && collision.collider.CompareTag("Hell"))
        {
            Respawn();
        }
    }
}
