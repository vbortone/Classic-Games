# Prompt 3: Implement the GetEmptyCells() method

## Purpose
This method identifies and returns all available (empty) cells where a player can make their next move.

## Implementation Details

```csharp
public IEnumerable<(int r, int c)> GetEmptyCells()
{
    for (int r = 0; r < 3; r++)
        for (int c = 0; c < 3; c++)
            if (_cells[r, c] == Cell.Empty)
                yield return (r, c);
}
```

## Key Features

1. **Return Type**: `IEnumerable<(int r, int c)>` - Returns a collection of tuples containing row and column coordinates
2. **Lazy Evaluation**: Uses `yield return` for efficient memory usage - only generates values when needed
3. **Nested Loops**: Iterates through all 9 positions on the 3x3 board (rows 0-2, columns 0-2)
4. **Empty Cell Detection**: Checks if each cell equals `Cell.Empty` before including it in the result

## Usage Examples

This method is particularly useful for:

- **AI/Bot Strategies**: Finding all possible moves for computer players
- **Game Logic**: Determining if the game is a draw (when no empty cells remain)
- **Move Validation**: Checking available positions before applying moves
- **UI Rendering**: Highlighting available positions to human players

## Example Output
If the board has X's in positions (0,0), (1,1), (2,2) and O's in positions (0,1), (1,0), the method would return:
```
(0, 2), (1, 2), (2, 0), (2, 1)
```

The method follows C# best practices with clean, readable code and efficient lazy evaluation using the iterator pattern.