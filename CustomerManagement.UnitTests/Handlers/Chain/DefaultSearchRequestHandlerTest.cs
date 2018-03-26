using System.Net;
using CustomerManagement.Exceptions;
using CustomerManagement.Handlers.Chain;
using NUnit.Framework;
using NBehave.Spec.NUnit;
using Assert=CustomerManagement.Common.TestUtilities.Assert;

namespace CustomerManagement.UnitTests.Handlers.Chain
{
    [TestFixture]
    public class DefaultSearchRequestHandlerTest
    {
        [Test]
        public void ShouldReturnRestExceptionWithHttpStatusBadRequestWhenCalled()
        {
            var requestHandler = new DefaultSearchRequestHandler();
            var restException = Assert.ThrowsExactly<RestException>(() => requestHandler.HandleRequest(null));
            restException.StatusCode.ShouldEqual(HttpStatusCode.BadRequest);
        }
    }
}
