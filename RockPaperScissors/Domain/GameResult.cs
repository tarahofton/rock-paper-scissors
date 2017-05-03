namespace RockPaperScissors.Domain
{
    public class GameResult
    {
        public Result Result { get; set; }

        public MoveChoice YourMove { get; set; }

        public MoveChoice OpponentMove { get; set; }
    }
}