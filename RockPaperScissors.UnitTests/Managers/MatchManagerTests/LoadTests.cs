using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using RockPaperScissors.Core;
using RockPaperScissors.Domain;
using RockPaperScissors.Managers;

namespace RockPaperScissors.UnitTests.Managers.MatchManagerTests
{
    [TestFixture]
    public class LoadTests
    {
        private Mock<IMatchConfiguration> _mockMatchConfiguration;
        private MatchManager _matchManager;

        [SetUp]
        public void Setup()
        {
            _mockMatchConfiguration = new Mock<IMatchConfiguration>();
            _matchManager = new MatchManager(_mockMatchConfiguration.Object, null, null);
        }

        [Test]
        public void CreatesMatch_WithPlayers()
        {
            var match = _matchManager.Load(OpponentType.Random);

            match.Player.Should().NotBeNull();
            match.Opponent.Should().NotBeNull();
        }

        [TestCaseSource(nameof(OpponentTypes))]
        public void CreatesMatch_WithCorrectOpponentType(OpponentType opponentType)
        {
            var match = _matchManager.Load(opponentType);

            match.Opponent.OpponentType.Should().Be(opponentType);
        }

        [Test]
        public void Creates_CorrectNumber_Games()
        {
            const int expectedNumberOfGames = 5;
            _mockMatchConfiguration.Setup(_ => _.MatchLength).Returns(expectedNumberOfGames);

            var match = _matchManager.Load(OpponentType.Random);

            match.Games.ToList().Count.Should().Be(expectedNumberOfGames);
        }

        private static Array OpponentTypes()
        {
            return Enum.GetValues(typeof(OpponentType));
        }
    }
}
