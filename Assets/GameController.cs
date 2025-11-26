using System;
using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private const int INITIAL_MOVES = 5;

    [SerializeField] private Grid grid;

    private int score;
    private int moves;

    public event Action<int> OnScoreChanged;
    public event Action<int> OnMovesChanged;
    public event Action<int> OnGameOver;

    void Start()
    {
        if (grid != null)
        {
            grid.OnBlocksCollected += OnBlocksCollected;
        }

        ResetGame();
    }

    void OnDestroy()
    {
        if (grid != null)
        {
            grid.OnBlocksCollected -= OnBlocksCollected;
        }
    }

    private void OnBlocksCollected(int blockCount)
    {
        if (moves <= 0) return;

        moves--;
        OnMovesChanged?.Invoke(moves);

        score += blockCount;
        OnScoreChanged?.Invoke(score);

        if (moves == 0)
        {
            StartCoroutine(ShowGameOverWithDelay());
        }
    }

    private IEnumerator ShowGameOverWithDelay()
    {
        yield return new WaitForSeconds(1f);
        OnGameOver?.Invoke(score);
    }

    public void RestartGame()
    {
        ResetGame();
    }

    private void ResetGame()
    {
        score = 0;
        moves = INITIAL_MOVES;

        OnScoreChanged?.Invoke(score);
        OnMovesChanged?.Invoke(moves);
    }
}
