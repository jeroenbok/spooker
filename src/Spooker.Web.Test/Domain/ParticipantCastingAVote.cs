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
            var castVote = 0;
            var participant = ParticipantInRound(round);
            participant.Voted += (sender, args) => castVote = args.Estimate;

            participant.Vote(5);
            
            Assert.That(castVote, Is.EqualTo(5), "cast vote");
        }

        [Test]
        public void Cannot_cast_vote_when_not_participating_in_votinground()
        {
            var participant = new Participant("name");

            var thrown = Assert.Throws<NotParticipatingInRoundException>(() => participant.Vote(5));

            Assert.That(thrown.Message, Is.EqualTo("Participant [name] is required to participate in a voting round before casting a vote."), "message");
        }

        private static Participant ParticipantInRound(VotingRound round)
        {
            var participant = new Participant("name");
            participant.Participate(round);
            return participant;
        }
    }
}