using NUnit.Framework;
using Spooker.Web.Domain;

namespace Spooker.Web.Test.Domain
{
    [TestFixture]
    public class When_participant_estimates_in_a_newly_joined_round : GivenWhenThenFixture
    {
        readonly EstimationRound _roundLeft = new EstimationRound();
        readonly EstimationRound _roundJoined = new EstimationRound();
        readonly Participant _participant = new Participant("JOE");

        protected override void Given()
        {
            _participant.Participate(_roundLeft);
            _participant.Participate(_roundJoined);
        }

        protected override void When()
        {
            _participant.Estimate(StoryPoints.Two);
        }

        [Then]
        public void Previous_round_does_not_receive_participants_estimate()
        {
            Assert.That(_roundLeft.Status.EstimateCount, Is.EqualTo(0), "round left received no estimate");
        }

        [Then]
        public void Active_round_receives_participants_estimate()
        {
            Assert.That(_roundJoined.Status.EstimateCount, Is.EqualTo(1), "round joined received estimate");
        }
    }
}