public class MiniBoard : Board
{
    public bool MakeMove(int x, int y, int currentPlayer)
    {
        if (IsFinished || board[x, y] != 0)
        {
            return false;
        }
        board[x, y] = currentPlayer;
        UpdateWinnerStatus();
        return true;
    }

    public void Reset()
    {
        board = new int[3, 3];
        Winner = 0;
    }
}