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
        // TODO: Students: implement with Cursor.
        // PROMPT: Implement ConfigureRange() that validates _difficulty and sets _lower/_upper accordingly.
        // If an unknown difficulty is provided, default to Normal and print a note.
        throw new NotImplementedException();
    }

    public void Play()
    {
        // TODO: Students: implement with Cursor.
        // PROMPT: Implement a loop that reads lines from Console, supports 'quit', validates integers,
        // increments _attempts, prints "higher/lower", and uses a warm/colder hint from GetHint().
        // On correct guess: compute score via ScoreCalculator.Calculate(_attempts, _upper - _lower + 1),
        // update _bestScore, and ask to replay (y/n). If yes, ResetSecret().
        throw new NotImplementedException();
    }

    private string GetHint(int lastGuess)
    {
        // TODO: Students: implement with Cursor.
        // PROMPT: Implement "warm/colder" logic: based on distance between lastGuess and _secret compared
        // to previous distance (store previous in a field). On first guess, just say "higher"/"lower".
        throw new NotImplementedException();
    }
}
