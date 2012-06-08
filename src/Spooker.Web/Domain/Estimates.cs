using System;
using System.Collections.Generic;

namespace Spooker.Web.Domain
{
    public class Estimates
    {
        private readonly IDictionary<string, int> _votesByParticipant;

        public Estimates() : this (new Dictionary<string, int>())
        {
        }

        private Estimates(Dictionary<string, int> votesByParticipant)
        {
            _votesByParticipant = votesByParticipant;
        }

        public int this[string participantName]
        {
            get
            {
                if (!_votesByParticipant.ContainsKey(participantName))
                    throw new ParticipantHasNotYetEstimatedException(participantName);
                return _votesByParticipant[participantName];
            }
        }

        public Estimates Register(Estimate estimate)
        {
            var newVotes = new Dictionary<string, int>(_votesByParticipant)
                               {{estimate.ParticipantName, estimate.StoryPoints}};
            return new Estimates(newVotes);
        }
    }

    public class ParticipantHasNotYetEstimatedException : Exception
    {
        public ParticipantHasNotYetEstimatedException(string participantName) : base(string.Format("Participant [{0}] has not estimated yet...", participantName))
        {
        }
    }
}