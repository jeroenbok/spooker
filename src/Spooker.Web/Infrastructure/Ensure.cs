using System;

namespace Spooker.Web.Infrastructure
{
    public static class Ensure
    {
        public static T NotNull<T>(T value, string message) where T : class
        {
            if (value == null)
                throw new ArgumentNullException(message, (Exception) null);
            return value;
        }
    }
}