using System;
using System.Collections.Generic;
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
    public class PlayGameTests
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
        public void Creates_Rules()
        {
            var rules = new List<Rule> { new Rule { MoveChoice = MoveChoice.Paper, BeatsMoveChoice = MoveChoice.Rock } };
            _mockRulesManager.Setup(_ => _.GetRules()).Returns(rules).Verifiable();
            var match = Builder<Domain.Match>.CreateNew().With(_ => _.Opponent = new Opponent(OpponentType.Random)).Build();

            _matchManager.PlayGame(match, MoveChoice.Paper);

            _mockRulesManager.Verify();
        }

        [Test]
        public void Returns_Draw_WhenSameMoves()
        {
            var rules = new List<Rule> { new Rule { MoveChoice = MoveChoice.Paper, BeatsMoveChoice = MoveChoice.Rock } };
            _mockRulesManager.Setup(_ => _.GetRules()).Returns(rules);
            _mockOpponantManager.Setup(_ => _.GetNextMove(OpponentType.Random, null)).Returns(MoveChoice.Paper);

            var match = Builder<Domain.Match>.CreateNew()
                .With(_ => _.Opponent = new Opponent(OpponentType.Random))
                .Build();

            var game = _matchManager.PlayGame(match, MoveChoice.Paper);

            game.Result.Should().Be(Result.Draw);
        }

        [Test]
        public void Throws_Exception_WhenRuleNotFound()
        {
            var match = Builder<Domain.Match>.CreateNew().With(_ => _.Opponent = new Opponent(OpponentType.Tactical)).Build();
            _mockOpponantManager.Setup(_ => _.GetNextMove(It.IsAny<OpponentType>(), It.IsAny<MoveChoice?>())).Returns(MoveChoice.Paper);

            Action action = () => _matchManager.PlayGame(match, MoveChoice.Rock);

            action.ShouldThrow<RulesException>().And.Message.Should().Be("Rule not found where MoveChoice is Rock");
        }

        [Test]
        public void Returns_Win_WhenChoice_BeatsOpponent()
        {
            var rules = new List<Rule> { new Rule { MoveChoice = MoveChoice.Paper, BeatsMoveChoice = MoveChoice.Rock } };
            _mockRulesManager.Setup(_ => _.GetRules()).Returns(rules);
            _mockOpponantManager.Setup(_ => _.GetNextMove(OpponentType.Random, null)).Returns(MoveChoice.Rock);

            var match = Builder<Domain.Match>.CreateNew()
                .With(_ => _.Opponent = new Opponent(OpponentType.Random))
                .Build();

            var game = _matchManager.PlayGame(match, MoveChoice.Paper);

            game.Result.Should().Be(Result.Win);
        }

        [Test]
        public void Returns_Lose_WhenOpponent_BeatsChoice()
        {
            var rules = new List<Rule> { new Rule { MoveChoice = MoveChoice.Paper, BeatsMoveChoice = MoveChoice.Rock } };
            _mockRulesManager.Setup(_ => _.GetRules()).Returns(rules);
            _mockOpponantManager.Setup(_ => _.GetNextMove(OpponentType.Random, null)).Returns(MoveChoice.Scissors);

            var match = Builder<Domain.Match>.CreateNew()
                .With(_ => _.Opponent = new Opponent(OpponentType.Random))
                .Build();

            var game = _matchManager.PlayGame(match, MoveChoice.Paper);

            game.Result.Should().Be(Result.Lose);
        }
    }
}
