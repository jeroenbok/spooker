using Spooker.Web.Domain;

namespace Spooker.Web.Test.Domain
{
    public abstract class In_round_with_sole_partipant_which_has_not_estimated : GivenWhenThenFixture
    {
        protected readonly EstimationRound _estimationRound = new EstimationRound();
        protected readonly Participant _soleParticipant = new Participant("sole");

        protected override void Given()
        {
            _soleParticipant.Participate(_estimationRound);
        }
    }
}