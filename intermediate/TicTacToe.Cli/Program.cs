using TicTacToe.Core;

Console.WriteLine("Tic-Tac-Toe — Human (X) vs Bot (O)");
var gameLoop = new GameLoop(new HeuristicBot());
gameLoop.Run();

public class GameLoop
{
    private readonly IBotStrategy _bot;
    private readonly Stack<Board> _history = new();
    private readonly Stack<Board> _redoStack = new();
    private Board _currentBoard;

    public GameLoop(IBotStrategy bot)
    {
        _bot = bot;
        _currentBoard = new Board();
        _history.Push(_currentBoard);
    }

    public void Run()
    {
        while (true)
        {
            Render(_currentBoard);
            ShowCommands();

            Console.Write("Enter command: ");
            var input = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(input)) continue;

            var command = input.Trim().ToLower();
            
            switch (command)
            {
                case "q":
                case "quit":
                    return;
                    
                case "u":
                case "undo":
                    Undo();
                    break;
                    
                case "r":
                case "redo":
                    Redo();
                    break;
                    
                case "h":
                case "help":
                    ShowCommands();
                    break;
                    
                default:
                    ProcessMove(input);
                    break;
            }
        }
    }

    private void ProcessMove(string input)
    {
        try
        {
            var move = ParseMove(input);
            ApplyMove(move);
            
            // Check status after human move
            var status = _currentBoard.GetStatus();
            if (status != GameStatus.InProgress)
            {
                Render(_currentBoard);
                DisplayGameResult(status);
                return;
            }
            
            // Bot move
            Console.WriteLine("Bot is thinking...");
            var botMove = _bot.ChooseMove(_currentBoard);
            ApplyMove(botMove);
            Console.WriteLine($"Bot placed {botMove.Player} at ({botMove.Row + 1},{botMove.Col + 1})");
            
            // Check status after bot move
            status = _currentBoard.GetStatus();
            if (status != GameStatus.InProgress)
            {
                Render(_currentBoard);
                DisplayGameResult(status);
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

    private void ApplyMove(Move move)
    {
        _currentBoard = _currentBoard.Apply(move);
        _history.Push(_currentBoard);
        _redoStack.Clear(); // Clear redo stack when new move is made
    }

    private void Undo()
    {
        if (_history.Count <= 1)
        {
            Console.WriteLine("Nothing to undo.");
            return;
        }

        var currentState = _history.Pop();
        _redoStack.Push(currentState);
        _currentBoard = _history.Peek();
        Console.WriteLine("Move undone.");
    }

    private void Redo()
    {
        if (_redoStack.Count == 0)
        {
            Console.WriteLine("Nothing to redo.");
            return;
        }

        var redoState = _redoStack.Pop();
        _currentBoard = redoState;
        _history.Push(_currentBoard);
        Console.WriteLine("Move redone.");
    }

    private void ShowCommands()
    {
        Console.WriteLine("Commands:");
        Console.WriteLine("  row,col - Make a move (e.g., '2,3')");
        Console.WriteLine("  u/undo  - Undo last move");
        Console.WriteLine("  r/redo  - Redo last undone move");
        Console.WriteLine("  h/help  - Show this help");
        Console.WriteLine("  q/quit  - Quit game");
        Console.WriteLine();
    }

    private static void Render(Board b)
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

    private static Move ParseMove(string input)
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

    private static void DisplayGameResult(GameStatus status)
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
}
