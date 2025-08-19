# Prompt 4: Implment Render() method

Perfect! Now I understand the structure. The `Board` class has a 3x3 grid with `Cell` enum values (`Empty`, `X`, `O`), and I can access cells using the indexer `board[row, col]`. Let me implement the `Render` method that draws a 3x3 grid with coordinates:

```csharp:intermediate/TicTacToe.Cli/Program.cs
static void Render(Board b)
{
    Console.WriteLine("    1   2   3");
    Console.WriteLine("  ┌───┬───┬───┐");
    
    for (int row = 0; row < 3; row++)
    {
        Console.Write($" {row + 1} │");
        
        for (int col = 0; col < 3; col++)
        {
            var cell = b[row, col];
            var symbol = cell switch
            {
                Cell.Empty => " ",
                Cell.X => "X",
                Cell.O => "O",
                _ => " "
            };
            
            Console.Write($" {symbol} │");
        }
        
        Console.WriteLine();
        
        if (row < 2)
        {
            Console.WriteLine("  ├───┼───┼───┤");
        }
    }
    
    Console.WriteLine("  └───┴───┴───┘");
    Console.WriteLine();
}
```

This implementation:

1. **Shows coordinates**: Column numbers (1, 2, 3) at the top and row numbers (1, 2, 3) on the left
2. **Creates a visual grid**: Uses Unicode box-drawing characters to create a clean 3x3 grid
3. **Displays cell contents**: Shows "X", "O", or empty space for each cell
4. **Uses pattern matching**: Leverages C# 8+ switch expression for clean cell symbol mapping
5. **Follows .NET conventions**: Uses descriptive variable names and proper formatting

The output will look like:
```
    1   2   3
  ┌───┬───┬───┐
 1│   │   │   │
  ├───┼───┼───┤
 2│   │ X │   │
  ├───┼───┼───┤
 3│   │   │ O │
  └───┴───┴───┘
```

This provides a clear, professional-looking game board that makes it easy for players to reference coordinates when making moves.