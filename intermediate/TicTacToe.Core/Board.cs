namespace TicTacToe.Core;

public enum Cell { Empty, X, O }
public enum GameStatus { InProgress, XWins, OWins, Draw }

public record Move(int Row, int Col, Cell Player);

public class Board
{
    private readonly Cell[,] _cells = new Cell[3,3];
    public Cell this[int r, int c] => _cells[r,c];
    public Cell CurrentPlayer { get; private set; } = Cell.X;

    // PROMPT: Ask Cursor to implement immutable Apply(Move) that returns a NEW Board with the move applied,
    // throws on invalid move, and flips CurrentPlayer.
    public Board Apply(Move move)
    {
        // Validate the move
        if (move.Row < 0 || move.Row >= 3 || move.Col < 0 || move.Col >= 3)
        {
            throw new ArgumentException("Move position is out of bounds", nameof(move));
        }

        if (_cells[move.Row, move.Col] != Cell.Empty)
        {
            throw new ArgumentException("Cell is already occupied", nameof(move));
        }

        if (move.Player != CurrentPlayer)
        {
            throw new ArgumentException("Move player does not match current player", nameof(move));
        }

        // Create a new board with the move applied
        var newBoard = new Board();
        
        // Copy the current state
        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                newBoard._cells[r, c] = _cells[r, c];
            }
        }
        
        // Apply the move
        newBoard._cells[move.Row, move.Col] = move.Player;
        
        // Flip the current player
        newBoard.CurrentPlayer = CurrentPlayer == Cell.X ? Cell.O : Cell.X;
        
        return newBoard;
    }

    // PROMPT: Ask Cursor to implement GetStatus() that checks rows, cols, diags, and draw.
    public GameStatus GetStatus()
    {
        // Check rows for win
        for (int r = 0; r < 3; r++)
        {
            if (_cells[r, 0] != Cell.Empty && 
                _cells[r, 0] == _cells[r, 1] && 
                _cells[r, 1] == _cells[r, 2])
            {
                return _cells[r, 0] == Cell.X ? GameStatus.XWins : GameStatus.OWins;
            }
        }

        // Check columns for win
        for (int c = 0; c < 3; c++)
        {
            if (_cells[0, c] != Cell.Empty && 
                _cells[0, c] == _cells[1, c] && 
                _cells[1, c] == _cells[2, c])
            {
                return _cells[0, c] == Cell.X ? GameStatus.XWins : GameStatus.OWins;
            }
        }

        // Check main diagonal (top-left to bottom-right)
        if (_cells[0, 0] != Cell.Empty && 
            _cells[0, 0] == _cells[1, 1] && 
            _cells[1, 1] == _cells[2, 2])
        {
            return _cells[0, 0] == Cell.X ? GameStatus.XWins : GameStatus.OWins;
        }

        // Check anti-diagonal (top-right to bottom-left)
        if (_cells[0, 2] != Cell.Empty && 
            _cells[0, 2] == _cells[1, 1] && 
            _cells[1, 1] == _cells[2, 0])
        {
            return _cells[0, 2] == Cell.X ? GameStatus.XWins : GameStatus.OWins;
        }

        // Check for draw (all cells filled)
        bool isDraw = true;
        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                if (_cells[r, c] == Cell.Empty)
                {
                    isDraw = false;
                    break;
                }
            }
            if (!isDraw) break;
        }

        return isDraw ? GameStatus.Draw : GameStatus.InProgress;
    }

    // PROMPT: Ask Cursor to implement GetEmptyCells() helper returning IEnumerable<(int r,int c)>.
}
