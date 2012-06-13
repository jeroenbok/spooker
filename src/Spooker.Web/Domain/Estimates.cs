using System.Collections.Generic;

namespace Spooker.Web.Domain
{
    public class Estimates
    {
        private readonly IDictionary<string, StoryPoints> _estimatesByParticipant;

        public Estimates() : this(new Dictionary<string, StoryPoints>())
        {
        }

        private Estimates(Dictionary<string, StoryPoints> estimatesByParticipant)
        {
            _estimatesByParticipant = estimatesByParticipant;
        }

        public StoryPoints this[string participantName]
        {
            get
            {
                if (!HasEstimateFor(participantName))
                    throw new ParticipantHasNotYetEstimatedException(participantName);
                return _estimatesByParticipant[participantName];
            }
        }

        public int Count
        {
            get { return _estimatesByParticipant.Count; }
        }

        public Estimates Register(Estimate estimate)
        {
            var newEstimatesByParticipant = new Dictionary<string, StoryPoints>(_estimatesByParticipant);
            newEstimatesByParticipant[estimate.ParticipantName] = estimate.StoryPoints;
                               
            return new Estimates(newEstimatesByParticipant);
        }

        public bool HasEstimateFor(string participantName)
        {
            return _estimatesByParticipant.ContainsKey(participantName);
        }
    }
}