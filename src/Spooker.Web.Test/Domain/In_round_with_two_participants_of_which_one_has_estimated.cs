using Spooker.Web.Domain;

namespace Spooker.Web.Test.Domain
{
    public abstract class In_round_with_two_participants_of_which_one_has_estimated : GivenWhenThenFixture
    {
        protected readonly EstimationRound _estimationRound = new EstimationRound();
        protected readonly Participant _participantWithEstimate = new Participant("WITH-ESTIMATE");
        protected readonly Participant _participantWithoutEstimate = new Participant("WITHOUT-ESTIMATE");

        protected override void Given()
        {
            _participantWithEstimate.Participate(_estimationRound);
            _participantWithEstimate.Estimate(StoryPoints.One);

            _participantWithoutEstimate.Participate(_estimationRound);
        }
    }
}