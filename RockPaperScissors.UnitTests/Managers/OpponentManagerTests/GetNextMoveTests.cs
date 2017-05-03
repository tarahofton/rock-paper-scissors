using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using RockPaperScissors.Core;
using RockPaperScissors.Domain;
using RockPaperScissors.Managers;

namespace RockPaperScissors.UnitTests.Managers.OpponentManagerTests
{
    [TestFixture]
    public class GetNextMoveTests
    {
        private Mock<IRulesManager> _mockRulesManager;
        private OpponentManager _opponentManager;

        [SetUp]
        public void Setup()
        {
            _mockRulesManager = new Mock<IRulesManager>();
            _opponentManager = new OpponentManager(_mockRulesManager.Object);
        }

        [Test]
        public void DoesNot_GetRules_WhenRandom()
        {
            _opponentManager.GetNextMove(OpponentType.Random, null);

            _mockRulesManager.Verify(_ => _.GetRules(), Times.Never);
        }

        [Test]
        public void DoesNot_GetRules_WhenTactical_And_NullPreviousMove()
        {
            _opponentManager.GetNextMove(OpponentType.Tactical, null);

            _mockRulesManager.Verify(_ => _.GetRules(), Times.Never);
        }

        [Test]
        public void GetsRules_When_Tactical_And_PreviousMove()
        {
            _mockRulesManager.Setup(_ => _.GetRules()).Returns(new List<Rule> { new Rule { MoveChoice = MoveChoice.Rock, BeatsMoveChoice = MoveChoice.Paper } }).Verifiable();

            _opponentManager.GetNextMove(OpponentType.Tactical, MoveChoice.Paper);

            _mockRulesManager.Verify();
        }

        [Test]
        public void Throws_Exception_WhenRuleNotFound()
        {
            Action action = () => _opponentManager.GetNextMove(OpponentType.Tactical, MoveChoice.Paper);

            action.ShouldThrow<RulesException>().And.Message.Should().Be("Rule not found where BeatsMoveChoice is Paper");
        }
    }
}
