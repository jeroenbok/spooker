using System.Collections.Generic;

namespace Spooker.Web.Domain
{
    public class Votes
    {
        private readonly IDictionary<string, int> _votesByParticipant;

        public Votes()
        {
        }

        private Votes(Dictionary<string, int> votesByParticipant)
        {
            _votesByParticipant = votesByParticipant;
        }

        public int this[string participantName]
        {
            get { return _votesByParticipant[participantName]; }
        }

        public Votes Register(Vote vote)
        {
            var newVotes = new Dictionary<string, int>(_votesByParticipant);
            newVotes.Add(vote.ParticipantName, vote.Estimate);
            return new Votes(newVotes);
        }
    }
}