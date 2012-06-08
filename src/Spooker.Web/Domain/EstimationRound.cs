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

        public EstimationStatus Status
        {
            get
            {
                var estimations = new List<Estimate>();
                foreach (var participant in _participants)
                {
                    var storyPoints = _estimates.HasEstimateFor(participant.Name)
                                                  ? _estimates[participant.Name]
                                                  : StoryPoints.None;
                    estimations.Add(new Estimate(participant.Name, storyPoints));
                }
                return new EstimationStatus(estimations);
            }
        }

        public void Join(Participant participant)
        {
            // TODO Handle dupe participant join attempts
            _participants.Add(participant);
            participant.Estimated += RegisterEstimate;
        }

        private void RegisterEstimate(object sender, EstimatedArgs args)
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