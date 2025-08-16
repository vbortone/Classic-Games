namespace NumberGuess;

public class Game
{
    private readonly string _difficulty;
    private readonly Random _random;
    private int _secret;
    private int _lower;
    private int _upper;
    private bool _running;
    private int _attempts;
    private int _bestScore = int.MaxValue;
    private int? _lastGuess; // Add field for warm/colder hints

    public Game(string difficulty, int? seed = null)
    {
        _difficulty = difficulty;
        _random = seed.HasValue ? new Random(seed.Value) : new Random();
        // PROMPT: Implement ConfigureRange() to set (_lower,_upper) for Easy(1-50), Normal(1-100), Hard(1-500).
        ConfigureRange();
        ResetSecret();
    }

    private void ResetSecret()
    {
        _secret = _random.Next(_lower, _upper + 1);
        _attempts = 0;
        _running = true;
    }

    private void ConfigureRange()
    {
        switch (_difficulty.ToLower())
        {
            case "easy":
                _lower = 1;
                _upper = 50;
                break;
            case "normal":
                _lower = 1;
                _upper = 100;
                break;
            case "hard":
                _lower = 1;
                _upper = 500;
                break;
            default:
                Console.WriteLine($"Unknown difficulty '{_difficulty}'. Defaulting to Normal.");
                _lower = 1;
                _upper = 100;
                break;
        }
    }

    public void Play()
    {
        Console.WriteLine($"\n🎯 Welcome to Number Guess! ({_difficulty} difficulty)");
        Console.WriteLine($"I'm thinking of a number between {_lower} and {_upper}.");
        Console.WriteLine("Type 'quit' to exit, or enter your guess:");
        
        while (_running)
        {
            Console.Write($"\nGuess #{_attempts + 1}: ");
            string? input = Console.ReadLine()?.Trim();
            
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("❌ Please enter a number or 'quit'.");
                continue;
            }
            
            if (input.ToLower() == "quit")
            {
                Console.WriteLine(" Thanks for playing! Goodbye!");
                _running = false;
                return;
            }
            
            if (!int.TryParse(input, out int guess))
            {
                Console.WriteLine("❌ Please enter a valid number or 'quit'.");
                continue;
            }
            
            if (guess < _lower || guess > _upper)
            {
                Console.WriteLine($"❌ Please enter a number between {_lower} and {_upper}.");
                continue;
            }
            
            _attempts++;
            
            if (guess == _secret)
            {
                HandleCorrectGuess();
            }
            else
            {
                HandleIncorrectGuess(guess);
            }
        }
    }

    private void HandleCorrectGuess()
    {
        int score = ScoreCalculator.Calculate(_attempts, _upper - _lower + 1);
        
        Console.WriteLine($"\n🎉 Congratulations! You found the number in {_attempts} attempt(s)!");
        Console.WriteLine($"Your score: {score} (lower is better)");
        
        if (score < _bestScore)
        {
            Console.WriteLine("🏆 New best score!");
            _bestScore = score;
        }
        else if (_bestScore != int.MaxValue)
        {
            Console.WriteLine($"Your best score so far: {_bestScore}");
        }
        
        AskForReplay();
    }

    private void HandleIncorrectGuess(int guess)
    {
        string direction = guess < _secret ? "higher" : "lower";
        string hint = GetHint(guess);
        
        Console.WriteLine($" {direction.ToUpper()}! {hint}");
    }

    private void AskForReplay()
    {
        while (true)
        {
            Console.Write("\nWould you like to play again? (y/n): ");
            string? input = Console.ReadLine()?.Trim().ToLower();
            
            if (input == "y" || input == "yes")
            {
                Console.WriteLine("\n🔄 Starting a new game...");
                _lastGuess = null; // Reset for new game
                ResetSecret();
                Console.WriteLine($"I'm thinking of a number between {_lower} and {_upper}.");
                break;
            }
            else if (input == "n" || input == "no")
            {
                Console.WriteLine(" Thanks for playing! Goodbye!");
                _running = false;
                break;
            }
            else
            {
                Console.WriteLine("❌ Please enter 'y' for yes or 'n' for no.");
            }
        }
    }

    private string GetHint(int lastGuess)
    {
        if (!_lastGuess.HasValue)
        {
            _lastGuess = lastGuess;
            return "First guess!";
        }
        
        int currentDistance = Math.Abs(lastGuess - _secret);
        int previousDistance = Math.Abs(_lastGuess.Value - _secret);
        
        string hint;
        if (currentDistance < previousDistance)
        {
            hint = "Getting warmer! 🔥";
        }
        else if (currentDistance > previousDistance)
        {
            hint = "Getting colder! ❄️";
        }
        else
        {
            hint = "Same distance! 🎯";
        }
        
        _lastGuess = lastGuess;
        return hint;
    }
}
