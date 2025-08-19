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
        throw new NotImplementedException();
    }

    // PROMPT: Ask Cursor to implement GetEmptyCells() helper returning IEnumerable<(int r,int c)>.
}
