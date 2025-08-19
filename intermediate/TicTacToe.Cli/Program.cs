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

    // PROMPT: Ask Cursor to parse input, validate, and apply move; handle errors gracefully.
    // After human move, check status.
    // If game continues, invoke bot. Then check status again and loop.
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
