# START HERE — Beginner: Number Guess (CLI, .NET 8)

## Prerequisites
- Install **.NET 8 SDK**: https://dotnet.microsoft.com/download
- Verify install:
  ```bash
  dotnet --version
  dotnet --info
  ```

## Project Setup
1. Unzip the repo and open a terminal at the folder:
   ```bash
   cd classic-games/beginner
   ```
2. (Optional sanity) Try to build and run tests. **It will fail** until you complete the prompts below:
   ```bash
   dotnet build ./NumberGuess/NumberGuess.csproj
   dotnet test  ./NumberGuess.Tests/NumberGuess.Tests.csproj
   ```

## How to Use Cursor for this Exercise
- Open the `beginner` folder in **Cursor**.
- Work through the **Prompt Roadmap** below **in order**. For each step:
  1. Open the file at the indicated line.
  2. Copy the prompt text (after “PROMPT:”) and paste it into Cursor.
  3. Let Cursor generate code; review and accept with small edits if needed.
  4. Rebuild after a few steps to check progress.

## Prompt Roadmap (do these in this order)
- 1. `NumberGuess/Game.cs` line **18** — PROMPT: Implement ConfigureRange() to set (_lower,_upper) for Easy(1-50), Normal(1-100), Hard(1-500).
- 2. `NumberGuess/Program.cs` line **7** — PROMPT: Ask Cursor to implement Game.Play() that runs the main loop with input validation,
- 3. `NumberGuess/Program.cs` line **11** — PROMPT: Ask Cursor to add command-line parsing: support --difficulty Easy|Normal|Hard and --seed <int>.
- 4. `NumberGuess.Tests/ScoreCalculatorTests.cs` line **11** — PROMPT: Ask Cursor to implement Calculate() and make these tests pass.
- 5. `NumberGuess/Game.cs` line **33** — PROMPT: Implement ConfigureRange() that validates _difficulty and sets _lower/_upper accordingly.
- 6. `NumberGuess/Game.cs` line **41** — PROMPT: Implement a loop that reads lines from Console, supports 'quit', validates integers,
- 7. `NumberGuess/Game.cs` line **51** — PROMPT: Implement "warm/colder" logic: based on distance between lastGuess and _secret compared
- 8. `NumberGuess/ScoreCalculator.cs` line **5** — PROMPT: Ask Cursor to design a scoring function that rewards fewer attempts and higher difficulty.

## Build, Run, Test
```bash
# Build app and tests
dotnet build ./NumberGuess/NumberGuess.csproj
dotnet build ./NumberGuess.Tests/NumberGuess.Tests.csproj

# Run tests
dotnet test  ./NumberGuess.Tests/NumberGuess.Tests.csproj

# Run the game (after prompts are implemented)
dotnet run   --project ./NumberGuess/NumberGuess.csproj --difficulty Normal
```
> Tip: Add `--seed 42` to make runs deterministic for debugging.

## Acceptance Criteria
- Game starts and accepts input with **Easy/Normal/Hard** ranges
- Warm/colder hints work after the first guess
- Score is computed; first-try is best; tests pass
- Graceful handling of invalid input and `quit`
