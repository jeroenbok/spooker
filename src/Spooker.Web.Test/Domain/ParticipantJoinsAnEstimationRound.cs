using NUnit.Framework;
using Spooker.Web.Domain;

namespace Spooker.Web.Test.Domain
{
    [TestFixture]
    public class ParticipantJoinsAnEstimationRound
    {
        [Test]
        public void Named_participant_can_join_estimation_round()
        {
            var participant = new Participant("name");
            var round = new EstimationRound();
            
            participant.Participate(round);

            Assert.That(round.Partipants, Is.EquivalentTo(new[] { participant }), "participants");
        }

        [Test]
        public void Multiple_participants_can_join_the_same_estimation_round()
        {
            var joe = new Participant("joe");
            var jane = new Participant("jane");
            var estimationRound = new EstimationRound();
            
            joe.Participate(estimationRound);
            jane.Participate(estimationRound);

            Assert.That(estimationRound.Partipants, Is.EquivalentTo(new[] { joe, jane }), "participants");
        }
        
        [Test]
        public void Cannot_join_estimation_round_when_same_name_is_already_participating()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void Can_handle_reading_and_writing_simultaneously_estimation_rounds_participant_list()
        {
            Assert.Inconclusive();
        }
    }
}