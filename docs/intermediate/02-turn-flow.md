# Turn Flow Document

## **Typical Turn Flow Diagram**
The diagram shows the 11-step flow of a typical game turn, from rendering the board through human input, validation, bot response, and game state management.

```
┌─────────────────────────────────────────────────────────────────────────────┐
│                           TYPICAL GAME TURN FLOW                            │
└─────────────────────────────────────────────────────────────────────────────┘

┌─────────────┐    ┌─────────────┐    ┌─────────────┐    ┌─────────────┐
│  1. RENDER  │───▶│ 2. HUMAN    │───▶│ 3. VALIDATE │───▶│ 4. APPLY    │
│   BOARD     │    │   INPUT     │    │   MOVE      │    │   MOVE      │
└─────────────┘    └─────────────┘    └─────────────┘    └─────────────┘
                                                              │
                                                              ▼
┌─────────────┐    ┌─────────────┐    ┌─────────────┐    ┌─────────────┐
│ 9. GAME     │◀───│ 8. CHECK    │◀───│ 7. APPLY    │◀───│ 6. BOT      │
│   OVER?     │    │   STATUS    │    │   BOT MOVE  │    │   MOVE      │
└─────────────┘    └─────────────┘    └─────────────┘    └─────────────┘
       │                                                          │
       ▼                                                          │
┌─────────────┐                                                   │
│ 10. DISPLAY │                                                   │
│   RESULT    │                                                   │
└─────────────┘                                                   │
       │                                                          │
       ▼                                                          │
┌─────────────┐                                                   │
│ 11. LOOP    │◀──────────────────────────────────────────────────┘
│   OR QUIT   │
└─────────────┘
```

## **Detailed Turn Flow (Bullet Points)**
I've broken down the turn into 5 distinct phases:

1. **Phase 1: Human Turn** - Board rendering, input collection, validation, and move application
2. **Phase 2: Status Check** - Determine if human's move ended the game
3. **Phase 3: Bot Turn** - AI move generation and application (if game continues)
4. **Phase 4: Final Status Check** - Determine if bot's move ended the game
5. **Phase 5: Game Flow Control** - Decision to continue or end the game

## **Key Architectural Principles**
I've highlighted the important design patterns used:
- **Immutability** - Each move creates a new Board instance
- **Separation of Concerns** - UI, game logic, and AI are separate
- **Strategy Pattern** - Pluggable bot behavior via IBotStrategy
- **Error Handling** - Graceful invalid input handling
- **State Machine** - GameStatus enum drives flow decisions

The flow clearly shows how the human move → validate → status check → bot move sequence works, with proper error handling and game state management at each step. This will help students understand the complete game loop before implementing the individual components.