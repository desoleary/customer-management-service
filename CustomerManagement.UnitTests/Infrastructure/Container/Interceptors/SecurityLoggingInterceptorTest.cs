using System;
using Castle.Core.Interceptor;
using CustomerManagement.Infrastructure.Container;
using CustomerManagement.Infrastructure.Container.Interceptors;
using CustomerManagement.Logging;
using CustomerManagement.Web;
using Moq;
using NBehave.Spec.NUnit;
using NUnit.Framework;

namespace CustomerManagement.UnitTests.Infrastructure.Container.Interceptors
{
    [TestFixture]
    public class SecurityLoggingInterceptorTest
    {
        private const string SomeUri = "http://requestedUri";

        private Mock<IWebOperationContext> webOperationContext;
        private Mock<IIncomingRequest> incomingRequest;
        private Mock<IInvocation> invocation;
        private Mock<ILogger> logger;

        [SetUp]
        public void SetUp()
        {
            logger = new Mock<ILogger>();
            invocation = new Mock<IInvocation>();
            webOperationContext = new Mock<IWebOperationContext> { DefaultValue = DefaultValue.Mock };
            incomingRequest = Mock.Get(webOperationContext.Object.IncomingRequest);

            InitializeContainer();
        }

        private void InitializeContainer()
        {
            var resolver = new Mock<IDependencyResolver>();
            var factory = new Mock<ILogFactory>();

            resolver.Setup(x => x.GetInstanceOf<ILogFactory>()).Returns(factory.Object);
            factory.Setup(x => x.CreateFor(It.IsAny<string>())).Returns(logger.Object);

            IoC.Initialize(resolver.Object);
        }

        [Test]
        public void LogsBasicSecurityInformationForEveryRequest()
        {
            var expectedUri = new Uri(SomeUri);
            var expectedAddress = new ClientAddress("address", 0);

            incomingRequest.Setup(request => request.RequestUri).Returns(expectedUri);
            incomingRequest.Setup(request => request.ClientAddress).Returns(expectedAddress);

            new SecurityLoggingInterceptor(webOperationContext.Object).Intercept(invocation.Object);

            logger.Verify(x => x.Info("ClientRequest {0} {1}", expectedAddress, expectedUri));
        }

        [Test]
        public void LogsSecurityInfoBeforeCallingServiceMethod()
        {
            var securityInfoIsLogged = false;

            incomingRequest.Setup(request => request.RequestUri).Returns(new Uri(SomeUri));
            incomingRequest.Setup(request => request.ClientAddress).Returns(new ClientAddress("address", 0));

            logger.Setup(x =>x.Info("ClientRequest {0} {1}", It.IsAny<ClientAddress>(), It.IsAny<Uri>())).
                Callback(() => securityInfoIsLogged = true);

            invocation.Setup(x => x.Proceed()).Callback(() => securityInfoIsLogged.ShouldBeTrue());

            new SecurityLoggingInterceptor(webOperationContext.Object).Intercept(invocation.Object);
        }
    }
}