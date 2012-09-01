using System;
using System.Web;
using Moq;
using NUnit.Framework;
using Spooker.Web.Infrastructure.Cookies;

namespace Spooker.Web.Test.Infrastructure.Cookies
{
    [TestFixture]
    public class CookieContainerTest
    {
        #region Setup/Teardown

        [SetUp]
        public virtual void SetUp()
        {
            _httpRequest = new Mock<HttpRequestBase>();
            _httpRequest.Setup(x => x.Cookies).Returns(new HttpCookieCollection());
            _httpResponse = new Mock<HttpResponseBase>();
            _httpResponse.Setup(x => x.Cookies).Returns(new HttpCookieCollection());

            _cookieContainer = new CookieContainer(_httpRequest.Object, _httpResponse.Object);
        }

        #endregion

        private CookieContainer _cookieContainer;
        private Mock<HttpRequestBase> _httpRequest;
        private Mock<HttpResponseBase> _httpResponse;

        [Test]
        public void When_there_are_no_cookies_Then_cookie_key_cannot_be_found()
        {
            bool exists = _cookieContainer.Exists("nonExistingCookie");

            Assert.That(exists, Is.False, "cookie exists");
        }

        [Test]
        public void When_there_is_a_cookie_Then_GetValue_returns_value()
        {
            var existingCookie = new HttpCookie("cookie");
            existingCookie.Value = "value";
            _httpRequest.Object.Cookies.Add(existingCookie);

            string value = _cookieContainer.GetValue("cookie");

            Assert.That(value, Is.EqualTo("value"), "cookie value");
        }

        [Test]
        public void When_there_is_an_existing_cookie_Then_key_can_be_found()
        {
            _httpRequest.Object.Cookies.Add(new HttpCookie("existingCookie"));

            bool exists = _cookieContainer.Exists("existingCookie");

            Assert.That(exists, "cookie exists");
        }

        [Test]
        public void When_there_is_cookie_Then_setValue_overwrites_cookie()
        {
            var existingCookie = new HttpCookie("cookie");
            existingCookie.Value = "existing value";
            existingCookie.Expires = DateTime.Now.AddDays(1);
            _httpResponse.Object.Cookies.Add(existingCookie);

            DateTime newExpirationDate = DateTime.Now.AddDays(2);
            _cookieContainer.SetValue("cookie", "new value", newExpirationDate);

            HttpCookie cookie = _httpResponse.Object.Cookies["cookie"];
            Assert.That(cookie, Is.Not.Null, "cookie");
            Assert.That(cookie.Value, Is.EqualTo("new value"), "cookie value");
            Assert.That(cookie.Expires, Is.EqualTo(newExpirationDate), "cookie expiration date");
        }

        [Test]
        public void When_there_is_no_cookie_Then_GetValue_returns_null()
        {
            string value = _cookieContainer.GetValue("cookie");

            Assert.That(value, Is.Null, "cookie value");
        }

        [Test]
        public void When_there_is_no_cookie_Then_setValue_creates_new_Cookie()
        {
            DateTime expirationDate = DateTime.Now.AddDays(1);
            _cookieContainer.SetValue("cookie", "value", expirationDate);

            HttpCookie cookie = _httpResponse.Object.Cookies["cookie"];
            Assert.That(cookie, Is.Not.Null, "cookie");
            Assert.That(cookie.Value, Is.EqualTo("value"), "cookie value");
            Assert.That(cookie.Expires, Is.EqualTo(expirationDate), "cookie expiration date");
        }
    }
}