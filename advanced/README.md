# START HERE — Advanced: Web Tic-Tac-Toe (Razor Pages, Minimax, .NET 8)

## Prerequisites
- **.NET 8 SDK** installed and verified with `dotnet --version`

## Project Setup
```bash
cd classic-games/advanced
dotnet build ./WebTicTacToe/WebTicTacToe.csproj
# The site won't be fully functional until you complete the prompts below.
dotnet run  --project ./WebTicTacToe/WebTicTacToe.csproj
```

Open the URL printed by `dotnet run` in your browser.


## Before You Prompt: Explore the Project
Take 5–10 minutes to see what renders and what’s missing.

1. Build & run the site:
   ```bash
   dotnet build ./WebTicTacToe/WebTicTacToe.csproj
   dotnet run  --project ./WebTicTacToe/WebTicTacToe.csproj
   ```
   The page will load but won’t be fully functional until you implement DI, state, and handlers.

2. Use **Cursor** to get a walkthrough:
   - Ask for a **tour of the Razor Pages flow**: `Program.cs` → DI → `Index.cshtml.cs` → `Index.cshtml`.
   - Have it **summarize the responsibilities** of `GameState` and `GameService` and how session should be used.
   - Ask Cursor to **list each `// PROMPT:`** and describe the required changes in everyday language.


## Using Cursor
Open the `advanced/WebTicTacToe` project in Cursor and follow the road map below. Implement DI and models first, then service logic with **Minimax**, then page handlers/UI.

## Prompt Roadmap (do these in this order)
- 1. `WebTicTacToe/Program.cs` line **6** — PROMPT: Ask Cursor to register GameService and any required helpers with DI.
- 2. `WebTicTacToe/Models/GameState.cs` line **3** — PROMPT: Ask Cursor to implement a serializable GameState that holds:
- 3. `WebTicTacToe/Services/GameService.cs` line **5** — PROMPT: Ask Cursor to implement GameService with methods:
- 4. `WebTicTacToe/Pages/Index.cshtml.cs` line **13** — PROMPT: Ask Cursor to expose state to the page (board/status/scoreboard)
- 5. `WebTicTacToe/Pages/Index.cshtml` line **9** — PROMPT: Ask Cursor to render a 3x3 grid as a form with buttons.

### Suggested Minimax seed prompt
> *"Create a Minimax algorithm for Tic-Tac-Toe (3x3) in C#. Return the best legal move for the current player. Use evaluation: win=+10, loss=-10, draw=0; consider depth to prefer faster wins and slower losses."*

## Build & Run
```bash
dotnet build ./WebTicTacToe/WebTicTacToe.csproj
dotnet run  --project ./WebTicTacToe/WebTicTacToe.csproj
```

## Acceptance Criteria
- Playable at `/`
- Bot uses **Minimax** and never loses
- Session scoreboard persists across rematches (in-memory session only)

## Closing: Ideate Enhancements with Cursor
When your acceptance criteria are green, take 5–10 minutes to brainstorm **stretch features**.

Use one or more of these prompts in Cursor:
- *“Suggest 10 practical feature improvements for this project, grouped by effort (S/M/L). Explain the user benefit of each.”*
- *“Identify weak spots in the current design and propose refactors that improve testability or clarity.”*
- *“Pick one Small feature and draft a task checklist with acceptance tests. Then implement it.”*
- *“Write a short README ‘What I’d Do Next’ section summarizing potential upgrades.”*
