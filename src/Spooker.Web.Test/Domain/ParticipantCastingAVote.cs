using NUnit.Framework;
using Spooker.Web.Domain;

namespace Spooker.Web.Test.Domain
{
    [TestFixture]
    public class ParticipantCastingAVote
    {
        [Test]
        public void Can_cast_vote_when_participating_in_votinground()
        {
            var round = new VotingRound();
            Vote castedVote = null;
            var participant = ParticipantInRound(round, "name");
            participant.Voted += (sender, args) => castedVote = args.Vote;

            participant.Cast(5);
            
            Assert.That(castedVote.ParticipantName, Is.EqualTo("name"), "participant");
            Assert.That(castedVote.Estimate, Is.EqualTo(5), "estimate");
        }

        [Test]
        public void Cannot_cast_vote_when_not_participating_in_votinground()
        {
            var participant = new Participant("name");

            var thrown = Assert.Throws<NotParticipatingInRoundException>(() => participant.Cast(5));

            Assert.That(thrown.Message, Is.EqualTo("Participant [name] is required to participate in a voting round before casting a vote."), "message");
        }

        [Test]
        public void Vote_cast_by_participant_is_registered_in_voting_round()
        {
            var round = new VotingRound();
            var participant = ParticipantInRound(round);
            
            participant.Cast(8);

            Assert.That(round.Votes[participant.Name], Is.EqualTo(8));
        }

        private Participant ParticipantInRound(VotingRound round, string name = "anonymous")
        {
            var participant = new Participant(name);
            participant.Participate(round);
            return participant;
        }
    }
}