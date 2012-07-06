using System;

namespace Spooker.Web.Domain
{
    public class AlreadyParticipatesInRoundException : Exception
    {
        public AlreadyParticipatesInRoundException(string name)
            : base(string.Format("Participant [{0}] is already a participant in the estimation round, please select a different name.", name))
        {
        }
    }
}