using NUnit.Framework;
using Spooker.Web.Domain;

namespace Spooker.Web.Test.Domain
{
    [TestFixture]
    public class ObservingEstimationRoundProgress
    {
        [Test]
        public void When_no_participants_have_estimated_then_no_estimates_are_reported()
        {
            var round = new EstimationRound();
            Participant.In(round, "joe");
            Participant.In(round, "jane");

            Assert.That(round.Status, Is.EquivalentTo(new[] { NoEstimate("joe"), NoEstimate("jane") }), "estimates");
        }

        private Estimate NoEstimate(string participantName)
        {
            return new Estimate(participantName, StoryPoints.None);
        }
    }
}
