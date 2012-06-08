using System;

namespace Spooker.Web.Domain
{
    public class NotParticipatingInRoundException : Exception
    {
        public NotParticipatingInRoundException(string name) : base(string.Format("Participant [{0}] is required to participate in a voting round before casting a vote.", name))
        {
        }
    }
}