using System.Linq;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using RockPaperScissors.Core;
using RockPaperScissors.Domain;
using RockPaperScissors.Managers;

namespace RockPaperScissors.UnitTests.Managers.MatchManagerTests
{
    [TestFixture]
    public class IsGameOverTests
    {
        private Mock<IMatchConfiguration> _mockMatchConfiguration;
        private Mock<IRulesManager> _mockRulesManager;
        private Mock<IOpponentManager> _mockOpponantManager;
        private MatchManager _matchManager;

        [SetUp]
        public void Setup()
        {
            _mockMatchConfiguration = new Mock<IMatchConfiguration>();
            _mockRulesManager = new Mock<IRulesManager>();
            _mockOpponantManager = new Mock<IOpponentManager>();
            _matchManager = new MatchManager(_mockMatchConfiguration.Object, _mockRulesManager.Object, _mockOpponantManager.Object);
        }

        [Test]
        public void Get_MatchLength_FromConfiguration()
        {
            var match = Builder<Domain.Match>.CreateNew()
                .With(_ => _.Games = new Game[0]).Build();
            _mockMatchConfiguration.Setup(_ => _.MatchLength).Returns(5).Verifiable();

            _matchManager.IsGameOver(match);

            _mockMatchConfiguration.Verify();
        }

        [Test]
        public void Returns_Null_When_NotEnoughGamesPlayed()
        {
            var match = Builder<Domain.Match>.CreateNew().With(_ => _.Games = new Game[0]).Build();
            _mockMatchConfiguration.Setup(_ => _.MatchLength).Returns(5);

            var isGameOver = _matchManager.IsGameOver(match);

            isGameOver.Should().BeNull();
        }

        [Test]
        public void Returns_Winner_When_WonEnoughGames()
        {
            var games = Builder<Game>.CreateListOfSize(2).All().With(_ => _.Result = Result.Win).Build();
            var match = Builder<Domain.Match>.CreateNew().With(_ => _.Games = games.ToArray()).Build();
            _mockMatchConfiguration.Setup(_ => _.MatchLength).Returns(3);

            var isGameOver = _matchManager.IsGameOver(match);

            isGameOver.Result.Should().Be(Result.Win);
        }

        [Test]
        public void Returns_Lose_When_NotWonEnoughGames()
        {
            var games = Builder<Game>.CreateListOfSize(2).All().With(_ => _.Result = Result.Lose).Build();
            var match = Builder<Domain.Match>.CreateNew().With(_ => _.Games = games.ToArray()).Build();
            _mockMatchConfiguration.Setup(_ => _.MatchLength).Returns(3);

            var isGameOver = _matchManager.IsGameOver(match);

            isGameOver.Result.Should().Be(Result.Lose);
        }

        [Test]
        public void Returns_Draw_When_DrawEnoughGames()
        {
            var games = Builder<Game>.CreateListOfSize(2).All().With(_ => _.Result = Result.Draw).Build();
            var match = Builder<Domain.Match>.CreateNew().With(_ => _.Games = games.ToArray()).Build();
            _mockMatchConfiguration.Setup(_ => _.MatchLength).Returns(3);

            var isGameOver = _matchManager.IsGameOver(match);

            isGameOver.Result.Should().Be(Result.Draw);
        }

        [Test]
        public void Returns_Draw_When_OneOfEach()
        {
            var wonGame = new Game { Result = Result.Win };
            var loseGame = new Game { Result = Result.Lose };
            var drawGame = new Game { Result = Result.Draw };
            var games = new[] { wonGame, loseGame, drawGame };
            var match = Builder<Domain.Match>.CreateNew().With(_ => _.Games = games).Build();
            _mockMatchConfiguration.Setup(_ => _.MatchLength).Returns(3);

            var isGameOver = _matchManager.IsGameOver(match);

            isGameOver.Result.Should().Be(Result.Draw);
        }

        [Test]
        public void Returns_Draw_When_DrawGames()
        {
            var wonGame1 = new Game { Result = Result.Win };
            var wonGame2 = new Game { Result = Result.Win };
            var loseGame1 = new Game { Result = Result.Lose };
            var loseGame2 = new Game { Result = Result.Lose };
            var drawGame = new Game { Result = Result.Draw };
            var games = new[] { wonGame1, wonGame2, loseGame1, loseGame2, drawGame };
            var match = Builder<Domain.Match>.CreateNew().With(_ => _.Games = games).Build();
            _mockMatchConfiguration.Setup(_ => _.MatchLength).Returns(5);

            var isGameOver = _matchManager.IsGameOver(match);

            isGameOver.Result.Should().Be(Result.Draw);
        }
    }
}
