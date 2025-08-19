# Prompt 6: Implement GampeLoop class

## **GameLoop Class Features**

### **Core Structure**
- **Encapsulated state**: All game state is managed within the class
- **Dependency injection**: Takes `IBotStrategy` in constructor for flexibility
- **Clean separation**: Game logic is separated from the main program

### **Undo/Redo System**
- **History management**: Uses `Stack<Board>` to track game states
- **Redo stack**: Separate stack for undone moves that can be redone
- **State preservation**: Each move creates a new board state (immutable design)
- **Smart clearing**: Redo stack is cleared when new moves are made

### **Command System**
- **Multiple input formats**: Supports both short and long commands
- **Help system**: Built-in help command shows all available options
- **Flexible parsing**: Handles moves, commands, and edge cases

## **Available Commands**
- `row,col` - Make a move (e.g., `2,3`)
- `u` or `undo` - Undo last move
- `r` or `redo` - Redo last undone move  
- `h` or `help` - Show command help
- `q` or `quit` - Quit game

## **Key Implementation Details**

### **Undo Logic**
```csharp
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
```

### **Redo Logic**
```csharp
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
```

### **Move Application**
```csharp
private void ApplyMove(Move move)
{
    _currentBoard = _currentBoard.Apply(move);
    _history.Push(_currentBoard);
    _redoStack.Clear(); // Clear redo stack when new move is made
}
```

## **Benefits of This Design**

1. **Maintainability**: Clean separation of concerns
2. **Testability**: GameLoop can be easily unit tested
3. **Extensibility**: Easy to add new commands or features
4. **User Experience**: Intuitive undo/redo with helpful feedback
5. **Memory Efficiency**: Uses immutable boards with stack-based history
6. **Error Handling**: Graceful handling of edge cases

The implementation follows .NET best practices with proper encapsulation, dependency injection, and clean code principles. Players can now easily undo mistakes and experiment with different moves!