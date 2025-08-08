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
    // TODO: Students implement with Cursor.
    throw new NotImplementedException();
}
