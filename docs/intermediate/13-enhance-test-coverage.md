# Enhance Test Coverage based on QA Review

I'll help you enhance the test cases for the intermediate TicTacToe project. Let me first examine the current test structure and then add comprehensive test cases based on the QA review document.

Read file: intermediate/TicTacToe.Tests/BoardTests.cs
Read file: intermediate/TicTacToe.Core/Board.cs
Read file: intermediate/TicTacToe.Core/HeuristicBot.cs
Read file: intermediate/TicTacToe.Core/IBotStrategy.cs
Now I'll enhance the test cases based on the QA review document requirements. I'll add comprehensive tests for row wins, column wins, diagonal wins, draw scenarios, invalid move scenarios, and bot strategy edge cases.

Now I'll create a separate test file for the HeuristicBot to test bot strategy edge cases:

Now let's run the tests to make sure they all pass:

```bash
cd intermediate && dotnet test
```
```bash
dotnet test TicTacToe.Tests/TicTacToe.Tests.csproj
```
I need to add the missing using statements for the test files. Let me fix the compilation errors:

Now let's run the tests again:

```bash
dotnet test TicTacToe.Tests/TicTacToe.Tests.csproj
```
I need to fix the failing tests. Let me analyze and correct the issues:

1. The draw scenario test is failing because the pattern I used actually results in an X win
2. The bot tests are failing because I need to account for the current player correctly

Let me fix these issues:

Now let me fix the bot tests by ensuring the current player is correct:

Now let's run the tests again to see if they pass:

```bash
dotnet test TicTacToe.Tests/TicTacToe.Tests.csproj
```
Perfect! All tests are now passing. Let me also create a test file for the IBotStrategy interface to ensure proper interface testing:

Now let's run all the tests to make sure everything is working correctly:

```bash
dotnet test TicTacToe.Tests/TicTacToe.Tests.csproj --verbosity normal
```
```bash
dotnet test TicTacToe.Tests/TicTacToe.Tests.csproj
```
Excellent! All 49 tests are now passing. Let me create a summary of what I've accomplished:

## Enhanced Test Cases Summary

I have successfully enhanced the test cases for the intermediate TicTacToe project with comprehensive coverage as specified in the QA review document. Here's what was implemented:

### 📋 **BoardTests.cs** - Enhanced with 25 test methods:

**Row Wins:**
- `RowWins_AllRows()` - Tests all three rows (0, 1, 2) using Theory with InlineData

**Column Wins:**
- `ColumnWins_AllColumns()` - Tests all three columns (0, 1, 2) using Theory with InlineData

**Diagonal Wins:**
- `DiagonalWins_MainDiagonal()` - Tests main diagonal (top-left to bottom-right)
- `DiagonalWins_AntiDiagonal()` - Tests anti-diagonal (top-right to bottom-left)

**Draw Scenarios:**
- `DrawScenario_NoWinner()` - Classic draw pattern
- `DrawScenario_AlternatePattern()` - Alternative draw pattern

**Invalid Move Scenarios:**
- `InvalidMove_OutOfBounds_ThrowsException()` - Tests all boundary violations using Theory
- `InvalidMove_OccupiedCell_ThrowsException()` - Tests moving to occupied cells
- `InvalidMove_WrongPlayer_ThrowsException()` - Tests wrong player validation

**Edge Cases & Helper Methods:**
- `GetEmptyCells_EmptyBoard_ReturnsAllCells()` - Tests empty board scenario
- `GetEmptyCells_PartiallyFilledBoard_ReturnsCorrectCells()` - Tests partially filled board
- `GetEmptyCells_FullBoard_ReturnsEmpty()` - Tests full board scenario
- `Board_Indexer_ReturnsCorrectValues()` - Tests board indexer functionality
- `Board_Immutable_OriginalBoardUnchanged()` - Tests immutability
- `CurrentPlayer_AlternatesCorrectly()` - Tests player alternation

### 🤖 **HeuristicBotTests.cs** - New file with 14 test methods:

**Bot Strategy Edge Cases:**
- `ChooseMove_EmptyBoard_PrefersCenter()` - Tests center preference
- `ChooseMove_WinningMoveAvailable_TakesWinningMove()` - Tests winning move detection
- `ChooseMove_OpponentCanWin_BlocksOpponent()` - Tests blocking opponent wins
- `ChooseMove_NoWinningMove_CenterAvailable_PrefersCenter()` - Tests center preference when available
- `ChooseMove_CenterOccupied_PrefersCorner()` - Tests corner preference when center taken
- `ChooseMove_CenterAndCornersOccupied_PrefersEdge()` - Tests edge preference
- `ChooseMove_AlmostFullBoard_TakesAnyAvailable()` - Tests end-game scenarios
- `ChooseMove_MultipleWinningMoves_ChoosesFirstFound()` - Tests multiple winning options
- `ChooseMove_OpponentWinningMoveOnDiagonal_BlocksCorrectly()` - Tests diagonal blocking
- `ChooseMove_OpponentWinningMoveOnAntiDiagonal_BlocksCorrectly()` - Tests anti-diagonal blocking
- `ChooseMove_NoWinningOrBlockingMoves_PrefersCenterThenCornersThenEdges()` - Tests preference hierarchy
- `ChooseMove_AsOPlayer_WorksCorrectly()` - Tests O player functionality
- `ChooseMove_ComplexScenario_HandlesCorrectly()` - Tests complex game scenarios
- `ChooseMove_EdgeCase_AllCornersOccupied()` - Tests edge case with all corners taken

### 🔧 **IBotStrategyTests.cs** - New file with 10 test methods:

**Interface Contract Testing:**
- `HeuristicBot_ImplementsIBotStrategy()` - Tests interface implementation
- `ChooseMove_ReturnsValidMove()` - Tests move validity
- `ChooseMove_EmptyBoard_ReturnsLegalMove()` - Tests legal moves on empty board
- `ChooseMove_PartiallyFilledBoard_ReturnsLegalMove()` - Tests legal moves on partial board
- `ChooseMove_AlmostFullBoard_ReturnsLegalMove()` - Tests legal moves near end-game
- `ChooseMove_AsOPlayer_ReturnsCorrectPlayer()` - Tests O player moves
- `ChooseMove_ConsistentBehavior_SameBoardSameMove()` - Tests deterministic behavior
- `ChooseMove_EdgeCase_SingleEmptyCell()` - Tests single empty cell scenario
- `ChooseMove_InterfaceContract_AlwaysReturnsMove()` - Tests interface contract
- `ChooseMove_ValidatesMove_WithinBounds()` - Tests move bounds validation

### ✅ **Test Results:**
- **Total Tests:** 49
- **Passed:** 49
- **Failed:** 0
- **Skipped:** 0

### 🎯 **Coverage Achieved:**
✅ Row wins (all 3 rows)  
✅ Column wins (all 3 columns)  
✅ Diagonal wins (main and anti-diagonal)  
✅ Draw scenarios (multiple patterns)  
✅ Invalid move scenarios (bounds, occupied, wrong player)  
✅ Bot strategy edge cases (winning moves, blocking, preferences)  
✅ Interface contract validation  
✅ Immutability testing  
✅ Player alternation testing  
✅ Helper method testing  

The enhanced test suite now provides comprehensive coverage for all the scenarios mentioned in the QA review document, ensuring robust validation of the TicTacToe game logic and bot strategy implementation.