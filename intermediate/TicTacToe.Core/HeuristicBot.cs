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
            // Create a temporary move to test if it would result in a win
            var testMove = new Move(r, c, player);
            var testBoard = board.Apply(testMove);
            
            if (testBoard.GetStatus() == (player == Cell.X ? GameStatus.XWins : GameStatus.OWins))
            {
                return (r, c);
            }
        }
        
        return null;
    }
}
