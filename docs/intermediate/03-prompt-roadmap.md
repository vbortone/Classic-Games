# Prompt Roadmap

## Enumerated PROMPT Lines in Order:

### 1. **`TicTacToe.Core/Board.cs` line 13** — Core Engine
**PROMPT:** Implement immutable `Apply(Move)` that returns a NEW Board with the move applied, throws on invalid move, and flips CurrentPlayer.

**What to implement:**
- Create a new Board instance with the move applied
- Validate the move (check bounds, ensure cell is empty)
- Update the CurrentPlayer (flip from X to O or vice versa)
- Return the new Board instance (immutable pattern)

### 2. **`TicTacToe.Core/Board.cs` line 20** — Game Logic
**PROMPT:** Implement `GetStatus()` that checks rows, cols, diags, and draw.

**What to implement:**
- Check all 3 rows for a winner (3 in a row)
- Check all 3 columns for a winner (3 in a row)
- Check both diagonals for a winner (3 in a row)
- Check for draw condition (all cells filled)
- Return appropriate GameStatus enum value

### 3. **`TicTacToe.Core/Board.cs` line 26** — Helper Method
**PROMPT:** Implement `GetEmptyCells()` helper returning `IEnumerable<(int r,int c)>`.

**What to implement:**
- Iterate through the 3x3 grid
- Find all cells that are `Cell.Empty`
- Return coordinates as tuples of (row, column)
- Use yield return for efficient enumeration

### 4. **`TicTacToe.Cli/Program.cs` line 8** — UI Rendering
**PROMPT:** Implement `Render(board)` that draws the 3x3 grid with coordinates.

**What to implement:**
- Display a visual 3x3 grid showing X, O, and empty cells
- Show coordinate labels (1-3 for rows/columns)
- Format the output clearly for user interaction
- Handle the current game state display

### 5. **`TicTacToe.Cli/Program.cs` line 16** — Input Processing
**PROMPT:** Parse input, validate, and apply move; handle errors gracefully.

**What to implement:**
- Parse user input in format "row,col" (e.g., "2,3")
- Validate input format and bounds (1-3 for both row and column)
- Convert to 0-based indices for internal use
- Apply the move to the board
- Handle invalid input with user-friendly error messages
- Check game status after human move
- Invoke bot move if game continues

### 6. **`TicTacToe.Cli/Program.cs` line 21** — Architecture Enhancement
**PROMPT:** Extract GameLoop into a class and add undo/redo as stretch goals.

**What to implement:**
- Create a `GameLoop` class to encapsulate the main game logic
- Move the while loop and game state management into this class
- Add undo functionality (maintain move history)
- Add redo functionality (restore previous states)
- Implement proper state management for the game flow

### 7. **`TicTacToe.Core/HeuristicBot.cs` line 4** — AI Strategy
**PROMPT:** Implement a simple heuristic: 1) If winning move exists, take it. 2) If opponent can win next, block it. 3) Prefer center, then corners, then edges.

**What to implement:**
- Check if bot can win in one move (priority 1)
- Check if opponent can win next move and block it (priority 2)
- If no immediate win/block, prefer center cell (1,1)
- If center taken, prefer corner cells (0,0), (0,2), (2,0), (2,2)
- If no corners available, choose any edge cell
- Return a valid Move object

### 8. **`TicTacToe.Core/IBotStrategy.cs` line 4** — Interface Implementation
**PROMPT:** Implement `ChooseMove(Board board)` that selects a legal move for the current player.

**What to implement:**
- This is the interface method that HeuristicBot implements
- Analyze the current board state
- Select a legal move based on the strategy
- Return a Move object with valid coordinates and current player

### 9. **`TicTacToe.Tests/BoardTests.cs` line 17** — Test Implementation
**PROMPT:** Make `Apply(...)` work immutably and then make this pass.

**What to implement:**
- Complete the test case for X winning on the top row
- Arrange a sequence of moves: `b = b.Apply(new Move(0,0,Cell.X))` etc.
- Verify that X wins after completing the top row
- Ensure the immutable pattern works correctly in the test

## Implementation Order Recommendation:
1. **Start with Board.cs** (prompts 1-3) - Core engine functionality
2. **Implement tests** (prompt 9) - Verify core functionality works
3. **Build CLI rendering** (prompt 4) - Basic UI
4. **Add input processing** (prompt 5) - Game interaction
5. **Implement bot strategy** (prompts 7-8) - AI opponent
6. **Enhance architecture** (prompt 6) - Code organization and stretch goals

This follows the recommended roadmap from the README: engine first, then bot, then CLI wiring.