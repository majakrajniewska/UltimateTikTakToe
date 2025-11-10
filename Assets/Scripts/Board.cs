public abstract class Board
{
    protected int[,] board = new int[3, 3];
    public int Winner { get; protected set; } = 0; 
    public bool IsFinished => Winner != 0;
    public int GetCell(int x, int y)
    {
        return board[x, y];
    }
    protected void UpdateWinnerStatus()
    {
        if (IsFinished) return;

        for (int x = 0; x < 3; x++)
        {
            if (board[x, 0] != 0 && board[x, 0] != 3 && 
                board[x, 0] == board[x, 1] && board[x, 1] == board[x, 2])
            {
                Winner = board[x, 0];
                return;
            }
        }

        for (int y = 0; y < 3; y++)
        {
            if (board[0, y] != 0 && board[0, y] != 3 && 
                board[0, y] == board[1, y] && board[1, y] == board[2, y])
            {
                Winner = board[0, y];
                return;
            }
        }

        if (board[0, 0] != 0 && board[0, 0] != 3 && 
            board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
        {
            Winner = board[0, 0];
            return;
        }

        if (board[2, 0] != 0 && board[2, 0] != 3 && 
            board[2, 0] == board[1, 1] && board[1, 1] == board[0, 2])
        {
            Winner = board[2, 0];
            return;
        }

        bool isFull = true;
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                if (board[x, y] == 0)
                {
                    isFull = false;
                    break;
                }
            }
            if (!isFull) break;
        }

        if (isFull)
        {
            Winner = 3;
        }
    }
}