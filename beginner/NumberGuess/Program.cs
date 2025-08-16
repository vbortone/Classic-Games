using NumberGuess;

// Parse command-line arguments
string difficulty = "Normal";
int? seed = null;

for (int i = 0; i < args.Length; i++)
{
    switch (args[i].ToLower())
    {
        case "--difficulty":
            if (i + 1 < args.Length)
            {
                string diffArg = args[i + 1];
                if (diffArg.Equals("Easy", StringComparison.OrdinalIgnoreCase) ||
                    diffArg.Equals("Normal", StringComparison.OrdinalIgnoreCase) ||
                    diffArg.Equals("Hard", StringComparison.OrdinalIgnoreCase))
                {
                    difficulty = diffArg;
                    i++; // Skip the next argument since we consumed it
                }
                else
                {
                    Console.WriteLine($"Warning: Invalid difficulty '{diffArg}'. Using Normal.");
                }
            }
            else
            {
                Console.WriteLine("Warning: --difficulty requires a value. Using Normal.");
            }
            break;
            
        case "--seed":
            if (i + 1 < args.Length && int.TryParse(args[i + 1], out int seedValue))
            {
                seed = seedValue;
                i++; // Skip the next argument since we consumed it
            }
            else
            {
                Console.WriteLine("Warning: --seed requires a valid integer value. Using random seed.");
            }
            break;
            
        default:
            // For backward compatibility, treat first positional argument as difficulty
            if (i == 0 && !args[i].StartsWith("--"))
            {
                if (args[i].Equals("Easy", StringComparison.OrdinalIgnoreCase) ||
                    args[i].Equals("Normal", StringComparison.OrdinalIgnoreCase) ||
                    args[i].Equals("Hard", StringComparison.OrdinalIgnoreCase))
                {
                    difficulty = args[i];
                }
                else
                {
                    Console.WriteLine($"Warning: Invalid difficulty '{args[i]}'. Using Normal.");
                }
            }
            else if (!args[i].StartsWith("--"))
            {
                Console.WriteLine($"Warning: Unknown argument '{args[i]}' ignored.");
            }
            break;
    }
}

Console.WriteLine($"Number Guess — Difficulty: {difficulty}");
if (seed.HasValue)
{
    Console.WriteLine($"Using seed: {seed.Value}");
}

var game = new Game(difficulty, seed);
// PROMPT: Ask Cursor to implement Game.Play() that runs the main loop with input validation,
// 'quit' command, and replay option. Provide clear prompts and friendly errors.
game.Play();
