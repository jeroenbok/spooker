using System;

namespace Spooker.Web.Domain
{
    public class ParticipantHasNotYetEstimatedException : Exception
    {
        public ParticipantHasNotYetEstimatedException(string participantName) : base(string.Format("Participant [{0}] has not estimated yet...", participantName))
        {
        }
    }
}