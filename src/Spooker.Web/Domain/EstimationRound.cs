using System.Collections.Generic;
using Spooker.Web.Infrastructure.Extensions;

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
                _participants.ForEach(p =>
                                          {
                                              var storyPoints = _estimates.HasEstimateFor(p.Name)
                                                                            ? _estimates[p.Name]
                                                                            : StoryPoints.None;
                                              estimations.Add(new Estimate(p.Name, storyPoints));
                                          });
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
}