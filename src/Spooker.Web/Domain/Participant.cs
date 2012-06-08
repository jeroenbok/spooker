using System;
using Spooker.Web.Infrastructure.Extensions;

namespace Spooker.Web.Domain
{
    public class Participant
    {
        public event EventHandler<EstimatedArgs> Voted;
        
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

        public void Estimate(int estimate)
        {
            if (_round == null)
                throw new NotParticipatingInRoundException(_name);
            Voted.Raise(this, new EstimatedArgs(new Estimate(_name, estimate)));
        }

        public override string ToString()
        {
            return _name;
        }
    }
}
