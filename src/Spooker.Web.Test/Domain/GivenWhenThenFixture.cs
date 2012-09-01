using System;
using NUnit.Framework;

namespace Spooker.Web.Test.Domain
{
    public abstract class GivenWhenThenFixture
    {
        private Exception _caughtException;

        [SetUp]
        public void SetUp()
        {
            Given();
            try
            {
                When();
            } catch (Exception e)
            {
                _caughtException = e;
            } finally
            {
                FinallyAfterExceptionCaught();
            }
        }

        protected abstract void Given();
        protected abstract void When();
        protected Exception CaughtException
        {
            get { return _caughtException; }
        }
        protected void FinallyAfterExceptionCaught() { }
    }
}