using TicTacToe.Core;
using Xunit;

namespace TicTacToe.Tests;

public class BoardTests
{
    [Fact]
    public void EmptyBoard_IsInProgress()
    {
        var b = new Board();
        Assert.Equal(GameStatus.InProgress, b.GetStatus());
    }

    [Fact]
    public void XCompletesRow_Wins()
    {
        // Arrange a sequence where X wins on top row.
        var b = new Board();
        
        // X moves to (0,0)
        b = b.Apply(new Move(0, 0, Cell.X));
        Assert.Equal(GameStatus.InProgress, b.GetStatus());
        Assert.Equal(Cell.O, b.CurrentPlayer);
        
        // O moves to (1,1)
        b = b.Apply(new Move(1, 1, Cell.O));
        Assert.Equal(GameStatus.InProgress, b.GetStatus());
        Assert.Equal(Cell.X, b.CurrentPlayer);
        
        // X moves to (0,1)
        b = b.Apply(new Move(0, 1, Cell.X));
        Assert.Equal(GameStatus.InProgress, b.GetStatus());
        Assert.Equal(Cell.O, b.CurrentPlayer);
        
        // O moves to (2,2)
        b = b.Apply(new Move(2, 2, Cell.O));
        Assert.Equal(GameStatus.InProgress, b.GetStatus());
        Assert.Equal(Cell.X, b.CurrentPlayer);
        
        // X moves to (0,2) - completing the top row and winning
        b = b.Apply(new Move(0, 2, Cell.X));
        Assert.Equal(GameStatus.XWins, b.GetStatus());
    }
}
