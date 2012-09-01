using System;
using System.Linq;
using System.Web;

namespace Spooker.Web.Infrastructure.Cookies
{
    public class CookieContainer : ICookieContainer
    {
        private readonly HttpRequestBase _request;
        private readonly HttpResponseBase _response;

        public CookieContainer(HttpRequestBase request, HttpResponseBase response)
        {
            // "Check" is a helper class, I've got from the "Kigg" project
//            Ensure.IsNotNull(request, "request");
//            Ensure.IsNotNull(response, "response");

            _request = request;
            _response = response;
        }

        public bool Exists(string key)
        {
            return _request.Cookies.AllKeys.Contains(key);
        }

        public string GetValue(string key)
        {
//            Ensure.IsNotEmpty(key, "key");

            HttpCookie cookie = _request.Cookies[key];
            return cookie != null ? cookie.Value : null;
        }

        public void SetValue(string key, string value, DateTime expires)
        {
//            Ensure.IsNotEmpty(key, "key");

            HttpCookie cookie = new HttpCookie(key, value) { Expires = expires };
            _response.Cookies.Set(cookie);
        }

        // ... see code sample for full implementation
    }
}