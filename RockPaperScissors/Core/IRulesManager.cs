using System.Collections.Generic;
using RockPaperScissors.Domain;

namespace RockPaperScissors.Core
{
    public interface IRulesManager
    {
        IEnumerable<Rule> GetRules();
    }
}