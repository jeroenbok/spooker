namespace Spooker.Web.Domain
{
    public class Estimate
    {
        private readonly string _participantName;
        private readonly int _storyPoints;

        public Estimate(string participantName, int storyPoints)
        {
            _participantName = participantName;
            _storyPoints = storyPoints;
        }

        public string ParticipantName
        {
            get { return _participantName; }
        }

        public int StoryPoints
        {
            get {
                return _storyPoints;
            }
        }
    }
}