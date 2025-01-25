using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [Header("UI")]
    [SerializeField] UIController _uiController;
    bool _isPause = false;

    [Header("Chcek Points")]
    int _currentCheckPoint = -1;
    [SerializeField] CheckPoint[] _checkPoints;
    Vector3 _lastCheckPoint = Vector3.zero;

    [Header("Lifes")]
    bool _isGameOver = false;
    int _totalLifes = 3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _isPause = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateCheckPoint();
        PlayerController.Instance.transform.position = _lastCheckPoint;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPause)
            {
                _uiController.Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        Time.timeScale = 0;
        _uiController.Pause();
        _isPause = true;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        _isPause = false;
    }

    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void UpdateCheckPoint()
    {
        _currentCheckPoint++;
        _lastCheckPoint = _checkPoints[_currentCheckPoint].transform.position;
        _checkPoints[_currentCheckPoint].gameObject.SetActive(false);
    }

    public void PlayerDie()
    {
        _totalLifes--;
        _uiController.Die(_totalLifes);
        if (_totalLifes == 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {

    }
}
