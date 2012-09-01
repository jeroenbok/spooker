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
            Assert.That(round.Status.IsCompleted, Is.False, "round completed");
        }

        [Test]
        public void When_all_participants_have_estimated_then_estimation_round_notifies_all_participants_have_estimated()
        {
            var round = new EstimationRound();
            var estimator = Participant.In(round, "estimator");
            RoundCompletedArgs completed = null;
            round.Completed += (sender, args) => completed = args;

            estimator.Estimate(StoryPoints.Coffee);

            Assert.That(completed, Is.Not.Null, "completed event");
            Assert.That(completed.Status, Is.Not.Null, "completed event status");
            Assert.That(round.Status.IsCompleted, Is.True, "round completed");
        }

        private Estimate NoEstimate(string participantName)
        {
            return new Estimate(participantName, StoryPoints.None);
        }
    }
}
