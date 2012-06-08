using System;
using System.Collections.Generic;

namespace Spooker.Web.Domain
{
    public class VotingRound
    {
        private readonly IList<Participant> _participants = new List<Participant>();
        private Votes _votes = new Votes();

        public IEnumerable<Participant> Partipants
        {
            get { return _participants; }
        }

        public Votes Votes
        {
            get { return new Votes(); }
        }

        public void Join(Participant participant)
        {
            // TODO Handle dupe participant join attempts
            _participants.Add(participant);
            participant.Voted += RegisterVote;
        }

        private void RegisterVote(object sender, VotedArgs args)
        {
            _votes = _votes.Register(args.Vote);
        }
    }

    public class VotedArgs : EventArgs
    {
        private readonly Vote _vote;

        public VotedArgs(Vote vote)
        {
            _vote = vote;
        }

        public Vote Vote
        {
            get { return _vote; }
        }
    }
}