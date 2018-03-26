using System.Collections.Generic;
using System.Collections.Specialized;
using CustomerManagement.Handlers;
using CustomerManagement.Handlers.Chain;
using CustomerManagement.Model.Students;
using CustomerManagement.View;
using CustomerManagement.Web;
using Moq;
using NUnit.Framework;

namespace CustomerManagement.UnitTests.Handlers.Chain
{
    [TestFixture]
    public class SearchRequestHandlerByNameTest
    {
        private Mock<IStudentRepository> repository;
        private Mock<IViewEngine> viewEngine;
        private Mock<ISearchRequestHandler> successorRequestHandler;

        [SetUp]
        public void SetUp()
        {
            repository = new Mock<IStudentRepository>();
            viewEngine = new Mock<IViewEngine>();
            successorRequestHandler = new Mock<ISearchRequestHandler>();
        }

        [Test]
        public void ShouldCallUnderlyingStudentRepository()
        {
            IEnumerable<Student> students = new List<Student>();
            repository.Setup(x => x.SearchByName(It.IsAny<string>(), It.IsAny<string>())).Returns(students);
            viewEngine.Setup(x => x.Render(students)).Returns("<some_tag/>");
            var requestParameter = PopulateRequestParamtersForSearchingStudentsByName();

            var requestHandler = new SearchRequestHandlerByName(successorRequestHandler.Object, repository.Object, viewEngine.Object);
            requestHandler.HandleRequest(requestParameter);

            repository.Verify(x => x.SearchByName(requestParameter.FirstName, requestParameter.LastName));
        }

        [Test]
        public void ShouldCallUnderlyingViewEngine()
        {
            IEnumerable<Student> students = new List<Student>();
            repository.Setup(x => x.SearchByName(It.IsAny<string>(), It.IsAny<string>())).Returns(students);
            viewEngine.Setup(x => x.Render(students)).Returns("<some_tag/>");
            var requestParameter = PopulateRequestParamtersForSearchingStudentsByName();

            var requestHandler = new SearchRequestHandlerByName(successorRequestHandler.Object, repository.Object, viewEngine.Object);
            requestHandler.HandleRequest(requestParameter);

            viewEngine.Verify(x => x.Render(students));
        }

        [Test]
        public void ShouldCallSuccessorUnderlyingRequestHandlerWhenRequestParametersDoNotSatisfyRequest()
        {
            var requestParameter = PopulateRequestParametersWithNoParameters();

            var requestHandler = new SearchRequestHandlerByName(successorRequestHandler.Object, repository.Object, viewEngine.Object);
            requestHandler.HandleRequest(requestParameter);

            successorRequestHandler.Verify(x => x.HandleRequest(requestParameter));
        }

        private static IRequestParameter PopulateRequestParametersWithNoParameters()
        {
            var webOperationContext = new Mock<IWebOperationContext>();
            var incomingRequest = new Mock<IIncomingRequest>();
            incomingRequest.Setup(x => x.QueryParameters).Returns(new NameValueCollection());
            webOperationContext.Setup(x => x.IncomingRequest).Returns(incomingRequest.Object);

            return new RequestParameter(webOperationContext.Object);
        }


        private static IRequestParameter PopulateRequestParamtersForSearchingStudentsByName()
        {
            var webOperationContext = new Mock<IWebOperationContext>();
            var incomingRequest = new Mock<IIncomingRequest>();
            incomingRequest.Setup(x => x.QueryParameters).Returns(new NameValueCollection
                                                                      {
                                                                          {"firstName", "firstName"},
                                                                          {"lastName", "lastName"}
                                                                      });
            webOperationContext.Setup(x => x.IncomingRequest).Returns(incomingRequest.Object);

            return new RequestParameter(webOperationContext.Object);
        }
    }
}
