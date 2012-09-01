using System;
using Spooker.Web.Infrastructure;

namespace Spooker.Web.Domain
{
    public class Participant
    {
        public event EventHandler<EstimatedArgs> Estimated = delegate { };
        
        private readonly string _name;
        private readonly Guid _id;

        private EstimationRound _participatingRound;

        public Participant(string name)
        {
            _name = name;
            _id = Guid.NewGuid();
        }

        public string Name
        {
            get { return _name; }
        }

        public Guid Id
        {
            get { return _id; }
        }

        public void Participate(EstimationRound round)
        {
            if (_participatingRound != null)
                _participatingRound.Remove(this);

            _participatingRound = round;
            _participatingRound.Join(this);
        }

        public void Estimate(StoryPoints storyPoints)
        {
            if (_participatingRound == null)
                throw new NotParticipatingInRoundException(_name);

            Estimated(this, new EstimatedArgs(new Estimate(_name, storyPoints)));
        }

        public override string ToString()
        {
            return new ReflectiveToStringBuilder(this).ToString();
        }

        public static Participant In(EstimationRound round, string name = "anonymous")
        {
            var participant = new Participant(name);
            participant.Participate(round);
            return participant;
        }
    }
}
