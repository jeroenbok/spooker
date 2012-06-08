using System;
using System.Collections.Generic;

namespace Spooker.Web.Domain
{
    public class Estimates
    {
        private readonly IDictionary<string, int> _estimatesByParticipant;

        public Estimates() : this (new Dictionary<string, int>())
        {
        }

        private Estimates(Dictionary<string, int> estimatesByParticipant)
        {
            _estimatesByParticipant = estimatesByParticipant;
        }

        public int this[string participantName]
        {
            get
            {
                if (!_estimatesByParticipant.ContainsKey(participantName))
                    throw new ParticipantHasNotYetEstimatedException(participantName);
                return _estimatesByParticipant[participantName];
            }
        }

        public Estimates Register(Estimate estimate)
        {
            var newEstimatesByParticipant = new Dictionary<string, int>(_estimatesByParticipant)
                               {{estimate.ParticipantName, estimate.StoryPoints}};
            return new Estimates(newEstimatesByParticipant);
        }
    }

    public class ParticipantHasNotYetEstimatedException : Exception
    {
        public ParticipantHasNotYetEstimatedException(string participantName) : base(string.Format("Participant [{0}] has not estimated yet...", participantName))
        {
        }
    }
}