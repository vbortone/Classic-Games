using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Core;
using Xunit;

namespace TicTacToe.Tests;

public class IBotStrategyTests
{
    [Fact]
    public void HeuristicBot_ImplementsIBotStrategy()
    {
        // Arrange & Act
        IBotStrategy bot = new HeuristicBot();
        
        // Assert
        Assert.NotNull(bot);
        Assert.IsAssignableFrom<IBotStrategy>(bot);
    }

    [Fact]
    public void ChooseMove_ReturnsValidMove()
    {
        // Arrange
        IBotStrategy bot = new HeuristicBot();
        var board = new Board();
        
        // Act
        var move = bot.ChooseMove(board);
        
        // Assert
        Assert.NotNull(move);
        Assert.True(move.Row >= 0 && move.Row < 3);
        Assert.True(move.Col >= 0 && move.Col < 3);
        Assert.Equal(Cell.X, move.Player);
    }

    [Fact]
    public void ChooseMove_EmptyBoard_ReturnsLegalMove()
    {
        // Arrange
        IBotStrategy bot = new HeuristicBot();
        var board = new Board();
        
        // Act
        var move = bot.ChooseMove(board);
        
        // Assert
        Assert.Equal(Cell.Empty, board[move.Row, move.Col]);
        Assert.Equal(Cell.X, move.Player);
    }

    [Fact]
    public void ChooseMove_PartiallyFilledBoard_ReturnsLegalMove()
    {
        // Arrange
        IBotStrategy bot = new HeuristicBot();
        var board = new Board();
        board = board.Apply(new Move(0, 0, Cell.X));
        board = board.Apply(new Move(1, 1, Cell.O));
        
        // Act
        var move = bot.ChooseMove(board);
        
        // Assert
        Assert.Equal(Cell.Empty, board[move.Row, move.Col]);
        Assert.Equal(Cell.X, move.Player);
    }

    [Fact]
    public void ChooseMove_AlmostFullBoard_ReturnsLegalMove()
    {
        // Arrange
        IBotStrategy bot = new HeuristicBot();
        var board = new Board();
        
        // Fill most of the board
        board = board.Apply(new Move(0, 0, Cell.X));
        board = board.Apply(new Move(0, 1, Cell.O));
        board = board.Apply(new Move(0, 2, Cell.X));
        board = board.Apply(new Move(1, 0, Cell.O));
        board = board.Apply(new Move(1, 1, Cell.X));
        board = board.Apply(new Move(1, 2, Cell.O));
        board = board.Apply(new Move(2, 0, Cell.X));
        board = board.Apply(new Move(2, 1, Cell.O));
        // Only (2, 2) is empty
        
        // Act
        var move = bot.ChooseMove(board);
        
        // Assert
        Assert.Equal(Cell.Empty, board[move.Row, move.Col]);
        Assert.Equal(Cell.X, move.Player);
    }

    [Fact]
    public void ChooseMove_AsOPlayer_ReturnsCorrectPlayer()
    {
        // Arrange
        IBotStrategy bot = new HeuristicBot();
        var board = new Board();
        board = board.Apply(new Move(0, 0, Cell.X)); // X moves first
        
        // Act
        var move = bot.ChooseMove(board);
        
        // Assert
        Assert.Equal(Cell.O, move.Player);
        Assert.Equal(Cell.Empty, board[move.Row, move.Col]);
    }

    [Fact]
    public void ChooseMove_ConsistentBehavior_SameBoardSameMove()
    {
        // Arrange
        IBotStrategy bot = new HeuristicBot();
        var board1 = new Board();
        var board2 = new Board();
        
        // Apply same moves to both boards
        board1 = board1.Apply(new Move(0, 0, Cell.X));
        board1 = board1.Apply(new Move(1, 1, Cell.O));
        board2 = board2.Apply(new Move(0, 0, Cell.X));
        board2 = board2.Apply(new Move(1, 1, Cell.O));
        
        // Act
        var move1 = bot.ChooseMove(board1);
        var move2 = bot.ChooseMove(board2);
        
        // Assert
        Assert.Equal(move1.Row, move2.Row);
        Assert.Equal(move1.Col, move2.Col);
        Assert.Equal(move1.Player, move2.Player);
    }

    [Fact]
    public void ChooseMove_EdgeCase_SingleEmptyCell()
    {
        // Arrange
        IBotStrategy bot = new HeuristicBot();
        var board = new Board();
        
        // Fill all but one cell
        board = board.Apply(new Move(0, 0, Cell.X));
        board = board.Apply(new Move(0, 1, Cell.O));
        board = board.Apply(new Move(0, 2, Cell.X));
        board = board.Apply(new Move(1, 0, Cell.O));
        board = board.Apply(new Move(1, 1, Cell.X));
        board = board.Apply(new Move(1, 2, Cell.O));
        board = board.Apply(new Move(2, 0, Cell.X));
        board = board.Apply(new Move(2, 1, Cell.O));
        // Only (2, 2) is empty
        
        // Act
        var move = bot.ChooseMove(board);
        
        // Assert
        Assert.Equal(2, move.Row);
        Assert.Equal(2, move.Col);
        Assert.Equal(Cell.X, move.Player);
    }

    [Fact]
    public void ChooseMove_InterfaceContract_AlwaysReturnsMove()
    {
        // Arrange
        IBotStrategy bot = new HeuristicBot();
        var board = new Board();
        
        // Act & Assert - should not throw
        var move = bot.ChooseMove(board);
        Assert.NotNull(move);
    }

    [Fact]
    public void ChooseMove_ValidatesMove_WithinBounds()
    {
        // Arrange
        IBotStrategy bot = new HeuristicBot();
        var board = new Board();
        
        // Act
        var move = bot.ChooseMove(board);
        
        // Assert
        Assert.True(move.Row >= 0 && move.Row < 3, "Row should be within bounds");
        Assert.True(move.Col >= 0 && move.Col < 3, "Column should be within bounds");
    }
}
