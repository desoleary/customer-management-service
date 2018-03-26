using CustomerManagement.Handlers;
using CustomerManagement.Model.Students;
using Moq;
using NUnit.Framework;

namespace CustomerManagement.UnitTests.Service
{
    [TestFixture]
    public class CustomerManagementRestServiceTest
    {
        private Mock<ISearchRequestHandler> searchRequestHandler;
        private Mock<IRequestHandler> requestHandler;
        private Mock<IRequestParameter> requestParameter;
        private CustomerManagementRestService service;

        [SetUp]
        public void SetUp()
        {
            requestHandler = new Mock<IRequestHandler>();
            searchRequestHandler = new Mock<ISearchRequestHandler>();
            requestParameter = new Mock<IRequestParameter>();
            service = new CustomerManagementRestService(requestHandler.Object, searchRequestHandler.Object, requestParameter.Object);
        }

        [Test]
        public void Search_ShouldCallUnderlyingRequestHandler()
        {
            service.Search();

            searchRequestHandler.Verify(x => x.HandleRequest(requestParameter.Object));
        }

        [Test]
        public void Get_ShouldCallUnderlyingRequestHandler()
        {
            const string someStudentId = "12345";
            service.Get(someStudentId);

            requestHandler.Verify(x => x.HandleRequestFor(someStudentId));
        }

        [Test]
        public void Create_ShouldCallUnderlyingRequestHandlerWithPassedInStudent()
        {
            var student = new Student();

            service.Create(student);

            requestHandler.Verify(x => x.HandleRequestFor(student));
        }        

        [Test]
        public void Delete_ShouldCallUnderlyingRequestHandler()
        {
            const string someStudentId = "12345";

            service.Delete(someStudentId);

            requestHandler.Verify(x => x.HandleRequestFor(someStudentId));
        }        
    }
}