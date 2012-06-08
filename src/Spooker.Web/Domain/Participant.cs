namespace Spooker.Web.Domain
{
    public class Participant
    {
        private readonly string _name;

        public Participant(string name)
        {
            _name = name;
        }

        public void Participate(VotingRound round)
        {
            round.Join(this);
        }

        public override string ToString()
        {
            return _name;
        }
    }
}
