using System.Collections.Generic;

namespace RockPaperScissors.Domain
{
    public class Match
    {
        public Player Player { get; set; }

        public Opponent Opponent { get; set; }

        public Game[] Games { get; set; }
    }
}