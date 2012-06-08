using System.Collections;
using System.Collections.Generic;

namespace Spooker.Web.Domain
{
    public class EstimationStatus : IEnumerable<Estimate>
    {
        private readonly IList<Estimate> _estimations;

        public EstimationStatus(IList<Estimate> estimations)
        {
            _estimations = estimations;
        }

        public IEnumerator<Estimate> GetEnumerator()
        {
            return _estimations.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}