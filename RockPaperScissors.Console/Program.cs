using System;
using System.Linq;
using Autofac;
using RockPaperScissors.Core;
using RockPaperScissors.Domain;
using RockPaperScissors.Managers;

namespace RockPaperScissors.GameConsole
{
    public class Program
    {
        private static IContainer Container { get; set; }
        private static Match _match;

        public static void Main()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<MatchManager>().As<IMatchManager>();
            builder.RegisterType<OpponentManager>().As<IOpponentManager>();
            builder.RegisterType<RulesManager>().As<IRulesManager>();
            builder.RegisterType<MatchConfiguration>().As<IMatchConfiguration>();
            Container = builder.Build();

            Printer.PrintMenu();

            var option = Console.ReadLine();

            Console.Clear();

            switch (option)
            {
                case "1":
                    Play(OpponentType.Random);
                    break;
                case "2":
                    Play(OpponentType.Tactical);
                    break;
                default:
                    return;
            }
        }

        private static void Play(OpponentType opponentType)
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var matchManager = scope.Resolve<IMatchManager>();
                _match = matchManager.Load(opponentType);
                for (var i = 0; i < _match.Games.Length; i++)
                {
                    TakeTurn(matchManager, i);
                    var matchResult = matchManager.IsGameOver(_match);
                    if (matchResult == null)
                    {
                        continue;
                    }

                    Printer.PrintMatchResult(matchResult);
                    break;
                }
            }

            Printer.PrintContinue();
        }

        private static void TakeTurn(IMatchManager matchManager, int turn)
        {
            Printer.PrintMoveChoice();
            var option = Console.ReadLine();
            if (option != "1" && option != "2" && option != "3")
            {
                return;
            }

            var moveChoice = MyMoveChoice(option);
            var gameResult = matchManager.PlayGame(_match, moveChoice);
            _match.Games[turn].Result = gameResult.Result;
            _match.Opponent.PreviousMove = gameResult.OpponentMove;

            Printer.PrintGameResult(gameResult);
        }

        private static MoveChoice MyMoveChoice(string option)
        {
            switch (option)
            {
                case "1":
                    return MoveChoice.Rock;
                case "2":
                    return MoveChoice.Paper;
                default:
                    return MoveChoice.Scissors;
            }
        }
    }
}
