using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Core;
using Xunit;

namespace TicTacToe.Tests;

public class HeuristicBotTests
{
    private readonly HeuristicBot _bot = new();

    [Fact]
    public void ChooseMove_EmptyBoard_PrefersCenter()
    {
        var board = new Board();
        var move = _bot.ChooseMove(board);
        
        Assert.Equal(1, move.Row);
        Assert.Equal(1, move.Col);
        Assert.Equal(Cell.X, move.Player);
    }

    [Fact]
    public void ChooseMove_WinningMoveAvailable_TakesWinningMove()
    {
        var board = new Board();
        
        // Set up a board where X can win in one move
        board = board.Apply(new Move(0, 0, Cell.X));
        board = board.Apply(new Move(1, 0, Cell.O));
        board = board.Apply(new Move(0, 1, Cell.X));
        board = board.Apply(new Move(1, 1, Cell.O));
        // X can win by placing at (0, 2)
        
        var move = _bot.ChooseMove(board);
        
        Assert.Equal(0, move.Row);
        Assert.Equal(2, move.Col);
        Assert.Equal(Cell.X, move.Player);
    }

    [Fact]
    public void ChooseMove_OpponentCanWin_BlocksOpponent()
    {
        var board = new Board();
        
        // Set up a board where O can win in one move
        board = board.Apply(new Move(0, 0, Cell.X));
        board = board.Apply(new Move(1, 0, Cell.O));
        board = board.Apply(new Move(2, 2, Cell.X));
        board = board.Apply(new Move(1, 1, Cell.O));
        // O can win by placing at (1, 2), so X should block there
        
        var move = _bot.ChooseMove(board);
        
        Assert.Equal(1, move.Row);
        Assert.Equal(2, move.Col);
        Assert.Equal(Cell.X, move.Player);
    }

    [Fact]
    public void ChooseMove_NoWinningMove_CenterAvailable_PrefersCenter()
    {
        var board = new Board();
        board = board.Apply(new Move(0, 0, Cell.X));
        board = board.Apply(new Move(0, 1, Cell.O));
        
        var move = _bot.ChooseMove(board);
        
        Assert.Equal(1, move.Row);
        Assert.Equal(1, move.Col);
        Assert.Equal(Cell.X, move.Player);
    }

    [Fact]
    public void ChooseMove_CenterOccupied_PrefersCorner()
    {
        var board = new Board();
        board = board.Apply(new Move(0, 0, Cell.X)); // X takes corner
        board = board.Apply(new Move(1, 1, Cell.O)); // O takes center
        
        var move = _bot.ChooseMove(board);
        
        // Should choose a corner
        bool isCorner = (move.Row == 0 && move.Col == 2) ||
                       (move.Row == 2 && move.Col == 0) ||
                       (move.Row == 2 && move.Col == 2);
        
        Assert.True(isCorner, $"Expected corner move, got ({move.Row}, {move.Col})");
        Assert.Equal(Cell.X, move.Player);
    }

    [Fact]
    public void ChooseMove_CenterAndCornersOccupied_PrefersEdge()
    {
        var board = new Board();
        board = board.Apply(new Move(0, 0, Cell.X)); // X takes corner
        board = board.Apply(new Move(1, 1, Cell.O)); // O takes center
        board = board.Apply(new Move(0, 2, Cell.X)); // X takes corner
        board = board.Apply(new Move(2, 0, Cell.O)); // O takes corner
        board = board.Apply(new Move(2, 2, Cell.X)); // X takes corner
        
        var move = _bot.ChooseMove(board);
        
        // Should choose an edge
        bool isEdge = (move.Row == 0 && move.Col == 1) ||
                     (move.Row == 1 && move.Col == 0) ||
                     (move.Row == 1 && move.Col == 2) ||
                     (move.Row == 2 && move.Col == 1);
        
        Assert.True(isEdge, $"Expected edge move, got ({move.Row}, {move.Col})");
        Assert.Equal(Cell.O, move.Player);
    }

    [Fact]
    public void ChooseMove_AlmostFullBoard_TakesAnyAvailable()
    {
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
        
        var move = _bot.ChooseMove(board);
        
        Assert.Equal(2, move.Row);
        Assert.Equal(2, move.Col);
        Assert.Equal(Cell.X, move.Player);
    }

    [Fact]
    public void ChooseMove_MultipleWinningMoves_ChoosesFirstFound()
    {
        var board = new Board();
        
        // Set up a board where X can win in multiple ways
        board = board.Apply(new Move(0, 0, Cell.X));
        board = board.Apply(new Move(1, 0, Cell.O));
        board = board.Apply(new Move(0, 1, Cell.X));
        board = board.Apply(new Move(1, 1, Cell.O));
        board = board.Apply(new Move(1, 2, Cell.X));
        // X can win by placing at (0, 2) or (2, 2)
        
        var move = _bot.ChooseMove(board);
        
        // Should choose one of the winning moves
        bool isWinningMove = (move.Row == 0 && move.Col == 2) ||
                            (move.Row == 2 && move.Col == 2);
        
        Assert.True(isWinningMove, $"Expected winning move, got ({move.Row}, {move.Col})");
        Assert.Equal(Cell.O, move.Player);
    }

    [Fact]
    public void ChooseMove_OpponentWinningMoveOnDiagonal_BlocksCorrectly()
    {
        var board = new Board();
        
        // Set up a diagonal threat for O
        board = board.Apply(new Move(0, 1, Cell.X));
        board = board.Apply(new Move(0, 0, Cell.O));
        board = board.Apply(new Move(1, 0, Cell.X));
        board = board.Apply(new Move(1, 1, Cell.O));
        // O can win by placing at (2, 2)
        
        var move = _bot.ChooseMove(board);
        
        Assert.Equal(2, move.Row);
        Assert.Equal(2, move.Col);
        Assert.Equal(Cell.X, move.Player);
    }

    [Fact]
    public void ChooseMove_OpponentWinningMoveOnAntiDiagonal_BlocksCorrectly()
    {
        var board = new Board();
        
        // Set up an anti-diagonal threat for O
        board = board.Apply(new Move(0, 1, Cell.X));
        board = board.Apply(new Move(0, 2, Cell.O));
        board = board.Apply(new Move(1, 0, Cell.X));
        board = board.Apply(new Move(1, 1, Cell.O));
        // O can win by placing at (2, 0)
        
        var move = _bot.ChooseMove(board);
        
        Assert.Equal(2, move.Row);
        Assert.Equal(0, move.Col);
        Assert.Equal(Cell.X, move.Player);
    }

    [Fact]
    public void ChooseMove_NoWinningOrBlockingMoves_PrefersCenterThenCornersThenEdges()
    {
        var board = new Board();
        board = board.Apply(new Move(0, 0, Cell.X));
        board = board.Apply(new Move(0, 2, Cell.O));
        
        var move = _bot.ChooseMove(board);
        
        // Should prefer center over remaining corners
        Assert.Equal(1, move.Row);
        Assert.Equal(1, move.Col);
        Assert.Equal(Cell.X, move.Player);
    }

    [Fact]
    public void ChooseMove_AsOPlayer_WorksCorrectly()
    {
        var board = new Board();
        board = board.Apply(new Move(0, 0, Cell.X)); // X moves first
        
        var move = _bot.ChooseMove(board);
        
        Assert.Equal(Cell.O, move.Player);
        // Should still prefer center
        Assert.Equal(1, move.Row);
        Assert.Equal(1, move.Col);
    }

    [Fact]
    public void ChooseMove_ComplexScenario_HandlesCorrectly()
    {
        var board = new Board();
        
        // Create a complex scenario
        board = board.Apply(new Move(0, 0, Cell.X));
        board = board.Apply(new Move(1, 1, Cell.O));
        board = board.Apply(new Move(0, 1, Cell.X));
        board = board.Apply(new Move(2, 0, Cell.O));
        board = board.Apply(new Move(0, 2, Cell.X));
        board = board.Apply(new Move(1, 0, Cell.O));
        board = board.Apply(new Move(1, 2, Cell.X));
        board = board.Apply(new Move(2, 1, Cell.O));
        // Only (2, 2) is empty
        
        var move = _bot.ChooseMove(board);
        
        Assert.Equal(2, move.Row);
        Assert.Equal(2, move.Col);
        Assert.Equal(Cell.X, move.Player);
    }

    [Fact]
    public void ChooseMove_EdgeCase_AllCornersOccupied()
    {
        var board = new Board();
        board = board.Apply(new Move(0, 0, Cell.X));
        board = board.Apply(new Move(0, 2, Cell.O));
        board = board.Apply(new Move(2, 0, Cell.X));
        board = board.Apply(new Move(2, 2, Cell.O));
        
        var move = _bot.ChooseMove(board);
        
        // Should choose an edge since center and corners are taken
        bool isEdge = (move.Row == 0 && move.Col == 1) ||
                     (move.Row == 1 && move.Col == 0) ||
                     (move.Row == 1 && move.Col == 2) ||
                     (move.Row == 2 && move.Col == 1);
        
        Assert.True(isEdge, $"Expected edge move, got ({move.Row}, {move.Col})");
        Assert.Equal(Cell.X, move.Player);
    }
}
