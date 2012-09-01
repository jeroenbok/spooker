using System;

namespace Spooker.Web.Domain
{
    public class ParticipantAlreadyParticpatesInRoundException : Exception
    {
        public ParticipantAlreadyParticpatesInRoundException(Guid id, string name) : base(string.Format("Participant with id [{0}] named [{1}] is already a participant in the estimation round.", id, name))
        {
        }
    }
}