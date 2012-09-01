using System.Collections.Generic;
using System.Linq;

namespace Spooker.Web.Domain
{
    public class RoundKeeper
    {
        private EstimationRound _activeRound;

        public bool HasActiveRound
        {
            get { return _activeRound != null; }
        }

        public EstimationRound ActiveRound
        {
            get { return _activeRound; }
        }

        public void Enroll(Participant participant)
        {
            if (!HasActiveRound)
            {
                StartNewRound(participant);
                return;
            }
            
            if (_activeRound.Status.IsCompleted)
            {
                var newRoundParticipants = _activeRound.Partipants.Union(new[] {participant}).ToArray();
                StartNewRound(newRoundParticipants);
            } else
            {
                participant.Participate(_activeRound);
            }
        }

        public void StartNewRound()
        {
            StartNewRound(_activeRound.Partipants.ToArray());
        }

        private void StartNewRound(Participant participant)
        {
            StartNewRound(new []{participant});
        }

        private void StartNewRound(IEnumerable<Participant> participants)
        {
            if (HasActiveRound && !ActiveRound.Status.IsCompleted)
                throw new ActiveRoundNotCompletedException();

            _activeRound = new EstimationRound();
            foreach (var participant in participants)
                participant.Participate(_activeRound);
        }

        public static class Factory
        {
            private static readonly RoundKeeper Instance = new RoundKeeper();

            public static RoundKeeper GetInstance()
            {
                return Instance;
            }
        }
    }
}