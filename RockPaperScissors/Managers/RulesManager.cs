using System.Collections.Generic;
using RockPaperScissors.Core;
using RockPaperScissors.Domain;

namespace RockPaperScissors.Managers
{
    public class RulesManager : IRulesManager
    {
        public IEnumerable<Rule> GetRules()
        {
            return new List<Rule>
            {
                new Rule {MoveChoice = MoveChoice.Rock, BeatsMoveChoice = MoveChoice.Scissors},
                new Rule {MoveChoice = MoveChoice.Scissors, BeatsMoveChoice = MoveChoice.Paper},
                new Rule {MoveChoice = MoveChoice.Paper, BeatsMoveChoice = MoveChoice.Rock}
            };
        }
    }
}