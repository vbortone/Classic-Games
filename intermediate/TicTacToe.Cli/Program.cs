using TicTacToe.Core;

Console.WriteLine("Tic-Tac-Toe — Human (X) vs Bot (O)");
var board = new Board();
IBotStrategy bot = new HeuristicBot();

while (true)
{
    // PROMPT: Ask Cursor to implement Render(board) that draws the 3x3 grid with coordinates.
    Render(board);

    // Human move
    Console.Write("Enter row,col (1-3,1-3) or 'q' to quit: ");
    var input = Console.ReadLine();
    if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase)) break;

    // Parse input, validate, and apply move; handle errors gracefully
    try
    {
        var move = ParseMove(input);
        board = board.Apply(move);
        
        // Check status after human move
        var status = board.GetStatus();
        if (status != GameStatus.InProgress)
        {
            Render(board);
            DisplayGameResult(status);
            break;
        }
        
        // Bot move
        Console.WriteLine("Bot is thinking...");
        var botMove = bot.ChooseMove(board);
        board = board.Apply(botMove);
        Console.WriteLine($"Bot placed {botMove.Player} at ({botMove.Row + 1},{botMove.Col + 1})");
        
        // Check status after bot move
        status = board.GetStatus();
        if (status != GameStatus.InProgress)
        {
            Render(board);
            DisplayGameResult(status);
            break;
        }
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Invalid move: {ex.Message}");
        Console.WriteLine("Please enter coordinates in format 'row,col' (e.g., '2,3')");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
    
}

// PROMPT: Ask Cursor to extract GameLoop into a class and add undo/redo as stretch goals.

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

static Move ParseMove(string input)
{
    if (string.IsNullOrWhiteSpace(input))
    {
        throw new ArgumentException("Input cannot be empty");
    }
    
    var parts = input.Split(',');
    if (parts.Length != 2)
    {
        throw new ArgumentException("Input must be in format 'row,col' (e.g., '2,3')");
    }
    
    if (!int.TryParse(parts[0].Trim(), out int row) || !int.TryParse(parts[1].Trim(), out int col))
    {
        throw new ArgumentException("Row and column must be valid numbers");
    }
    
    // Convert from 1-based user input to 0-based array indices
    row--;
    col--;
    
    if (row < 0 || row >= 3 || col < 0 || col >= 3)
    {
        throw new ArgumentException("Row and column must be between 1 and 3");
    }
    
    return new Move(row, col, Cell.X);
}

static void DisplayGameResult(GameStatus status)
{
    var message = status switch
    {
        GameStatus.XWins => "Congratulations! You (X) win!",
        GameStatus.OWins => "Bot (O) wins!",
        GameStatus.Draw => "It's a draw!",
        _ => "Game is still in progress"
    };
    
    Console.WriteLine(message);
}
