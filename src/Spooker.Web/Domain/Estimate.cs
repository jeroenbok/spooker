using Spooker.Web.Infrastructure;

namespace Spooker.Web.Domain
{
    public class Estimate
    {
        private readonly string _participantName;
        private readonly StoryPoints _storyPoints;

        public Estimate(string participantName, StoryPoints storyPoints)
        {
            _participantName = participantName;
            _storyPoints = storyPoints;
        }

        public string ParticipantName
        {
            get { return _participantName; }
        }

        public StoryPoints StoryPoints
        {
            get {
                return _storyPoints;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Estimate)) return false;
            return Equals((Estimate) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_participantName.GetHashCode() * 397) ^ _storyPoints.GetHashCode();
            }
        }

        public override string ToString()
        {
            return new ReflectiveToStringBuilder(this).ToString();
        }

        private bool Equals(Estimate other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other._participantName, _participantName) && Equals(other._storyPoints, _storyPoints);
        }
    }
}