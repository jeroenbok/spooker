using System;

namespace Spooker.Web.Infrastructure.Cookies
{
    public class AppCookies : IAppCookies
    {
        private readonly ICookieContainer _cookieContainer;

        public AppCookies(ICookieContainer cookieContainer)
        {
            _cookieContainer = Ensure.NotNull(cookieContainer, "cookieContainer");
        }

        public Guid ParticipantId
        {
            get
            {
                Guid userId;
                if (Guid.TryParse(_cookieContainer.GetValue("Spooker.ParticipantId"), out userId))
                    return userId;
                return Guid.Empty;
            }
            set { _cookieContainer.SetValue("Spooker.ParticipantId", value.ToString(), DateTime.Now.AddHours(4)); }
        }
    }
}