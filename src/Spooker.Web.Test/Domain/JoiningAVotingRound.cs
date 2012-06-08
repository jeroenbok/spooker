using NUnit.Framework;
using Spooker.Web.Domain;

namespace Spooker.Web.Test.Domain
{
    [TestFixture]
    public class JoiningAVotingRound
    {
        [Test]
        public void When_participant_enters_name_on_joining_then_he_participates_in_the_voting_round()
        {
            var participant = new Participant("name");
            var votingRound = new VotingRound();
            
            participant.Participate(votingRound);

            Assert.That(votingRound.Partipants, Is.EquivalentTo(new[] { participant }), "participants");
        }

        [Test]
        public void Multiple_participants_can_join_the_same_voting_round()
        {
            var joe = new Participant("joe");
            var jane = new Participant("jane");
            var votingRound = new VotingRound();
            
            joe.Participate(votingRound);
            jane.Participate(votingRound);

            Assert.That(votingRound.Partipants, Is.EquivalentTo(new[] { joe, jane }), "participants");
        }
        
        [Test]
        public void Participant_cannot_join_voting_round_when_someone_with_same_name_is_already_participating()
        {
            Assert.Inconclusive();
        }

        [Test]
        public void Can_handle_reading_and_writing_simultaneously_the_votingrounds_participant_list()
        {
            Assert.Inconclusive();
        }
    }
}