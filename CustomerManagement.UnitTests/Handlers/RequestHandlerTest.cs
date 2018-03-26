using System;
using System.Net;
using CustomerManagement.Exceptions;
using CustomerManagement.Handlers;
using CustomerManagement.Model.Students;
using CustomerManagement.View;
using CustomerManagement.Web;
using Moq;
using NUnit.Framework;

namespace CustomerManagement.UnitTests.Handlers
{
    [TestFixture]
    public class RequestHandlerTest
    {
        private Mock<IStudentRepository> repository;
        private Mock<IViewEngine> viewEngine;
        private Mock<IWebOperationContext> webOperationContext;
        private Mock<IIncomingRequest> incomingRequest;
        private RequestHandler requestHandler;

        [SetUp]
        public void SetUp()
        {
            repository = new Mock<IStudentRepository>();    
            viewEngine = new Mock<IViewEngine>();
            webOperationContext = new Mock<IWebOperationContext>();
            incomingRequest = new Mock<IIncomingRequest>();

            webOperationContext.Setup(x => x.IncomingRequest).Returns(incomingRequest.Object);
            requestHandler = new RequestHandler(repository.Object, viewEngine.Object, webOperationContext.Object);
        }

        [Test]
        public void ShouldCallUnderlyingStudentRepositoryWithGivenId()
        {
            const string id = "some identifier";
            var student = new Student(0, null, null, DateTime.Now, null, null, null, null);

            incomingRequest.Setup(x => x.HttpMethod).Returns(HttpMethod.GET);
            repository.Setup(x => x.Get(id)).Returns(student);
            viewEngine.Setup(x => x.Render(student)).Returns("<some_tag/>");

            requestHandler.HandleRequestFor(id);

            repository.Verify(x => x.Get(id));
        }
        
        [Test]
        public void ShouldThrowBadRequestExceptionWhenCalledWithNullId()
        {
            Common.TestUtilities.Assert.ThrowsRestExceptionWithHttpStatusCodeOf(HttpStatusCode.BadRequest,
                () => requestHandler.HandleRequestFor((Student)null));
        }

        [Test]
        public void ShouldThrowBadRequestExceptionWhenCalledWithEmptyId()
        {
            Common.TestUtilities.Assert.ThrowsRestExceptionWithHttpStatusCodeOf(HttpStatusCode.BadRequest,
                () => requestHandler.HandleRequestFor(string.Empty));
        }

        [Test]
        public void ShouldCallUnderlyingStudentRepositoryWithGivenType()
        {
            var student = new Student
                              {
                                  FirstName = "firstName",
                                  LastName = "lastName",
                                  DateOfBirth = DateTime.Now,
                                  AddressLine1 = "addressLine1",
                                  AddressLine2 = "addressLine2",
                                  Grade = "grade",
                                  PhoneNumber = "phoneNumber"
                              };

            repository.Setup(x => x.Modify(student)).Returns(student);
            viewEngine.Setup(x => x.Render(student)).Returns("<some_tag/>");

            requestHandler.HandleRequestFor(student);

            repository.Verify(x => x.Modify(student));
        }

        [Test] 
        public void ShouldThrowBadRequestRestExceptionWhenCalledWithNullInstance()
        {
            Common.TestUtilities.Assert.ThrowsRestExceptionWithHttpStatusCodeOf(HttpStatusCode.BadRequest, 
                () => requestHandler.HandleRequestFor((Student) null));
        }
    }
}
