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

        private void StartNewRound(params Participant[] participants)
        {
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