using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI movesText;

    void Start()
    {
        gameController.OnScoreChanged += UpdateScore;
        gameController.OnMovesChanged += UpdateMoves;
    }

    void OnDestroy()
    {
        if (gameController != null)
        {
            gameController.OnScoreChanged -= UpdateScore;
            gameController.OnMovesChanged -= UpdateMoves;
        }
    }

    private void UpdateScore(int score)
    {
        scoreText.text = $"{score}";
    }

    private void UpdateMoves(int moves)
    {
        movesText.text = $"{moves}";
    }
}
