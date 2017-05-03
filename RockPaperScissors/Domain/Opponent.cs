using System;

namespace RockPaperScissors.Domain
{
    public class Opponent : Player
    {
        public Opponent(OpponentType opponentType)
        {
            OpponentType = opponentType;
        }

        public OpponentType OpponentType { get; private set; }

        public MoveChoice? PreviousMove { get; set; }
    }
}