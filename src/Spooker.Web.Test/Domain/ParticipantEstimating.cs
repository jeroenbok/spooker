using NUnit.Framework;
using Spooker.Web.Domain;

namespace Spooker.Web.Test.Domain
{
    [TestFixture]
    public class ParticipantEstimating
    {
        [Test]
        public void Can_estimate_when_participating_in_estimation_round()
        {
            var round = new EstimationRound();
            Estimate estimate = null;
            var participant = Participant.In(round, "name");
            participant.Estimated += (sender, args) => estimate = args.Estimate;

            participant.Estimate(StoryPoints.Five);
            
            Assert.That(estimate.ParticipantName, Is.EqualTo("name"), "participant");
            Assert.That(estimate.StoryPoints, Is.EqualTo(StoryPoints.Five), "storypoints");
        }

        [Test]
        public void Cannot_estimate_when_not_participating_in_estimation_round()
        {
            var participant = new Participant("name");

            var thrown = Assert.Throws<NotParticipatingInRoundException>(() => participant.Estimate(StoryPoints.Coffee));

            Assert.That(thrown.Message, Is.EqualTo("Participant [name] is required to participate in a estimation round before estimating."), "message");
        }

        [Test]
        public void Estimate_by_participant_is_registered_in_estimation_round()
        {
            var round = new EstimationRound();
            var participant = Participant.In(round);

            participant.Estimate(StoryPoints.Eight);

            Assert.That(round.Estimates[participant.Name], Is.EqualTo(StoryPoints.Eight));
        }

        [Test]
        public void Estimates_by_multiple_participants_are_registered_in_estimation_round()
        {
            var round = new EstimationRound();
            var joe = Participant.In(round, "joe");
            var jane = Participant.In(round, "jane");

            joe.Estimate(StoryPoints.Three);
            jane.Estimate(StoryPoints.Five);

            Assert.That(round.Estimates[joe.Name], Is.EqualTo(StoryPoints.Three));
            Assert.That(round.Estimates[jane.Name], Is.EqualTo(StoryPoints.Five));
        }

        [Test]
        public void When_participant_reestimates_then_previous_estimate_is_overwritten()
        {
            var round = new EstimationRound();
            var joe = Participant.In(round, "joe");
            Participant.In(round, "jane");
            
            joe.Estimate(StoryPoints.Zero);
            joe.Estimate(StoryPoints.One);

            Assert.That(round.Estimates[joe.Name], Is.EqualTo(StoryPoints.One), "estimate of joe");
        }

        [Test]
        public void When_all_participants_have_estimated_then_estimate_cannot_be_changed()
        {
            Assert.Inconclusive();
        }
    }
}