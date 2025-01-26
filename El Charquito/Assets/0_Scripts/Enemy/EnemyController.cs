using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyController : MonoBehaviour
{
    [SerializeField] protected Enemy_SO _enemy_SO;

    [Header("Move")]
    Vector3 _spawnPos = Vector3.zero;
    Rigidbody2D _rigidbody2D;
    int _moveXDirection = 0;
    int _moveYDirection = 0;
    float _destinationPos = 0;

    [Header("Attack")]
    RaycastHit2D _hit;
    bool _isPlayerInArea = false;
    bool _ischasing = false;
    string _playerLayerName = "PlayerLayer";
    int _layerMask = 0;
    Vector2 _origin = Vector2.zero;
    float _distance = 0;

    Animator _animator;
    [Header("Life")]
    int _currentLife = 0;
    SpriteRenderer _spriteRenderer;

    protected void Awaking()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected void Starting()
    {
        _currentLife = _enemy_SO.Life;
        _spawnPos = transform.position;

        _destinationPos = _enemy_SO.MaxDetectableArea.x;
        // Asignar la LayerMask utilizando el nombre del layer
        _layerMask = 1 << LayerMask.NameToLayer(_playerLayerName);
        _distance = _enemy_SO.MaxDetectableArea.y;

        if (!_enemy_SO.IsFLyer)
        {
            _origin = new Vector2(_enemy_SO.MaxDetectableArea.x, transform.position.y);
        }
        else
        {
            _origin = new Vector2(transform.position.x, transform.position.y);
            _distance /= 2;
        }
    }

    #region Update
    protected void GuardOrChase()
    {
        if (!_isPlayerInArea && !_ischasing)
        {
            if (!_enemy_SO.IsFLyer)
            {
                //if (transform.position.y != _spawnPos.y)
                //{
                //    Vector3 pos = transform.position;
                //    pos.y = _spawnPos.y;
                //    transform.position = pos;
                //}
            }
            else
            {
                {
                    Vector3 myPos = transform.position;
                    float toYPos = _spawnPos.y - myPos.y;
                    if (toYPos > 0)
                    {
                        _moveYDirection = 1;
                    }
                    else
                    {
                        _moveYDirection = -1;
                    }
                }
            }
            Guard();
        }
        else
        {
            Chase();
        }
    }

    protected void Guard()
    {
        if (_enemy_SO.ISDynamic)
        {
            _animator.SetBool("isMoving", true);
            if (_destinationPos == _enemy_SO.MaxDetectableArea.x)
            {
                _moveXDirection = -1;
            }
            else
            {
                _moveXDirection = 1;
            }

            if (transform.position.x <= _enemy_SO.MaxDetectableArea.x)
            {
                _destinationPos = _enemy_SO.MaxDetectableArea.y;
            }
            else if (transform.position.x >= _enemy_SO.MaxDetectableArea.y)
            {
                _destinationPos = _enemy_SO.MaxDetectableArea.x;
            }
        }
        else
        {
            _animator.SetBool("isMoving", false);
            _moveYDirection = 0;
            _moveXDirection = 0;
        }
    }

    //protected void Fly()
    //{
    //    if (_enemy_SO.ISDynamic)
    //    {
    //        _animator.SetBool("isMoving", true);
    //        if (_destinationPos == _enemy_SO.MaxDetectableArea.x)
    //        {
    //            _moveXDirection = -1;
    //        }
    //        else
    //        {
    //            _moveXDirection = 1;
    //        }

    //        if (transform.position.x <= _enemy_SO.MaxDetectableArea.x)
    //        {
    //            _destinationPos = _enemy_SO.MaxDetectableArea.y;
    //        }
    //        else if (transform.position.x >= _enemy_SO.MaxDetectableArea.y)
    //        {
    //            _destinationPos = _enemy_SO.MaxDetectableArea.x;
    //        }
    //    }
    //    else
    //    {
    //        _animator.SetBool("isMoving", false);
    //        _moveYDirection = 0;
    //        _moveXDirection = 0;
    //    }
    //}

    protected void Chase()
    {
        Vector3 playerPos = PlayerController.Instance.gameObject.transform.position;
        Vector3 myPos = transform.position;

        if (_enemy_SO.IsFLyer)
        {
            float toYPos = playerPos.y - myPos.y;
            if (toYPos > 0)
            {
                _moveYDirection = 1;
            }
            else
            {
                _moveYDirection = -1;
            }
        }

        float toXPos = playerPos.x - myPos.x;
        if (toXPos > 0)
        {
            _moveXDirection = 1;
        }
        else
        {
            _moveXDirection = -1;
        }
    }
    #endregion

    #region FixedUppdate
    protected void Move()
    {
        _rigidbody2D.linearVelocityX = _moveXDirection * _enemy_SO.MoveSpeed;
        if (_enemy_SO.IsFLyer)
        {
            _rigidbody2D.linearVelocityY = _moveYDirection * _enemy_SO.MoveSpeed;
        }
    }
    #endregion

    #region LateUpdate

    protected void Detector()
    {
        if (_enemy_SO.IsFLyer)
            AirAreaDetector();
        else
            EarthAreaDetector();
    }
    void EarthAreaDetector()
    {
        _hit = Physics2D.Raycast(_origin, Vector2.right, _distance, _layerMask);
        Debug.Log(_distance);
        if (_hit.collider != null)
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

    void AirAreaDetector()
    {
        _hit = Physics2D.CircleCast(_origin, _distance, Vector2.right, _distance, _layerMask);
        if (_hit.collider != null)
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
    #endregion

    protected IEnumerator StopChase()
    {
        yield return new WaitForSeconds(_enemy_SO.ChaseTime);
        _ischasing = false;
    }

    protected void Die(int damage)
    {
        _currentLife -= damage;
        if (_currentLife <= 0)
        {
            _ischasing = false;
            _isPlayerInArea = false;
            //SFXController.Instance.Explosion(transform.position);
            gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(Damage());
        }
    }

    IEnumerator Damage()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        _spriteRenderer.color = Color.white;
    }

    public void Spawn()
    {
        transform.position = _spawnPos;
        gameObject.SetActive(true);
        _currentLife = _enemy_SO.Life;
    }

    void OnDrawGizmosSelected()
    {
        if (_hit.collider == null)
            Gizmos.color = Color.green;
        else
            Gizmos.color = Color.red;

        if (_enemy_SO.IsFLyer)
        {
            Gizmos.DrawWireSphere(_origin, _distance);
        }
        else
        {
            Gizmos.DrawLine(_origin, Vector2.right * _distance);
        }
    }
}
