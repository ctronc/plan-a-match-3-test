using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [SerializeField] private GameObject container;
    [SerializeField] private Button replayButton;

    void Awake()
    {
        container.SetActive(false);
        replayButton.onClick.AddListener(OnReplayClicked);
    }

    void Start()
    {
        gameController.OnGameOver += ShowGameOver;
    }

    void OnDestroy()
    {
        if (gameController != null)
        {
            gameController.OnGameOver -= ShowGameOver;
        }

        if (replayButton != null)
        {
            replayButton.onClick.RemoveListener(OnReplayClicked);
        }
    }

    private void ShowGameOver(int finalScore)
    {
        container.SetActive(true);
    }

    private void OnReplayClicked()
    {
        container.SetActive(false);
        gameController.RestartGame();
    }
}
