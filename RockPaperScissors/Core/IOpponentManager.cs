using RockPaperScissors.Domain;

namespace RockPaperScissors.Core
{
    public interface IOpponentManager
    {
        MoveChoice GetNextMove(OpponentType opponentType, MoveChoice? previousMove);
    }
}