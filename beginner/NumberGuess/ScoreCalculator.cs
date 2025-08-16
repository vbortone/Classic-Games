namespace NumberGuess;

public static class ScoreCalculator
{
    public static int Calculate(int attempts, int rangeSize)
    {
        int baseScore = rangeSize / 10;
        int score = Math.Max(1, baseScore + attempts - 1);
        return score;
    }
}
