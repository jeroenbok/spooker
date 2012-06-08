using System;
using Spooker.Web.Infrastructure.Extensions;

namespace Spooker.Web.Domain
{
    public class Participant
    {
        public event EventHandler<VotedArgs> Voted;
        
        private readonly string _name;
        private VotingRound _round;

        public Participant(string name)
        {
            _name = name;
        }

        public void Participate(VotingRound round)
        {
            _round = round;
            _round.Join(this);
        }

        public override string ToString()
        {
            return _name;
        }

        public void Vote(int estimate)
        {
            if (_round == null)
                throw new NotParticipatingInRoundException(_name);
            Voted.Raise(this, new VotedArgs(estimate));
        }
    }
}
