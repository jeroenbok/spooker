using NUnit.Framework;

namespace Spooker.Web.Test.Domain
{
    [TestFixture]
    public class When_sole_participant_in_round_is_removed : In_round_with_sole_partipant_which_has_not_estimated
    {
        protected override void When()
        {
            _estimationRound.Remove(_soleParticipant);
        }

        [Then]
        public void Round_is_completed()
        {
            Assert.That(_estimationRound.Status.IsCompleted, "round is completed");
        }
    }
}