using UnityEngine;

public class BubbleBehaviour : MonoBehaviour
{
    private int _movementDirection;
    [SerializeField] float _originalMoveSpeedX; 
    private float _moveSpeedX;
    [SerializeField] float _speedDecreaseX;
    [SerializeField] float _originalMoveSpeedY;
    private float _moveSpeedY;
    private int _oscilationState;
    Rigidbody2D _rigidbody2D;
    
     private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.SetActive(false);
    }
    void Awake()
    {
        _moveSpeedX = _originalMoveSpeedX;
        _moveSpeedY = _originalMoveSpeedY; 
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _oscilationState = 1;
        
    }

    void Update()
    {
        if(gameObject.activeSelf)
        {
            
        }
    }

    public void shootBubble(int direction)
    {
        _rigidbody2D.linearVelocityX = direction * _moveSpeedX;
        _rigidbody2D.linearVelocityY = _moveSpeedY;
        _movementDirection = direction;

    }
}
