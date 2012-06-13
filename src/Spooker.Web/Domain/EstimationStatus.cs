using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Spooker.Web.Domain
{
    public class EstimationStatus : IEnumerable<Estimate>
    {
        private readonly IList<Estimate> _estimationsOfAllParticipants;

        public EstimationStatus(IList<Estimate> estimationsOfAllParticipants)
        {
            _estimationsOfAllParticipants = estimationsOfAllParticipants;
        }

        public IEnumerator<Estimate> GetEnumerator()
        {
            return _estimationsOfAllParticipants.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int EstimateCount
        {
            get { return _estimationsOfAllParticipants.Count(e => e.StoryPoints != StoryPoints.None);  }
        }

        public int ParticipantCount
        {
            get { return _estimationsOfAllParticipants.Count; }
        }
    }
}