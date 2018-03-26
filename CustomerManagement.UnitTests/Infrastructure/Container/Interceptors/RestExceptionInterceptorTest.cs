using System.Net;
using Castle.Core.Interceptor;
using CustomerManagement.Exceptions;
using CustomerManagement.Infrastructure.Container.Interceptors;
using CustomerManagement.Web;
using Moq;
using NUnit.Framework;
using Assert=CustomerManagement.Common.TestUtilities.Assert;

namespace CustomerManagement.UnitTests.Infrastructure.Container.Interceptors
{
    [TestFixture]
    public class RestExceptionInterceptorTest
    {
        private Mock<IWebOperationContext> webOperationContext;
        private Mock<IInvocation> invocation;

        [SetUp]
        public void Setup()
        {
            webOperationContext = new Mock<IWebOperationContext> { DefaultValue = DefaultValue.Mock };
            invocation = new Mock<IInvocation>();
        }

        [Test]
        public void ShouldReThrowExceptionForUnsupportedStatusCodes()
        {
            invocation.Setup(x => x.Proceed()).Throws(new RestException(HttpStatusCode.RequestTimeout));

            var interceptor = new RestExceptionInterceptor(webOperationContext.Object);

            Assert.ThrowsExactly<RestException>(() => interceptor.Intercept(invocation.Object));
        }

        [Test]
        public void VerifyEntityBodyIsSuppressedForBadRequest()
        {
            invocation.Setup(x => x.Proceed()).Throws(new RestException(HttpStatusCode.BadRequest));

            var interceptor = new RestExceptionInterceptor(webOperationContext.Object);
            interceptor.Intercept(invocation.Object);

            VerifyEntityBodyIsSuppressedFor(HttpStatusCode.BadRequest);
        }

        [Test]
        public void VerifyEntityBodyIsSuppressedForNoContent()
        {
            invocation.Setup(x => x.Proceed()).Throws(new RestException(HttpStatusCode.NoContent));

            var interceptor = new RestExceptionInterceptor(webOperationContext.Object);
            interceptor.Intercept(invocation.Object);

            VerifyEntityBodyIsSuppressedFor(HttpStatusCode.NoContent);
        }

        [Test]
        public void VerifyEntityBodyIsSuppressedForNotFound()
        {
            invocation.Setup(x => x.Proceed()).Throws(new RestException(HttpStatusCode.NotFound));

            var interceptor = new RestExceptionInterceptor(webOperationContext.Object);
            interceptor.Intercept(invocation.Object);

            VerifyEntityBodyIsSuppressedFor(HttpStatusCode.NotFound);
        }

        private void VerifyEntityBodyIsSuppressedFor(HttpStatusCode statusCode)
        {
            var outgoingResponse = Mock.Get(webOperationContext.Object.OutgoingResponse);

            outgoingResponse.VerifySet(x => x.StatusCode = statusCode);
            outgoingResponse.VerifySet(x => x.SuppressEntityBody = true);
        }
    }
}