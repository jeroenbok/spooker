using NUnit.Framework;

namespace Spooker.Web.Test.Domain
{
    [TestFixture]
    public class When_participant_without_estimate_is_removed : In_round_with_two_participants_of_which_one_has_estimated
    {
        protected override void When()
        {
            _estimationRound.Remove(_participantWithoutEstimate);
        }

        [Then]
        public void Round_is_completed()
        {
            Assert.That(_estimationRound.Status.IsCompleted, "round is completed");
        }
    }
}