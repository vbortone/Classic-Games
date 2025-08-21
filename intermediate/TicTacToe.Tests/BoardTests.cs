using System;
using System.Collections.Generic;
using System.Linq;
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

    [Theory]
    [InlineData(0)] // Top row
    [InlineData(1)] // Middle row
    [InlineData(2)] // Bottom row
    public void RowWins_AllRows(int row)
    {
        var b = new Board();
        
        // X takes the entire row
        b = b.Apply(new Move(row, 0, Cell.X));
        b = b.Apply(new Move((row + 1) % 3, 0, Cell.O)); // O moves elsewhere
        b = b.Apply(new Move(row, 1, Cell.X));
        b = b.Apply(new Move((row + 2) % 3, 1, Cell.O)); // O moves elsewhere
        b = b.Apply(new Move(row, 2, Cell.X));
        
        Assert.Equal(GameStatus.XWins, b.GetStatus());
    }

    [Theory]
    [InlineData(0)] // Left column
    [InlineData(1)] // Middle column
    [InlineData(2)] // Right column
    public void ColumnWins_AllColumns(int col)
    {
        var b = new Board();
        
        // X takes the entire column
        b = b.Apply(new Move(0, col, Cell.X));
        b = b.Apply(new Move(0, (col + 1) % 3, Cell.O)); // O moves elsewhere
        b = b.Apply(new Move(1, col, Cell.X));
        b = b.Apply(new Move(1, (col + 2) % 3, Cell.O)); // O moves elsewhere
        b = b.Apply(new Move(2, col, Cell.X));
        
        Assert.Equal(GameStatus.XWins, b.GetStatus());
    }

    [Fact]
    public void DiagonalWins_MainDiagonal()
    {
        var b = new Board();
        
        // X takes main diagonal (top-left to bottom-right)
        b = b.Apply(new Move(0, 0, Cell.X));
        b = b.Apply(new Move(0, 1, Cell.O)); // O moves elsewhere
        b = b.Apply(new Move(1, 1, Cell.X));
        b = b.Apply(new Move(0, 2, Cell.O)); // O moves elsewhere
        b = b.Apply(new Move(2, 2, Cell.X));
        
        Assert.Equal(GameStatus.XWins, b.GetStatus());
    }

    [Fact]
    public void DiagonalWins_AntiDiagonal()
    {
        var b = new Board();
        
        // X takes anti-diagonal (top-right to bottom-left)
        b = b.Apply(new Move(0, 2, Cell.X));
        b = b.Apply(new Move(0, 0, Cell.O)); // O moves elsewhere
        b = b.Apply(new Move(1, 1, Cell.X));
        b = b.Apply(new Move(2, 2, Cell.O)); // O moves elsewhere
        b = b.Apply(new Move(2, 0, Cell.X));
        
        Assert.Equal(GameStatus.XWins, b.GetStatus());
    }

    [Fact]
    public void OWins_WithRow()
    {
        var b = new Board();
        
        // X moves first, then O wins
        b = b.Apply(new Move(0, 0, Cell.X));
        b = b.Apply(new Move(1, 0, Cell.O));
        b = b.Apply(new Move(0, 1, Cell.X));
        b = b.Apply(new Move(1, 1, Cell.O));
        b = b.Apply(new Move(2, 2, Cell.X));
        b = b.Apply(new Move(1, 2, Cell.O)); // O completes middle row
        
        Assert.Equal(GameStatus.OWins, b.GetStatus());
    }

    [Fact]
    public void DrawScenario_NoWinner()
    {
        var b = new Board();
        
        // Classic draw pattern (no winner)
        b = b.Apply(new Move(0, 0, Cell.X)); // X
        b = b.Apply(new Move(0, 1, Cell.O)); // O
        b = b.Apply(new Move(0, 2, Cell.X)); // X
        b = b.Apply(new Move(1, 0, Cell.O)); // O
        b = b.Apply(new Move(1, 2, Cell.X)); // X
        b = b.Apply(new Move(1, 1, Cell.O)); // O
        b = b.Apply(new Move(2, 0, Cell.X)); // X
        b = b.Apply(new Move(2, 2, Cell.O)); // O
        b = b.Apply(new Move(2, 1, Cell.X)); // X
        
        Assert.Equal(GameStatus.Draw, b.GetStatus());
    }

    [Fact]
    public void DrawScenario_AlternatePattern()
    {
        var b = new Board();
        
        // Another draw pattern
        b = b.Apply(new Move(0, 0, Cell.X)); // X
        b = b.Apply(new Move(1, 1, Cell.O)); // O
        b = b.Apply(new Move(0, 1, Cell.X)); // X
        b = b.Apply(new Move(0, 2, Cell.O)); // O
        b = b.Apply(new Move(2, 0, Cell.X)); // X
        b = b.Apply(new Move(1, 0, Cell.O)); // O
        b = b.Apply(new Move(1, 2, Cell.X)); // X
        b = b.Apply(new Move(2, 1, Cell.O)); // O
        b = b.Apply(new Move(2, 2, Cell.X)); // X
        
        Assert.Equal(GameStatus.Draw, b.GetStatus());
    }

    [Theory]
    [InlineData(-1, 0, "Move position is out of bounds")]
    [InlineData(0, -1, "Move position is out of bounds")]
    [InlineData(3, 0, "Move position is out of bounds")]
    [InlineData(0, 3, "Move position is out of bounds")]
    public void InvalidMove_OutOfBounds_ThrowsException(int row, int col, string expectedMessage)
    {
        var b = new Board();
        var invalidMove = new Move(row, col, Cell.X);
        
        var exception = Assert.Throws<ArgumentException>(() => b.Apply(invalidMove));
        Assert.Contains(expectedMessage, exception.Message);
    }

    [Fact]
    public void InvalidMove_OccupiedCell_ThrowsException()
    {
        var b = new Board();
        
        // Make a valid move first
        b = b.Apply(new Move(0, 0, Cell.X));
        
        // Try to move to the same cell
        var invalidMove = new Move(0, 0, Cell.O);
        
        var exception = Assert.Throws<ArgumentException>(() => b.Apply(invalidMove));
        Assert.Contains("Cell is already occupied", exception.Message);
    }

    [Fact]
    public void InvalidMove_WrongPlayer_ThrowsException()
    {
        var b = new Board();
        
        // X should move first, but O tries to move
        var invalidMove = new Move(0, 0, Cell.O);
        
        var exception = Assert.Throws<ArgumentException>(() => b.Apply(invalidMove));
        Assert.Contains("Move player does not match current player", exception.Message);
    }

    [Fact]
    public void GetEmptyCells_EmptyBoard_ReturnsAllCells()
    {
        var b = new Board();
        var emptyCells = b.GetEmptyCells().ToList();
        
        Assert.Equal(9, emptyCells.Count);
        
        // Verify all positions are present
        var expectedPositions = new List<(int, int)>
        {
            (0, 0), (0, 1), (0, 2),
            (1, 0), (1, 1), (1, 2),
            (2, 0), (2, 1), (2, 2)
        };
        
        foreach (var expected in expectedPositions)
        {
            Assert.Contains(expected, emptyCells);
        }
    }

    [Fact]
    public void GetEmptyCells_PartiallyFilledBoard_ReturnsCorrectCells()
    {
        var b = new Board();
        b = b.Apply(new Move(0, 0, Cell.X));
        b = b.Apply(new Move(1, 1, Cell.O));
        
        var emptyCells = b.GetEmptyCells().ToList();
        
        Assert.Equal(7, emptyCells.Count);
        Assert.DoesNotContain((0, 0), emptyCells);
        Assert.DoesNotContain((1, 1), emptyCells);
        Assert.Contains((0, 1), emptyCells);
        Assert.Contains((0, 2), emptyCells);
        Assert.Contains((1, 0), emptyCells);
        Assert.Contains((1, 2), emptyCells);
        Assert.Contains((2, 0), emptyCells);
        Assert.Contains((2, 1), emptyCells);
        Assert.Contains((2, 2), emptyCells);
    }

    [Fact]
    public void GetEmptyCells_FullBoard_ReturnsEmpty()
    {
        var b = new Board();
        
        // Fill the board
        b = b.Apply(new Move(0, 0, Cell.X));
        b = b.Apply(new Move(0, 1, Cell.O));
        b = b.Apply(new Move(0, 2, Cell.X));
        b = b.Apply(new Move(1, 0, Cell.O));
        b = b.Apply(new Move(1, 1, Cell.X));
        b = b.Apply(new Move(1, 2, Cell.O));
        b = b.Apply(new Move(2, 0, Cell.X));
        b = b.Apply(new Move(2, 1, Cell.O));
        b = b.Apply(new Move(2, 2, Cell.X));
        
        var emptyCells = b.GetEmptyCells().ToList();
        Assert.Empty(emptyCells);
    }

    [Fact]
    public void Board_Indexer_ReturnsCorrectValues()
    {
        var b = new Board();
        
        // Initially all cells should be empty
        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                Assert.Equal(Cell.Empty, b[r, c]);
            }
        }
        
        // After a move, the cell should contain the player
        b = b.Apply(new Move(1, 1, Cell.X));
        Assert.Equal(Cell.X, b[1, 1]);
        Assert.Equal(Cell.Empty, b[0, 0]);
    }

    [Fact]
    public void Board_Immutable_OriginalBoardUnchanged()
    {
        var originalBoard = new Board();
        var modifiedBoard = originalBoard.Apply(new Move(0, 0, Cell.X));
        
        // Original board should still be empty
        Assert.Equal(Cell.Empty, originalBoard[0, 0]);
        Assert.Equal(Cell.X, originalBoard.CurrentPlayer);
        
        // Modified board should have the move
        Assert.Equal(Cell.X, modifiedBoard[0, 0]);
        Assert.Equal(Cell.O, modifiedBoard.CurrentPlayer);
    }

    [Fact]
    public void CurrentPlayer_AlternatesCorrectly()
    {
        var b = new Board();
        
        Assert.Equal(Cell.X, b.CurrentPlayer);
        
        b = b.Apply(new Move(0, 0, Cell.X));
        Assert.Equal(Cell.O, b.CurrentPlayer);
        
        b = b.Apply(new Move(0, 1, Cell.O));
        Assert.Equal(Cell.X, b.CurrentPlayer);
        
        b = b.Apply(new Move(1, 0, Cell.X));
        Assert.Equal(Cell.O, b.CurrentPlayer);
    }
}
