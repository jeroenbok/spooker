using System;
using Spooker.Web.Infrastructure.Extensions;

namespace Spooker.Web.Domain
{
    public class Participant
    {
        public event EventHandler<EstimatedArgs> Estimated;
        
        private readonly string _name;
        private EstimationRound _round;

        public Participant(string name)
        {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
        }

        public void Participate(EstimationRound round)
        {
            _round = round;
            _round.Join(this);
        }

        public void Estimate(StoryPoints storyPoints)
        {
            if (_round == null)
                throw new NotParticipatingInRoundException(_name);
            Estimated.Raise(this, new EstimatedArgs(new Estimate(_name, storyPoints)));
        }

        public override string ToString()
        {
            return _name;
        }

        public static Participant In(EstimationRound round, string name = "anonymous")
        {
            var participant = new Participant(name);
            participant.Participate(round);
            return participant;
        }
    }
}
