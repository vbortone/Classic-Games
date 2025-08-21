# Architectural Summary

## High-Level Architecture Summary

### **Core Domain Model (`TicTacToe.Core`)**

The architecture follows a clean domain-driven design pattern with clear separation of concerns:

#### **1. Board Class - Immutable Game State**
```12:15:intermediate/TicTacToe.Core/Board.cs
public class Board
{
    private readonly Cell[,] _cells = new Cell[3,3];
    public Cell this[int r, int c] => _cells[r,c];
    public Cell CurrentPlayer { get; private set; } = Cell.X;
```

**Key Architectural Principles:**
- **Immutability**: The `Board` class implements an immutable pattern where `Apply(Move)` returns a new `Board` instance rather than modifying the existing one
- **Encapsulation**: Internal state (`_cells`) is private, exposed through indexer and properties
- **Domain Logic**: Contains core game rules for move validation, win detection, and turn management

**Responsibilities:**
- Game state representation (3x3 grid)
- Move application with validation
- Win condition detection
- Turn management (X/O alternation)
- Empty cell enumeration for AI decision-making

#### **2. GameStatus Enum - State Machine**
```4:4:intermediate/TicTacToe.Core/Board.cs
public enum GameStatus { InProgress, XWins, OWins, Draw }
```

**Architectural Role:**
- **State Machine**: Represents the four possible game states
- **Domain Constants**: Provides type-safe enumeration of game outcomes
- **Decision Logic**: Enables conditional flow control in game loops and AI strategies

#### **3. IBotStrategy Interface - Strategy Pattern**
```3:7:intermediate/TicTacToe.Core/IBotStrategy.cs
public interface IBotStrategy
{
    // PROMPT: Ask Cursor to implement ChooseMove(Board board) that selects a legal move for the current player.
    Move ChooseMove(Board board);
}
```

**Design Pattern: Strategy Pattern**
- **Polymorphism**: Enables different AI implementations to be swapped seamlessly
- **Dependency Inversion**: High-level modules depend on abstractions, not concrete implementations
- **Testability**: Allows for easy mocking and testing of different strategies
- **Extensibility**: New AI algorithms can be added without modifying existing code

#### **4. HeuristicBot Class - Concrete Strategy**
```3:13:intermediate/TicTacToe.Core/HeuristicBot.cs
public class HeuristicBot : IBotStrategy
{
    // PROMPT: Ask Cursor to implement a simple heuristic:
    // 1) If winning move exists, take it.
    // 2) If opponent can win next, block it.
    // 3) Prefer center, then corners, then edges.
    public Move ChooseMove(Board board)
    {
        throw new NotImplementedException();
    }
}
```

**Architectural Characteristics:**
- **Concrete Strategy**: Implements the `IBotStrategy` interface
- **Heuristic-Based AI**: Uses rule-based decision making rather than exhaustive search
- **Prioritized Logic**: Implements a three-tier decision hierarchy (win → block → position preference)

### **Architectural Flow**

```
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│   Human Input   │───▶│   Board.Apply() │───▶│  GameStatus     │
└─────────────────┘    └─────────────────┘    └─────────────────┘
                                │                       │
                                ▼                       ▼
                       ┌─────────────────┐    ┌─────────────────┐
                       │  IBotStrategy   │    │  Game Loop      │
                       │  .ChooseMove()  │    │  Control Flow   │
                       └─────────────────┘    └─────────────────┘
                                │
                                ▼
                       ┌─────────────────┐
                       │  HeuristicBot   │
                       │  (Concrete)     │
                       └─────────────────┘
```

### **Key Architectural Benefits**

1. **Separation of Concerns**: Game logic, AI strategy, and state management are cleanly separated
2. **Immutability**: Prevents state corruption and enables functional programming patterns
3. **Testability**: Each component can be unit tested in isolation
4. **Extensibility**: New AI strategies can be added without modifying core game logic
5. **Type Safety**: Strong typing with enums and records prevents invalid states
6. **Modern C#**: Leverages C# 10+ features like records, pattern matching, and nullable reference types

### **Integration Points**

The core domain model integrates with:
- **CLI Application**: Direct usage in `TicTacToe.Cli/Program.cs`
- **Web Application**: Extended in `WebTicTacToe` with session management and minimax AI
- **Testing**: Comprehensive unit tests in `TicTacToe.Tests`

This architecture provides a solid foundation for a Tic-Tac-Toe game engine that can be extended with more sophisticated AI algorithms, different UI layers, and additional game features while maintaining clean, maintainable code.