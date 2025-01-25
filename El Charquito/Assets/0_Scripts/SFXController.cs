using UnityEngine;

public class SFXController : MonoBehaviour
{
    public static SFXController Instance { get; private set; }

    [Header("Explosions")]
    int _currentexplosion = 0;
    [SerializeField] ParticleSystem[] _explosions;

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
    }

    public void Explosion(Vector3 pos)
    {
        if (_currentexplosion >= _explosions.Length)
            _currentexplosion = 0;

        _explosions[_currentexplosion].gameObject.transform.position = pos;
        _explosions[_currentexplosion].Play();
        _currentexplosion ++;
    }
}
