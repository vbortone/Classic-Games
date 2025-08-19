# Propmt 2: Implement GetStatus() Method

## Win Condition Checks:

1. **Rows**: Checks each of the 3 rows for three matching non-empty cells
2. **Columns**: Checks each of the 3 columns for three matching non-empty cells  
3. **Main Diagonal**: Checks the diagonal from top-left (0,0) to bottom-right (2,2)
4. **Anti-Diagonal**: Checks the diagonal from top-right (0,2) to bottom-left (2,0)

## Logic Flow:

1. **Win Detection**: For each win condition, it first ensures the cells aren't empty, then checks if all three cells match
2. **Player Determination**: Returns `GameStatus.XWins` or `GameStatus.OWins` based on which player achieved the win
3. **Draw Detection**: If no win is found, checks if all 9 cells are filled (no empty cells remain)
4. **In Progress**: If no win and not a draw, the game is still in progress

## Key Features:

- **Early Return**: Returns immediately when a win condition is found (no need to check further)
- **Empty Cell Validation**: Ensures win conditions only count non-empty cells
- **Efficient Draw Check**: Uses nested loops with early break to check for empty cells
- **Clear Logic**: Each win condition is clearly separated and commented

The method follows the standard Tic-Tac-Toe rules and will correctly identify:
- X wins (3 X's in a row, column, or diagonal)
- O wins (3 O's in a row, column, or diagonal)  
- Draw (all cells filled with no winner)
- In Progress (game still ongoing)