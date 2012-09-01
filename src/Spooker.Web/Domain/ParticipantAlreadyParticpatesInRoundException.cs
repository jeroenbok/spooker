using System;

namespace Spooker.Web.Domain
{
    public class ParticipantAlreadyParticpatesInRoundException : Exception
    {
        public ParticipantAlreadyParticpatesInRoundException(string name)
            : base(string.Format("Participant [{0}] is already a participant in the estimation round, please select a different name.", name))
        {
        }
    }
}