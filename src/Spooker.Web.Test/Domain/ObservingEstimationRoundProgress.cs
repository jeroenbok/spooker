using NUnit.Framework;
using Spooker.Web.Domain;

namespace Spooker.Web.Test.Domain
{
    [TestFixture]
    public class ObservingEstimationRoundProgress
    {
        [Test]
        public void When_no_participant_has_estimated_then_round_status_has_no_estimates()
        {
            var round = new EstimationRound();
            Participant.In(round, "nonEstimator");

            Assert.That(round.Status, Is.EquivalentTo(new[] { NoEstimate("nonEstimator") }), "estimates");
        }

        [Test]
        public void When_single_participant_has_estimated_then_round_status_has_single_estimate()
        {
            var round = new EstimationRound();
            var estimator = Participant.In(round, "estimator");

            estimator.Estimate(StoryPoints.Three);

            Assert.That(round.Status, Is.EquivalentTo(new[] { new Estimate("estimator", StoryPoints.Three) }), "estimates");
        }

        [Test]
        public void When_one_participant_has_estimated_and_another_has_not_then_round_status_has_two_estimates()
        {
            var round = new EstimationRound();
            var estimator = Participant.In(round, "estimator");
            Participant.In(round, "nonEstimator");

            estimator.Estimate(StoryPoints.Three);

            Assert.That(round.Status, Is.EquivalentTo(new[] { new Estimate("estimator", StoryPoints.Three), NoEstimate("nonEstimator") }), "estimates");
        }

        [Test]
        public void When_one_participant_has_estimated_and_another_has_not_then_round_status_has_1_estimate_and_2_participants()
        {
            var round = new EstimationRound();
            var estimator = Participant.In(round, "estimator");
            Participant.In(round, "nonEstimator");
            estimator.Estimate(StoryPoints.Three);

            Assert.That(round.Status.EstimateCount, Is.EqualTo(1), "number of estimates");
            Assert.That(round.Status.ParticipantCount, Is.EqualTo(2), "number of participants");
        }

        private Estimate NoEstimate(string participantName)
        {
            return new Estimate(participantName, StoryPoints.None);
        }
    }
}
