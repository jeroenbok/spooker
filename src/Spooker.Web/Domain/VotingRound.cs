using System;
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

    public class VotedArgs : EventArgs
    {
        private readonly int _estimate;

        public VotedArgs(int estimate)
        {
            _estimate = estimate;
        }

        public int Estimate
        {
            get { return _estimate; }
        }
    }
}