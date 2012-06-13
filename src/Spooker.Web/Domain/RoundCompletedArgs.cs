using System;

namespace Spooker.Web.Domain
{
    public class RoundCompletedArgs : EventArgs
    {
        private readonly EstimationStatus _status;

        public RoundCompletedArgs(EstimationStatus status)
        {
            _status = status;
        }

        public EstimationStatus Status
        {
            get { return _status; }
        }
    }
}