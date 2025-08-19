# Prompt 5: Parse input, validate, and apply move; handle errors gracefully

## **Input Parsing & Validation (`ParseMove` method)**
- **Robust parsing**: Handles `row,col` format with proper validation
- **Input sanitization**: Trims whitespace and validates format
- **Range validation**: Ensures coordinates are between 1-3 (converts to 0-based indices)
- **Error handling**: Throws descriptive `ArgumentException` for invalid input

## **Game Loop Logic**
- **Human move processing**: Parses input, validates, and applies move
- **Status checking**: After each move, checks if game has ended
- **Bot integration**: Invokes `bot.ChooseMove()` and applies bot's move
- **Graceful error handling**: Catches exceptions and provides helpful error messages
- **Game termination**: Breaks loop when game ends (win/draw)

## **Helper Methods**
- **`DisplayGameResult`**: Shows appropriate messages for win/draw scenarios
- **`Render`**: Displays the game board with coordinates (already implemented)

## **Key Features**
1. **Immutable board updates**: Uses `board.Apply(move)` which returns a new board
2. **Proper error handling**: Catches both `ArgumentException` and general exceptions
3. **User-friendly messages**: Clear instructions and error messages
4. **Game state management**: Proper status checking after each move
5. **Bot feedback**: Shows where the bot placed its move

The implementation follows .NET best practices with:
- Descriptive method names
- Proper exception handling
- C# 8+ pattern matching
- Immutable data structures
- Clean separation of concerns

The game will now run a complete loop where players can input moves, see validation errors, watch the bot respond, and see the final game result!