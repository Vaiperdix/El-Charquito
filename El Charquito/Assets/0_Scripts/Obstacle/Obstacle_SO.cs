using UnityEngine;

[CreateAssetMenu(fileName = "Obstacle_SO", menuName = "Scriptable Objects/Obstacle")]
public class Obstacle_SO : ScriptableObject
{
    [SerializeField] bool _isDinamic = false;
    [SerializeField] float _gravity = 0;


    public bool ISDinamic { get { return _isDinamic; } }
    public float Gravity { get { return _gravity; } }
}
