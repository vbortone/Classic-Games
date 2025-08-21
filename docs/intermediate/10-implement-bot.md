# Implement Heurisitcs

The implementation follows the three rules you specified:

1. **Winning Move**: The `FindWinningMove` helper method tests each empty cell by applying a temporary move and checking if it results in a win for the current player.

2. **Blocking Move**: Uses the same `FindWinningMove` method but with the opponent's player to find moves that would let the opponent win, then blocks them.

3. **Position Preference**: Implements the preference order:
   - Center (position 1,1)
   - Corners (positions 0,0, 0,2, 2,0, 2,2)
   - Edges (positions 0,1, 1,0, 1,2, 2,1)

The bot will always make the best strategic move available according to these rules, making it a strong opponent that prioritizes winning opportunities, defensive blocking, and strategic positioning.