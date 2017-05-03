using System;
using System.Linq;
using RockPaperScissors.Domain;

namespace RockPaperScissors.GameConsole
{
    public static class Printer
    {
        public static void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine("Playing best of 3 games");
            Console.WriteLine("Choose your opponent:");
            Console.WriteLine(" ");
            Console.WriteLine("1 - Play Random Computer Player");
            Console.WriteLine("2 - Play Tactical Computer Player");
            Console.WriteLine("0 - Exit");
        }

        public static void PrintMoveChoice()
        {
            Console.WriteLine("Choose a move:");
            Console.WriteLine(" ");
            Console.WriteLine("1 - Rock");
            Console.WriteLine("2 - Paper");
            Console.WriteLine("3 - Scissors");
        }

        public static void PrintContinue()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }

        public static void PrintGameResult(GameResult gameResult)
        {
            if (gameResult.Result == Result.Win)
            {
                Console.WriteLine($"You chose {gameResult.YourMove}, Computer Player chose {gameResult.OpponentMove}, you win.");
            }

            if (gameResult.Result == Result.Draw)
            {
                Console.WriteLine($"You chose {gameResult.YourMove}, Computer Player chose {gameResult.OpponentMove}, it was a draw.");
            }

            if (gameResult.Result == Result.Lose)
            {
                Console.WriteLine($"You chose {gameResult.YourMove}, Computer Player chose {gameResult.OpponentMove}, you lost.");
            }
        }

        public static void PrintMatchResult(MatchResult matchResult)
        {
            Console.WriteLine(" ");
            var numberOfGamesPlayed = matchResult.Match.Games.Count(_ => _.Result != null);
            for (var i = 0; i < numberOfGamesPlayed; i++)
            {
                Console.WriteLine($"Game {i+1} you {matchResult.Match.Games[i].Result} ");
            }

            Console.WriteLine(" ");
            if (matchResult.Result == Result.Win)
            {
                Console.WriteLine("You win the match");
                return;
            }

            if (matchResult.Result == Result.Lose)
            {
                Console.WriteLine("You lost the match");
                return;
            }

            Console.WriteLine("The match was a draw");
        }

    }
}