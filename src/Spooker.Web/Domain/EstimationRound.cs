using System;
using System.Collections.Generic;

namespace Spooker.Web.Domain
{
    public class EstimationRound
    {
        private readonly IList<Participant> _participants = new List<Participant>();
        private Estimates _estimates = new Estimates();

        public IEnumerable<Participant> Partipants
        {
            get { return _participants; }
        }

        public Estimates Estimates
        {
            get { return _estimates; }
        }

        public void Join(Participant participant)
        {
            // TODO Handle dupe participant join attempts
            _participants.Add(participant);
            participant.Voted += RegisterVote;
        }

        private void RegisterVote(object sender, EstimatedArgs args)
        {
            _estimates = _estimates.Register(args.Estimate);
        }
    }

    public class EstimatedArgs : EventArgs
    {
        private readonly Estimate _estimate;

        public EstimatedArgs(Estimate estimate)
        {
            _estimate = estimate;
        }

        public Estimate Estimate
        {
            get { return _estimate; }
        }
    }
}