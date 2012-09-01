using System;
using System.Collections.Generic;
using System.Linq;
using Spooker.Web.Infrastructure.Extensions;

namespace Spooker.Web.Domain
{
    public class EstimationRound
    {
        public event EventHandler<RoundCompletedArgs> Completed = delegate { };

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
                return new EstimationStatus(estimations, AllParticipantsHaveEstimated);
            }
        }

        public void Join(Participant participant)
        {
            if (_participants.Any(p => p.Name == participant.Name))
                throw new ParticipantAlreadyParticpatesInRoundException(participant.Name);
            _participants.Add(participant);
            participant.Estimated += RegisterEstimate;
        }

        public void Remove(Participant participant)
        {
            var idOfParticipantToRemove = participant.Id;
            var participantToRemove = _participants.SingleOrDefault(p => p.Id.Equals(idOfParticipantToRemove));
            if (participantToRemove == null)
                throw new NoSuchParticipantException(idOfParticipantToRemove);
            _participants.Remove(participant);
        }

        public bool HasParticipant(Guid participantId)
        {
            return Partipants.All(p => p.Id != participantId);
        }

        public string NameOfParticipant(Guid participantId)
        {
            return Partipants.Single(p => p.Id == participantId).Name;
        }

        private void RegisterEstimate(object sender, EstimatedArgs args)
        {
            if (AllParticipantsHaveEstimated)
                throw new CannotEstimateWhenAllEstimatesAreGivenException();

            _estimates = _estimates.Register(args.Estimate);
            
            if (AllParticipantsHaveEstimated)
                Completed(this, new RoundCompletedArgs(Status));
        }

        private bool AllParticipantsHaveEstimated
        {
            get { return Partipants.Count() == Estimates.Count; }
        }

        public Participant ParticipantById(Guid participantId)
        {
            return Partipants.Single(p => p.Id == participantId);
        }
    }
}