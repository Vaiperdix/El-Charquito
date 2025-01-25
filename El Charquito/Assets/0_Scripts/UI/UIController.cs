using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Gameplay")]
    [SerializeField] GameObject _gameplayPanel;
    [SerializeField] GameObject[] _lifes;

    [Header("Pause")]
    [SerializeField] GameObject _pausePanel;
    [SerializeField] Button _resumeButton;
    [SerializeField] Button _quitButton;

    private void Start()
    {
        _resumeButton.onClick.AddListener(Resume);
        _quitButton.onClick.AddListener(Quit);
        _pausePanel.SetActive(false);
        _gameplayPanel.SetActive(true);
    }

    public void Resume()
    {
        GameController.Instance.Resume();
        _pausePanel.SetActive(false);
        _gameplayPanel.SetActive(true);
    }

    private void Quit()
    {
        GameController.Instance.Quit();        
    }

    public void Pause()
    {
        _pausePanel.SetActive(true);
        _gameplayPanel.SetActive(false);
    }

    public void Die(int life)
    {
        _lifes[life].SetActive(false);
    }
}
