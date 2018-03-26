using System.Collections.Specialized;
using CustomerManagement.Web;
using Moq;
using NUnit.Framework;
using NBehave.Spec.NUnit;

namespace CustomerManagement.UnitTests.Service
{
    [TestFixture]
    public class RequestParameterTest
    {
        [Test]
        public void ShouldReturnFirstNameAsARequestParameter()
        {
            var parameter = new RequestParameter(WithWebOperationContextForSearchingStudentsByName());
            parameter.FirstName.ShouldEqual("firstName");
        }
        
        [Test]
        public void ShouldReturnLastNameAsARequestParameter()
        {
            var parameter = new RequestParameter(WithWebOperationContextForSearchingStudentsByName());
            parameter.FirstName.ShouldEqual("firstName");
        }
        
        [Test]
        public void ShouldReturnFirstAndLastNameAsARequestParameterWhenMatchingKeysFoundInAnyCaseFormat()
        {
            var parameter = new RequestParameter(WithWebOperationContextRequestParameterKeyIsInUpperCaseFormat());
            parameter.FirstName.ShouldEqual("firstName");
            parameter.LastName.ShouldEqual("lastName");
        }

        [Test]
        public void ShouldIgnoreRequestParameterWithKeyThatIsEmpty()
        {
            var parameter = new RequestParameter(WithWebOperationContextRequestParameterKeyEmpty());
            parameter.ShouldNotBeNull();
        }

        private static IWebOperationContext WithWebOperationContextRequestParameterKeyIsInUpperCaseFormat()
        {
            var webOperationContext = new Mock<IWebOperationContext>();
            var incomingRequest = new Mock<IIncomingRequest>();
            incomingRequest.Setup(x => x.QueryParameters).Returns(new NameValueCollection
                                                                      {
                                                                          {string.Empty, "some value"},
                                                                          {"firstName".ToUpper(), "firstName"},
                                                                          {"lastName".ToUpper(), "lastName"}
                                                                      });
            webOperationContext.Setup(x => x.IncomingRequest).Returns(incomingRequest.Object);

            return webOperationContext.Object;
        }

        private static IWebOperationContext WithWebOperationContextRequestParameterKeyEmpty()
        {
            var webOperationContext = new Mock<IWebOperationContext>();
            var incomingRequest = new Mock<IIncomingRequest>();
            incomingRequest.Setup(x => x.QueryParameters).Returns(new NameValueCollection()
                                                                      {
                                                                          {string.Empty, "some value"},
                                                                          {"firstName", "firstName"},
                                                                          {"lastName", "lastName"}
                                                                      });
            webOperationContext.Setup(x => x.IncomingRequest).Returns(incomingRequest.Object);

            return webOperationContext.Object;
        }
        
        private static IWebOperationContext WithWebOperationContextForSearchingStudentsByName()
        {
            var webOperationContext = new Mock<IWebOperationContext>();
            var incomingRequest = new Mock<IIncomingRequest>();
            incomingRequest.Setup(x => x.QueryParameters).Returns(new NameValueCollection()
                                                                      {
                                                                          {"firstName", "firstName"},
                                                                          {"lastName", "lastName"}
                                                                      });
            webOperationContext.Setup(x => x.IncomingRequest).Returns(incomingRequest.Object);

            return webOperationContext.Object;
        }               
    }
}
