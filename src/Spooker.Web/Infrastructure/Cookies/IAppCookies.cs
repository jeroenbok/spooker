using System;

namespace Spooker.Web.Infrastructure.Cookies
{
    public interface IAppCookies
    {
        Guid ParticipantId { get; set; }
    }
}