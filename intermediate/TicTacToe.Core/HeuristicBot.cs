namespace TicTacToe.Core;

public class HeuristicBot : IBotStrategy
{
    // PROMPT: Ask Cursor to implement a simple heuristic:
    // 1) If winning move exists, take it.
    // 2) If opponent can win next, block it.
    // 3) Prefer center, then corners, then edges.
    public Move ChooseMove(Board board)
    {
        var currentPlayer = board.CurrentPlayer;
        var opponent = currentPlayer == Cell.X ? Cell.O : Cell.X;
        
        // 1. If a winning move exists, take it
        var winningMove = FindWinningMove(board, currentPlayer);
        if (winningMove.HasValue)
        {
            return new Move(winningMove.Value.r, winningMove.Value.c, currentPlayer);
        }
        
        // 2. If opponent can win next, block it
        var blockingMove = FindWinningMove(board, opponent);
        if (blockingMove.HasValue)
        {
            return new Move(blockingMove.Value.r, blockingMove.Value.c, currentPlayer);
        }
        
        // 3. Prefer center, then corners, then edges
        var emptyCells = board.GetEmptyCells().ToList();
        
        // Try center first
        var center = emptyCells.FirstOrDefault(cell => cell.r == 1 && cell.c == 1);
        if (center != default)
        {
            return new Move(center.r, center.c, currentPlayer);
        }
        
        // Try corners next
        var corners = emptyCells.Where(cell => 
            (cell.r == 0 && cell.c == 0) || // top-left
            (cell.r == 0 && cell.c == 2) || // top-right
            (cell.r == 2 && cell.c == 0) || // bottom-left
            (cell.r == 2 && cell.c == 2)).ToList(); // bottom-right
        
        if (corners.Any())
        {
            var corner = corners.First();
            return new Move(corner.r, corner.c, currentPlayer);
        }
        
        // Finally, take any remaining edge
        var edges = emptyCells.Where(cell => 
            (cell.r == 0 && cell.c == 1) || // top edge
            (cell.r == 1 && cell.c == 0) || // left edge
            (cell.r == 1 && cell.c == 2) || // right edge
            (cell.r == 2 && cell.c == 1)).ToList(); // bottom edge
        
        if (edges.Any())
        {
            var edge = edges.First();
            return new Move(edge.r, edge.c, currentPlayer);
        }
        
        // Fallback: take any available move
        var anyMove = emptyCells.First();
        return new Move(anyMove.r, anyMove.c, currentPlayer);
    }
    
    private (int r, int c)? FindWinningMove(Board board, Cell player)
    {
        var emptyCells = board.GetEmptyCells();
        
        foreach (var (r, c) in emptyCells)
        {
            // Simulate the move without using board.Apply to avoid player validation
            if (WouldResultInWin(board, r, c, player))
            {
                return (r, c);
            }
        }
        
        return null;
    }
    
    private bool WouldResultInWin(Board board, int row, int col, Cell player)
    {
        // Check if placing 'player' at (row, col) would result in a win
        
        // Check row
        bool rowWin = true;
        for (int c = 0; c < 3; c++)
        {
            var cell = c == col ? player : board[row, c];
            if (cell != player)
            {
                rowWin = false;
                break;
            }
        }
        if (rowWin) return true;
        
        // Check column
        bool colWin = true;
        for (int r = 0; r < 3; r++)
        {
            var cell = r == row ? player : board[r, col];
            if (cell != player)
            {
                colWin = false;
                break;
            }
        }
        if (colWin) return true;
        
        // Check main diagonal (if the move is on the diagonal)
        if (row == col)
        {
            bool diagWin = true;
            for (int i = 0; i < 3; i++)
            {
                var cell = i == row ? player : board[i, i];
                if (cell != player)
                {
                    diagWin = false;
                    break;
                }
            }
            if (diagWin) return true;
        }
        
        // Check anti-diagonal (if the move is on the anti-diagonal)
        if (row + col == 2)
        {
            bool antiDiagWin = true;
            for (int i = 0; i < 3; i++)
            {
                var cell = i == row ? player : board[i, 2 - i];
                if (cell != player)
                {
                    antiDiagWin = false;
                    break;
                }
            }
            if (antiDiagWin) return true;
        }
        
        return false;
    }
}
