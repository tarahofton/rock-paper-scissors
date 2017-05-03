using System;
using System.Linq;
using RockPaperScissors.Core;
using RockPaperScissors.Domain;

namespace RockPaperScissors.Managers
{
    public class OpponentManager : IOpponentManager
    {
        private readonly IRulesManager _rulesManager;

        public OpponentManager(IRulesManager rulesManager)
        {
            _rulesManager = rulesManager;
        }

        public MoveChoice GetNextMove(OpponentType opponentType, MoveChoice? previousMove)
        {
            var values = Enum.GetValues(typeof(MoveChoice));
            if (opponentType == OpponentType.Random || !previousMove.HasValue)
            {
                var random = new Random();
                return (MoveChoice)values.GetValue(random.Next(values.Length));
            }

            var rules = _rulesManager.GetRules();

            var beatenRule = rules.FirstOrDefault(_ => _.BeatsMoveChoice == previousMove.Value);

            if (beatenRule == null)
            {
                throw new RulesException($"Rule not found where BeatsMoveChoice is {previousMove.Value}");
            }

            return beatenRule.MoveChoice;
        }
    }
}