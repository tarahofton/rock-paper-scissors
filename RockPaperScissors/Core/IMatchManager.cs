using RockPaperScissors.Domain;

namespace RockPaperScissors.Core
{
    public interface IMatchManager
    {
        Match Load(OpponentType opponentType);

        GameResult PlayGame(Match match, MoveChoice moveChoice);

        MatchResult IsGameOver(Match match);
    }
}
