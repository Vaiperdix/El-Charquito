using UnityEngine;

[CreateAssetMenu(fileName = "Enemy_SO", menuName = "Scriptable Objects/Enemy")]
public class Enemy_SO : ScriptableObject
{
    [SerializeField] AnimatorOverrideController _animatorOverrideController;

    [Header("Move")]
    [SerializeField] bool _isWalker = false;
    [SerializeField] float _moveSpeed = 0;
    [SerializeField] Vector2 _maxWalkableArea = Vector2.zero;

    [Header("Attack")]
    [SerializeField] float _chaseTime = 0;

    public AnimatorOverrideController AnimatorOverrideController { get { return _animatorOverrideController; } }
    public bool ISWalker { get { return _isWalker; } }
    public float MoveSpeed { get { return _moveSpeed; } }
    public Vector2 MaxWalkableArea { get { return _maxWalkableArea; } }

    public float ChaseTime { get { return _chaseTime; } }
}
