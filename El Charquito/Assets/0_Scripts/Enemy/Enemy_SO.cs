using UnityEngine;

[CreateAssetMenu(fileName = "Enemy_SO", menuName = "Scriptable Objects/Enemy")]
public class Enemy_SO : ScriptableObject
{
    [Header("Move")]
    [SerializeField] bool _isFLyer = false;
    [SerializeField] bool _isDynamic = false;
    [SerializeField] float _moveSpeed = 0;
    [SerializeField] Vector2 _maxDetectableArea = Vector2.zero;

    [Header("Attack")]
    [SerializeField] float _chaseTime = 0;
    [SerializeField] int _life = 0;

    public bool IsFLyer { get { return _isFLyer; } }
    public bool ISDynamic { get { return _isDynamic; } }
    public float MoveSpeed { get { return _moveSpeed; } }
    public Vector2 MaxDetectableArea { get { return _maxDetectableArea; } }

    public float ChaseTime { get { return _chaseTime; } }
    public int Life { get { return _life; } }
}
