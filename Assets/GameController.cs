using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private const int INITIAL_MOVES = 5;
    private const int POINTS_PER_MOVE = 10;

    private int score;
    private int moves;

    public event Action<int> OnScoreChanged;
    public event Action<int> OnMovesChanged;
    public event Action<int> OnGameOver;

    void Start()
    {
        ResetGame();
    }

    // Called by Make Move button
    public void MakeMove()
    {
        if (moves <= 0) return;

        moves--;
        OnMovesChanged?.Invoke(moves);

        score += POINTS_PER_MOVE;
        OnScoreChanged?.Invoke(score);

        // Check if game is over
        if (moves == 0)
        {
            OnGameOver?.Invoke(score);
        }
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
