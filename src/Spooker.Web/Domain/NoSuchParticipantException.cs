using System;

namespace Spooker.Web.Domain
{
    public class NoSuchParticipantException : Exception
    {
        public NoSuchParticipantException(Guid participantId) : base(string.Format("No participant with id [{0}] was found", participantId))
        {
        }
    }
}