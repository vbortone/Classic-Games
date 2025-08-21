# Prompt 1: Implement Move() method

## Key Features:

1. **Immutable Design**: Creates a completely new `Board` instance instead of modifying the existing one
2. **Comprehensive Validation**: 
   - Checks if the move position is within bounds (0-2 for both row and column)
   - Verifies the target cell is empty
   - Ensures the move's player matches the current player
3. **State Copying**: Deep copies the entire `_cells` array to preserve the current game state
4. **Move Application**: Places the player's mark in the specified position
5. **Player Switching**: Flips the `CurrentPlayer` from X to O or vice versa
6. **Exception Handling**: Throws descriptive `ArgumentException` messages for invalid moves

## How it works:

- The method takes a `Move` record containing the row, column, and player
- It validates the move thoroughly before applying it
- Creates a new `Board` instance and copies all current cell states
- Applies the move to the new board
- Switches the current player
- Returns the new board instance

This implementation follows .NET best practices with proper validation, clear error messages, and maintains immutability as requested. The original board remains unchanged, and a new board with the move applied is returned.