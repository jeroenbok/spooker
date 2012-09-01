using NUnit.Framework;
using Spooker.Web.Domain;

namespace Spooker.Web.Test.Domain
{
    [TestFixture]
    public class RoundKeeperTest
    {
        [Test]
        public void Fresh_roundkeeper_has_no_active_round()
        {
            var roundKeeper = new RoundKeeper();
            Assert.That(roundKeeper.HasActiveRound, Is.False, "roundkeeper has active round");
        }

        [Test]
        public void Can_enroll_new_participant_when_there_is_no_active_round()
        {
            var roundKeeper = new RoundKeeper();
            var participant = new Participant("NEW");

            roundKeeper.Enroll(participant);
            
            Assert.That(roundKeeper.HasActiveRound, "roundkeeper has active round");
            Assert.That(roundKeeper.ActiveRound.Status.ParticipantCount, Is.EqualTo(1), "round participant count");
        }

        [Test]
        public void Can_enroll_new_participant_when_there_is_an_active_non_completed_round()
        {
            var roundKeeper = new RoundKeeper();
            var participant1 = new Participant("ONE");
            var participant2 = new Participant("TWO");
            roundKeeper.Enroll(participant1);
            
            roundKeeper.Enroll(participant2);
            
            Assert.That(roundKeeper.ActiveRound.Status.ParticipantCount, Is.EqualTo(2), "round participant count");
        }

        [Test]
        public void Starts_new_round_when_enrolling_new_participant_when_active_roud_is_completed()
        {
            var roundKeeper = new RoundKeeper();
            var currentRoundParticipant = new Participant("ACTIVE");
            var newRoundInitiatingParticipant = new Participant("NEW");
            roundKeeper.Enroll(currentRoundParticipant);
            currentRoundParticipant.Estimate(StoryPoints.One);

            roundKeeper.Enroll(newRoundInitiatingParticipant);

            Assert.That(roundKeeper.ActiveRound.Status.ParticipantCount, Is.EqualTo(2), "participant count");
            Assert.That(roundKeeper.ActiveRound.Status.EstimateCount, Is.EqualTo(0), "estimate count");
        }
    }
}