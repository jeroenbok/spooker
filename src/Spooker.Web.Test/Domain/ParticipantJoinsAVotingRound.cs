using NUnit.Framework;
using Spooker.Web.Domain;

namespace Spooker.Web.Test.Domain
{
    [TestFixture]
    public class ParticipantJoinsAVotingRound
    {
        [Test]
        public void Named_participant_can_join_voting_round()
        {
            var participant = new Participant("name");
            var votingRound = new EstimationRound();
            
            participant.Participate(votingRound);

            Assert.That(votingRound.Partipants, Is.EquivalentTo(new[] { participant }), "participants");
        }

        [Test]
        public void Multiple_participants_can_join_the_same_voting_round()
        {
            var joe = new Participant("joe");
            var jane = new Participant("jane");
            var votingRound = new EstimationRound();
            
            joe.Participate(votingRound);
            jane.Participate(votingRound);

            Assert.That(votingRound.Partipants, Is.EquivalentTo(new[] { joe, jane }), "participants");
        }
        
        [Test]
        public void Cannot_join_voting_round_when_same_name_is_already_participating()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void Can_handle_reading_and_writing_simultaneously_votingrounds_participant_list()
        {
            Assert.Inconclusive();
        }
    }
}