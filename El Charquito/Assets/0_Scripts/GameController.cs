using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    [SerializeField] Enemy[] _enemies;

    [System.Obsolete]
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _isPause = false;
            _enemies = FindObjectsOfType<Enemy>();
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
        StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(0.2f);
        Time.timeScale = 0.95f;
        foreach (Enemy enemy in _enemies)
        {
            enemy.Spawn();
        }
        yield return new WaitForSeconds(1f);
        if (_totalLifes == 0)
        {
            Time.timeScale = 1f;
            GameOver();
        }
        else
        {
            PlayerController.Instance.Spawn(_lastCheckPoint);
            Time.timeScale = 1f;
        }
    }

    void GameOver()
    {
        SceneManager.LoadScene(0);
    }
}
