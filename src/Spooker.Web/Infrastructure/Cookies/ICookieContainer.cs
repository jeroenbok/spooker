using System;

namespace Spooker.Web.Infrastructure.Cookies
{
    public interface ICookieContainer
    {
        bool Exists(string key);

        string GetValue(string key);
        void SetValue(string key, string value, DateTime expires);
    }
}