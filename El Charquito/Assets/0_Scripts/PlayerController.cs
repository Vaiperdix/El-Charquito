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
    [SerializeField] float _jumpSpeed;
    private float _moveSpeed;
    private int _accelerationState;
    [SerializeField] ObjectPooler _bubblesPool;
    private int _lookingDirection;
    


    private bool sameDirection(){
        if ( ((_modeDirection==-1)&&(Input.GetKey(KeyCode.A))) || ((_modeDirection==0)&&(!Input.GetKey(KeyCode.A))&&(!Input.GetKey(KeyCode.D))) || ((_modeDirection==1)&&(Input.GetKey(KeyCode.D))))
            return true;
        return false;
    }

    private void shooting(){
        GameObject bubble = _bubblesPool.GetPooledObject();
        bubble.transform.position = transform.position;
        bubble.SetActive(true);
        BubbleBehaviour script = bubble.GetComponent<BubbleBehaviour>();
        script.shootBubble(_lookingDirection);
    }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        _moveSpeed = _originalMoveSpeed;
        _accelerationState = 0;
    }

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_accelerationState<_accelerationLimit && sameDirection())
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

            }
            else if (Input.GetKey(KeyCode.D))
            {
                _modeDirection = 1;
                _lookingDirection = 1;
            }
            else
            {
                _modeDirection = 0;
            }
            _moveSpeed = _originalMoveSpeed;
            _accelerationState = 0;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            shooting();
        }
    }
    private void FixedUpdate()
    {
        _rigidbody2D.linearVelocityX = _modeDirection * _moveSpeed;
    }
}
