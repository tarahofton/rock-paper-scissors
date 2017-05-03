using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using RockPaperScissors.Domain;
using RockPaperScissors.Managers;

namespace RockPaperScissors.UnitTests.Managers.RulesManagerTests
{
    [TestFixture]
    public class GetRulesTests
    {
        private RulesManager _rulesManager;

        [SetUp]
        public void Setup()
        {
            _rulesManager = new RulesManager();
        }

        [Test]
        public void Rock_Beats_Scissors()
        {
            var rules = _rulesManager.GetRules();

            var rockRule = rules.FirstOrDefault(_ => _.MoveChoice == MoveChoice.Rock);

            rockRule.BeatsMoveChoice.Should().Be(MoveChoice.Scissors);
        }

        [Test]
        public void Scissors_Beats_Paper()
        {
            var rules = _rulesManager.GetRules();

            var rockRule = rules.FirstOrDefault(_ => _.MoveChoice == MoveChoice.Scissors);

            rockRule.BeatsMoveChoice.Should().Be(MoveChoice.Paper);
        }

        [Test]
        public void Paper_Beats_Rock()
        {
            var rules = _rulesManager.GetRules();

            var rockRule = rules.FirstOrDefault(_ => _.MoveChoice == MoveChoice.Paper);

            rockRule.BeatsMoveChoice.Should().Be(MoveChoice.Rock);
        }
    }
}
