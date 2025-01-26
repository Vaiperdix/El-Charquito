using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] protected Obstacle_SO _obstacle_SO;

    [Header("Move")]
    Rigidbody2D _rigidbody2D;
    Vector3 _spawnPos = Vector3.zero;
    protected void Awaking()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    protected void Starting()
    {
        _spawnPos =  transform.position;
        if (_obstacle_SO.ISDinamic)
        {
            _rigidbody2D.gravityScale = _obstacle_SO.Gravity;
        }
    }

    protected void Respawn()
    {
        transform.position = _spawnPos;
    }
}
