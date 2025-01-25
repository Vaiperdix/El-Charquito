using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Enemy_SO _enemy_SO;

    [Header("Move")]
    Rigidbody2D _rigidbody2D;
    int _moveDirection = 0;
    float _destinationPos = 0;

    [Header("Attack")]
    bool _isPlayerInArea = false;
    bool _ischasing = false;
    string _playerLayerName = "PlayerLayer";
    int _layerMask = 0;
    Vector2 _origin = Vector2.zero;
    float _distance = 0;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _destinationPos = _enemy_SO.MaxWalkableArea.x;
        // Asignar la LayerMask utilizando el nombre del layer
        _layerMask = 1 << LayerMask.NameToLayer(_playerLayerName);

        _origin = new Vector2(_enemy_SO.MaxWalkableArea.x, transform.position.y);
        if (_enemy_SO.MaxWalkableArea.x >= 0)
        {
            _distance = _enemy_SO.MaxWalkableArea.y - _enemy_SO.MaxWalkableArea.x;
        }
        else
        {
            _distance = _enemy_SO.MaxWalkableArea.y + Math.Abs(_enemy_SO.MaxWalkableArea.x);
        }
    }

    void Update()
    {
        if (!_isPlayerInArea && !_ischasing)
        {
            if (_enemy_SO.ISWalker)
            {
                if (_destinationPos == _enemy_SO.MaxWalkableArea.x)
                {
                    _moveDirection = -1;
                }
                else
                {
                    _moveDirection = 1;
                }

                if (transform.position.x <= _enemy_SO.MaxWalkableArea.x)
                {
                    _destinationPos = _enemy_SO.MaxWalkableArea.y;
                }
                else if (transform.position.x >= _enemy_SO.MaxWalkableArea.y)
                {
                    _destinationPos = _enemy_SO.MaxWalkableArea.x;
                }
            }
        }
        else
        {
            _moveDirection = 0;
            //float myPos = transform.position.x;
            //float playerPos = PlayerController.Instance.gameobject.transform.position.x;
            //float toPos = myPos - playerPos;
            //if (myPos > toPos)
            //{
            //    _destinationPos = 1;
            //}
            //else
            //{
            //    _destinationPos = -1;
            //}
        }
    }

    private void FixedUpdate()
    {
        _rigidbody2D.linearVelocityX = _moveDirection * _enemy_SO.MoveSpeed;
    }

    private void LateUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(_origin, Vector2.right, _distance, _layerMask);
        Debug.DrawRay(_origin, Vector2.right * _distance, (hit.collider != null ? Color.red : Color.green));
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            if (!_isPlayerInArea)
            {
                StopCoroutine(StopChase());
                _ischasing = true;
            }
            _isPlayerInArea = true;
        }
        else
        {
            if (_isPlayerInArea)
            {
                StartCoroutine(StopChase());
            }
            _isPlayerInArea = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bubble"))
        {
            Die();
        }
    }

    IEnumerator StopChase()
    {
        yield return new WaitForSeconds(_enemy_SO.ChaseTime);
        _ischasing = false;
    }

    void Die()
    {
        SFXController.Instance.Explosion(transform.position);
        gameObject.SetActive(false);
    }

    public void Spawn()
    {
        gameObject.SetActive(true);
    }
}
