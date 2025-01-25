using UnityEngine;

[CreateAssetMenu(fileName = "Obstacle_SO", menuName = "Scriptable Objects/Obstacle")]
public class Obstacle_SO : ScriptableObject
{
    [SerializeField] AnimatorOverrideController _animatorOverrideController;
    [SerializeField] bool _isDinamic = false;
    [SerializeField] float _moveSpeed = 0;


    public AnimatorOverrideController AnimatorOverrideController { get { return _animatorOverrideController; } }
    public bool ISDinamic { get { return _isDinamic; } }
    public float MoveSpeed { get { return _moveSpeed; } }
}
