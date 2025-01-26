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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider != null && collision.collider.CompareTag("Enviroment"))
            _onAirMarker = false;
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

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
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
            }
            else if (Input.GetKey(KeyCode.D))
            {
                _modeDirection = 1;
                _lookingDirection = 1;
                _spriteRenderer.flipX = false;
            }
            else
            {
                _modeDirection = 0;
            }
            _moveSpeed = _originalMoveSpeed;
            _accelerationState = 0;
        }
        if (Input.GetKeyDown(KeyCode.W) && !_onAirMarker)
        {
            _jumpMarker = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shooting();
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
}
