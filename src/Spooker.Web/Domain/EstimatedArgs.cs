using System;

namespace Spooker.Web.Domain
{
    public class EstimatedArgs : EventArgs
    {
        private readonly Estimate _estimate;

        public EstimatedArgs(Estimate estimate)
        {
            _estimate = estimate;
        }

        public Estimate Estimate
        {
            get { return _estimate; }
        }
    }
}