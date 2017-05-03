using System.Linq;
using RockPaperScissors.Core;
using RockPaperScissors.Domain;

namespace RockPaperScissors.Managers
{
    public class MatchManager : IMatchManager
    {
        private readonly IMatchConfiguration _matchConfiguration;
        private readonly IRulesManager _rulesManager;
        private readonly IOpponentManager _opponentManager;

        public MatchManager(IMatchConfiguration matchConfiguration, IRulesManager rulesManager, IOpponentManager opponentManager)
        {
            _matchConfiguration = matchConfiguration;
            _rulesManager = rulesManager;
            _opponentManager = opponentManager;
        }

        public Match Load(OpponentType opponentType)
        {

            var player = new Player();
            var opponent = new Opponent(opponentType);
            var length = _matchConfiguration.MatchLength;
            var games = new Game[length];

            for (var i = 0; i < length; i++)
            {
                games[i] = new Game { Result = null };
            }

            return new Match
            {
                Player = player,
                Opponent = opponent,
                Games = games
            };
        }

        public GameResult PlayGame(Match match, MoveChoice moveChoice)
        {
            var rules = _rulesManager.GetRules();
            var opponentMove = _opponentManager.GetNextMove(match.Opponent.OpponentType, match.Opponent.PreviousMove);
            var result = new GameResult { YourMove = moveChoice, OpponentMove = opponentMove };
            if (opponentMove == moveChoice)
            {
                result.Result = Result.Draw;
                return result;
            }

            var moveChoiceRule = rules.FirstOrDefault(_ => _.MoveChoice == moveChoice);
            if (moveChoiceRule == null)
            {
                throw new RulesException($"Rule not found where MoveChoice is {moveChoice}");
            }

            if (moveChoiceRule.BeatsMoveChoice == opponentMove)
            {
                result.Result = Result.Win;
                return result;
            }

            result.Result = Result.Lose;
            return result;
        }

        public MatchResult IsGameOver(Match match)
        {
            var matchLength = _matchConfiguration.MatchLength;
            var minimumGamesToWin = matchLength - 1;
            var games = match.Games.ToList();

            if (games.Count(_ => _.Result.HasValue) < minimumGamesToWin)
            {
                return null;
            }

            if (games.Count(_ => _.Result == Result.Win) >= minimumGamesToWin)
            {
                return new MatchResult { Result = Result.Win, Match = match };
            }

            if (games.Count(_ => _.Result == Result.Lose) >= minimumGamesToWin)
            {
                return new MatchResult { Result = Result.Lose, Match = match };
            }

            if (games.Count(_ => _.Result == Result.Draw) >= minimumGamesToWin)
            {
                return new MatchResult { Result = Result.Draw, Match = match };
            }

            if (games.Count(_ => _.Result.HasValue) == matchLength)
            {
                return new MatchResult { Result = Result.Draw, Match = match };
            }

            return null;
        }
    }
}