using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private MainBoard gameLogic;
    [SerializeField]
    private UIMiniBoard[] uiMiniBoards;
    [SerializeField]
    private TextMeshProUGUI mainWinnerText; 

    [SerializeField]
    private Button restartButton;
    void Awake()
    {
        gameLogic = new MainBoard();
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);
        }
    }

    void Start()
    {
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                int index = y * 3 + x;
                uiMiniBoards[index].Initialize(this, x, y);
            }
        }
        RestartGame();
    }

    public void HandlePlayerMove(int boardX, int boardY, int cellX, int cellY)
    {
        bool moveWasSuccessful = gameLogic.MakeMove(boardX, boardY, cellX, cellY);

        if (moveWasSuccessful)
        {
            UpdateUIViews();
        }
    }

    private void UpdateUIViews()
    {
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                int index = y * 3 + x;
                MiniBoard logicBoard = gameLogic.miniBoards[x, y];
                uiMiniBoards[index].UpdateCells(logicBoard);
                uiMiniBoards[index].UpdateWinner(logicBoard.Winner);
            }
        }
        UpdateBoardInteractability();
        if (gameLogic.IsFinished)
        {
            if (gameLogic.Winner == 1) mainWinnerText.text = "X WINS!";
            else if (gameLogic.Winner == 2) mainWinnerText.text = "O WINS!";
            else if (gameLogic.Winner == 3) mainWinnerText.text = "It's a tie!";

            SetAllBoardsInteractable(false);
            restartButton.gameObject.SetActive(true);
        }
        else
        {
            mainWinnerText.text = (gameLogic.currentPlayer == 1 ? "X" : "O") + " moves";
        }
    }

    private void UpdateBoardInteractability()
    {
        Vector2Int? allowedBoardPos = gameLogic.nextAvailableBoard;

        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                int index = y * 3 + x;
                UIMiniBoard currentUIBoard = uiMiniBoards[index];
                bool isBoardFinished = gameLogic.miniBoards[x, y].IsFinished;

                bool canInteract = true;
                if (allowedBoardPos != null)
                {
                    if (allowedBoardPos.Value.x != x || allowedBoardPos.Value.y != y)
                    {
                        canInteract = false;
                    }
                }
                if (isBoardFinished)
                {
                    canInteract = false;
                }
                if (gameLogic.IsFinished) canInteract = false;
                currentUIBoard.SetInteractable(canInteract);
            }
        }
    }
    private void RestartGame()
    {
        gameLogic.Reset();
        foreach (UIMiniBoard board in uiMiniBoards)
        {
            board.ResetBoard();
        }

        UpdateUIViews();
        restartButton.gameObject.SetActive(false);
    }
    private void SetAllBoardsInteractable(bool isInteractable)
    {
         foreach (UIMiniBoard board in uiMiniBoards)
         {
             board.SetInteractable(isInteractable);
         }
    }
}