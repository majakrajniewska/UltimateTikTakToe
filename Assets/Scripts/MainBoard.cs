using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class MainBoard : Board
{
    public MiniBoard[,] miniBoards;
    public int currentPlayer;
    public Vector2Int? nextAvailableBoard;
    public MainBoard()
    {
        currentPlayer = 1;
        miniBoards = new MiniBoard[3, 3];
        nextAvailableBoard = null;

        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                miniBoards[i, j] = new MiniBoard();
    }

    public bool MakeMove(int boardX, int boardY, int x, int y)
    {
        if (nextAvailableBoard != null &&
            (nextAvailableBoard.Value.x != boardX || nextAvailableBoard.Value.y != boardY) ||
            miniBoards[boardX, boardY].Winner != 0)
            return false;
        if (miniBoards[boardX, boardY].MakeMove(x, y, currentPlayer) == true)
        {
            board[boardX, boardY] = miniBoards[boardX, boardY].Winner;
            UpdateWinnerStatus();
            currentPlayer = currentPlayer == 1 ? 2 : 1;
            if (miniBoards[x, y].IsFinished)
            {
                nextAvailableBoard = null;
            }
            else
            {
                nextAvailableBoard = new Vector2Int(x, y);
            }
            return true;
        }
        return false;
    }
    
    public void Reset()
    {
        foreach (MiniBoard mb in miniBoards)
        {
            mb.Reset();
        }
        board = new int[3, 3];
        Winner = 0;
        currentPlayer = 1;
        nextAvailableBoard = null;
    }
}
