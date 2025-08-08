using NumberGuess;

var difficulty = args.Length > 0 ? args[0] : "Normal";
Console.WriteLine($"Number Guess — Difficulty: {difficulty}");

var game = new Game(difficulty);
// PROMPT: Ask Cursor to implement Game.Play() that runs the main loop with input validation,
// 'quit' command, and replay option. Provide clear prompts and friendly errors.
game.Play();

// PROMPT: Ask Cursor to add command-line parsing: support --difficulty Easy|Normal|Hard and --seed <int>.
// Then refactor Program to parse those options and pass into Game.
