using System;
using System.Net;
using Castle.Core.Interceptor;
using CustomerManagement.Infrastructure.Container;
using CustomerManagement.Infrastructure.Container.Interceptors;
using CustomerManagement.Logging;
using CustomerManagement.Web;
using Moq;
using NUnit.Framework;

namespace CustomerManagement.UnitTests.Infrastructure.Container.Interceptors
{
    [TestFixture]
    public class ExceptionSuppressionAndLoggingInterceptorTest
    {
        private Mock<IWebOperationContext> webOperationContext;
        private Mock<IInvocation> invocation;
        private Mock<ILogger> logger;

        [SetUp]
        public void SetUp()
        {
            webOperationContext = new Mock<IWebOperationContext> { DefaultValue = DefaultValue.Mock };
            invocation = new Mock<IInvocation>();
            logger = new Mock<ILogger>();

            InitializeContainer();
        }

        private void InitializeContainer()
        {
            var resolver = new Mock<IDependencyResolver>();
            var factory = new Mock<ILogFactory>();

            resolver.Setup(x => x.GetInstanceOf<ILogFactory>()).Returns(factory.Object);
            factory.Setup(x => x.CreateFor(It.IsAny<Type>())).Returns(logger.Object);

            IoC.Initialize(resolver.Object);
        }

        [Test]
        public void WhenUnexpectedExceptionsOccurSetStatusCodeOfOutgoingResponseToInternalServerError()
        {
            var exception = new Exception("Something has gone terribly wrong!");
            invocation.Setup(x => x.Proceed()).Throws(exception);

            IInterceptor interceptor = new ExceptionSuppressionAndLoggingInterceptor(webOperationContext.Object);
            interceptor.Intercept(invocation.Object);

            var outgoingResponse = Mock.Get(webOperationContext.Object.OutgoingResponse);
            outgoingResponse.VerifySet(x => x.StatusCode = HttpStatusCode.InternalServerError);
            outgoingResponse.VerifySet(x => x.SuppressEntityBody = true);
        }

        [Test]
        public void LogUnexpectedExceptions()
        {
            var exception = new Exception("Something has gone terribly wrong!");
            invocation.Setup(x => x.Proceed()).Throws(exception);

            IInterceptor interceptor = new ExceptionSuppressionAndLoggingInterceptor(webOperationContext.Object);
            interceptor.Intercept(invocation.Object);

            logger.Verify(x => x.Exception("Exception", exception));
        }
    }
}