using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(CanvasGroup))]
public class UIMiniBoard : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField]
    private Button[] cellButtons;
    [SerializeField]
    private TextMeshProUGUI winTextOverlay;
    [SerializeField]
    private GameObject buttonContainer;
    private GameManager gameManager;
    private CanvasGroup canvasGroup;
    private int boardX;
    private int boardY;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (winTextOverlay != null)
        {
            winTextOverlay.gameObject.SetActive(false);
        }
        if (buttonContainer == null)
        {
            buttonContainer = GetComponent<GridLayoutGroup>()?.gameObject;
        }
    }
    
    public void Initialize(GameManager manager, int bX, int bY)
    {
        gameManager = manager;
        boardX = bX;
        boardY = bY;

        for (int i = 0; i < cellButtons.Length; i++)
        {
            int cellIndex = i;
            cellButtons[i].onClick.RemoveAllListeners(); 
            
            cellButtons[i].onClick.AddListener(() => {
                OnCellClicked(cellIndex);
            });
        }
    }
    private void OnCellClicked(int cellIndex)
    {
        int cellX = cellIndex % 3;
        int cellY = cellIndex / 3;
        gameManager.HandlePlayerMove(boardX, boardY, cellX, cellY);
    }
    public void UpdateCells(MiniBoard logicBoard)
    {
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                int player = logicBoard.GetCell(x, y);
                int index = y * 3 + x;

                TextMeshProUGUI buttonText = cellButtons[index].GetComponentInChildren<TextMeshProUGUI>();
                if (player == 1) buttonText.text = "X";
                else if (player == 2) buttonText.text = "O";
                else buttonText.text = "";
                
                cellButtons[index].interactable = (player == 0);
            }
        }
    }
    public void UpdateWinner(int winner)
    {
        if (winner == 0) return;
        if (buttonContainer != null)
        {
            buttonContainer.SetActive(false);
        }
        if (winTextOverlay != null)
        {
            winTextOverlay.gameObject.SetActive(true);
            if (winner == 1) winTextOverlay.text = "X";
            else if (winner == 2) winTextOverlay.text = "O";
            else if (winner == 3) winTextOverlay.text = "#";
        }
    }

    public void SetInteractable(bool isInteractable)
    {
        canvasGroup.interactable = isInteractable;
        canvasGroup.alpha = isInteractable ? 1.0f : 0.4f;
    }
    public void ResetBoard()
    {
        if (buttonContainer != null) buttonContainer.SetActive(true);
        if (winTextOverlay != null) winTextOverlay.gameObject.SetActive(false);

        SetInteractable(true); 
    }
}