# START HERE — Intermediate: Tic-Tac-Toe (Engine + CLI, .NET 8)

## Prerequisites
- **.NET 8 SDK** installed and verified with `dotnet --version`

## Project Setup
```bash
cd classic-games/intermediate
dotnet build ./TicTacToe.Core/TicTacToe.Core.csproj
dotnet build ./TicTacToe.Cli/TicTacToe.Cli.csproj
dotnet build ./TicTacToe.Tests/TicTacToe.Tests.csproj
# Tests will fail until you complete the prompts below
```

## Using Cursor
Follow the **Prompt Roadmap** below. Implement the engine first, then the bot, then wire up the CLI.

## Prompt Roadmap (do these in this order)
- 1. `TicTacToe.Core/Board.cs` line **14** — PROMPT: Ask Cursor to implement immutable Apply(Move) that returns a NEW Board with the move applied,
- 2. `TicTacToe.Core/Board.cs` line **21** — PROMPT: Ask Cursor to implement GetStatus() that checks rows, cols, diags, and draw.
- 3. `TicTacToe.Core/Board.cs` line **27** — PROMPT: Ask Cursor to implement GetEmptyCells() helper returning IEnumerable<(int r,int c)>.
- 4. `TicTacToe.Cli/Program.cs` line **9** — PROMPT: Ask Cursor to implement Render(board) that draws the 3x3 grid with coordinates.
- 5. `TicTacToe.Cli/Program.cs` line **17** — PROMPT: Ask Cursor to parse input, validate, and apply move; handle errors gracefully.
- 6. `TicTacToe.Cli/Program.cs` line **22** — PROMPT: Ask Cursor to extract GameLoop into a class and add undo/redo as stretch goals.
- 7. `TicTacToe.Core/HeuristicBot.cs` line **5** — PROMPT: Ask Cursor to implement a simple heuristic:
- 8. `TicTacToe.Core/IBotStrategy.cs` line **5** — PROMPT: Ask Cursor to implement ChooseMove(Board board) that selects a legal move for the current player.
- 9. `TicTacToe.Tests/BoardTests.cs` line **18** — PROMPT: Ask Cursor to make Apply(...) work immutably and then make this pass.

## Build, Run, Test
```bash
dotnet test  ./TicTacToe.Tests/TicTacToe.Tests.csproj
dotnet run   --project ./TicTacToe.Cli/TicTacToe.Cli.csproj
```

## Acceptance Criteria
- Immutable engine (`Apply`, `GetStatus`) with tests passing
- Heuristic bot plays legal moves (win/block/center/corners/edges)
- CLI renders board and handles invalid input without crashing
- (Stretch) Extract a `GameLoop` class and add undo/redo
