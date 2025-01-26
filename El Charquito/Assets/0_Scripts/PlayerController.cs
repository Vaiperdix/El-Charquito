using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [Header("Move")]
    Rigidbody2D _rigidbody2D;
    int _modeDirection;
    [SerializeField] float _originalMoveSpeed;
    [SerializeField] float _speedIncrease;
    [SerializeField] int _accelerationLimit;
    [SerializeField] float _jumpSpeed = 3.5f;
    private bool _jumpMarker = false;
    [SerializeField] bool _onAirMarker = false;
    private float _moveSpeed;
    private int _accelerationState;
    private int _lookingDirection;

    SpriteRenderer _spriteRenderer;
    Animator _animator;
    BoxCollider2D _collider;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            _collider = GetComponent<BoxCollider2D>();
        }
        else
        {
            Destroy(gameObject);
        }
        _moveSpeed = _originalMoveSpeed;
        _accelerationState = 0;
    }

    private void Update()
    {
        if (Time.timeScale == 1)
        {
            if (_accelerationState < _accelerationLimit && sameDirection())
            {
                _moveSpeed = _moveSpeed + _speedIncrease;
                _accelerationState++;
            }
            else if (!(sameDirection()))
            {
                if (Input.GetKey(KeyCode.A))
                {
                    _modeDirection = -1;
                    _lookingDirection = -1;
                    _spriteRenderer.flipX = true;
                    _animator.SetBool("isMoving", true);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    _modeDirection = 1;
                    _lookingDirection = 1;
                    _spriteRenderer.flipX = false;
                    _animator.SetBool("isMoving", true);
                }
                else
                {
                    _animator.SetBool("isMoving", false);
                    _modeDirection = 0;
                }
                _moveSpeed = _originalMoveSpeed;
                _accelerationState = 0;
            }
            if (Input.GetKeyDown(KeyCode.W) && !_onAirMarker)
            {
                _animator.SetTrigger("jump");
                _jumpMarker = true;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _animator.SetTrigger("shoot");
                shooting();
            }
        }
    }
    private void FixedUpdate()
    {
        _rigidbody2D.linearVelocityX = _modeDirection * _moveSpeed;
        if (_jumpMarker && !_onAirMarker)
        {
            _onAirMarker = true;
            _rigidbody2D.linearVelocityY = _jumpSpeed;
            _jumpMarker = false;
        }
    }

    private void LateUpdate()
    {
        _onAirMarker = IsGrounded();
    }

    bool IsGrounded()
    {
        int _layerMask = 1 << LayerMask.NameToLayer("EnviromentLayer");

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.55f, _layerMask);
        // Dibujar el Raycast en la Scene view
        Debug.DrawRay(transform.position, Vector2.down * 0.55f, hit.collider != null ? Color.red : Color.green);
        if (hit.collider != null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider != null && (collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("Hell")))
        {
            _collider.enabled = false;
            _rigidbody2D.gravityScale = 0;
            GameController.Instance.PlayerDie();
        }
    }

    public void Spawn(Vector3 pos)
    {
        _collider.enabled = true;
        transform.position = pos;
        _rigidbody2D.gravityScale = 1;
    }

    private bool sameDirection()
    {
        if (((_modeDirection == -1) && (Input.GetKey(KeyCode.A))) || ((_modeDirection == 0) && (!Input.GetKey(KeyCode.A)) && (!Input.GetKey(KeyCode.D))) || ((_modeDirection == 1) && (Input.GetKey(KeyCode.D))))
            return true;
        return false;
    }

    private void shooting()
    {
        BubbleBehaviour bubble = ObjectPooler.Instance.GetPooledObject();
        bubble.shootBubble(transform.position, _lookingDirection);
    }
}
