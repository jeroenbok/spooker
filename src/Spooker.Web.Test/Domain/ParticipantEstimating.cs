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
            var participant = ParticipantIn(round, "name");
            participant.Estimated += (sender, args) => estimate = args.Estimate;

            participant.Estimate(5);
            
            Assert.That(estimate.ParticipantName, Is.EqualTo("name"), "participant");
            Assert.That(estimate.StoryPoints, Is.EqualTo(5), "storypoints");
        }

        [Test]
        public void Cannot_estimate_when_not_participating_in_estimation_round()
        {
            var participant = new Participant("name");

            var thrown = Assert.Throws<NotParticipatingInRoundException>(() => participant.Estimate(5));

            Assert.That(thrown.Message, Is.EqualTo("Participant [name] is required to participate in a estimation round before estimating."), "message");
        }

        [Test]
        public void Estimate_by_participant_is_registered_in_estimation_round()
        {
            var round = new EstimationRound();
            var participant = ParticipantIn(round);
            
            participant.Estimate(8);

            Assert.That(round.Estimates[participant.Name], Is.EqualTo(8));
        }

        private Participant ParticipantIn(EstimationRound round, string name = "anonymous")
        {
            var participant = new Participant(name);
            participant.Participate(round);
            return participant;
        }
    }
}