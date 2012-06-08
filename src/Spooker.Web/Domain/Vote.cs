namespace Spooker.Web.Domain
{
    public class Vote
    {
        private readonly string _participantName;
        private readonly int _estimate;

        public Vote(string participantName, int estimate)
        {
            _participantName = participantName;
            _estimate = estimate;
        }

        public string ParticipantName
        {
            get { return _participantName; }
        }

        public int Estimate
        {
            get {
                return _estimate;
            }
        }
    }
}