using System.Collections.Generic;

namespace Spooker.Web.Domain
{
    public class VotingRound
    {
        private readonly IList<Participant> _participants = new List<Participant>();

        public IEnumerable<Participant> Partipants
        {
            get { return _participants; }
        }

        public void Join(Participant participant)
        {
            _participants.Add(participant);
        }
    }
}