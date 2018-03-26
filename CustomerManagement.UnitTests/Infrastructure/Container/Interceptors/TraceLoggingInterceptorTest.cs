using System;
using Castle.Core.Interceptor;
using CustomerManagement.Infrastructure.Container;
using CustomerManagement.Infrastructure.Container.Interceptors;
using CustomerManagement.Logging;
using Moq;
using NBehave.Spec.NUnit;
using NUnit.Framework;

namespace CustomerManagement.UnitTests.Infrastructure.Container.Interceptors
{
    [TestFixture]
    public class TraceLoggingInterceptorTest
    {
        private Mock<IInvocation> invocation;
        private Mock<ILogger> logger;

        private string log;

        [SetUp]
        public void SetUp()
        {
            logger = new Mock<ILogger>();
            invocation = new Mock<IInvocation>();

            log = string.Empty;
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
        public void ShouldLogMessageBeforeInterceptedMethodIsCalled()
        {
            invocation.Setup(i => i.Proceed()).Callback(() => log += "Proceed");
            logger.Setup(l => l.Trace("Entering: {0}", It.IsAny<object>()))
                .Callback((string format, object args) => log += "Entering");

            IInterceptor interceptor = new TraceLoggingInterceptor();
            interceptor.Intercept(invocation.Object);

            log.ShouldEqual("EnteringProceed");
        }

        [Test]
        public void ShouldLogMessageAfterInterceptedMethodIsCalled()
        {
            invocation.Setup(i => i.Proceed()).Callback(() => log += "Proceed");
            logger.Setup(l => l.Trace(It.IsAny<IFormatProvider>(), "Exiting: {0:R}", It.IsAny<object>()))
                .Callback((IFormatProvider provider, string format, object args) => log += "Exiting");

            IInterceptor interceptor = new TraceLoggingInterceptor();
            interceptor.Intercept(invocation.Object);

            log.ShouldEqual("ProceedExiting");
        }

        [Test]
        public void ShouldLogInformationFromTheInvocationContext()
        {
            var mock = new Mock<IInvocationContext>();

            IInterceptor interceptor = new TraceLoggingInterceptorForTest(mock.Object);
            interceptor.Intercept(invocation.Object);

            logger.Verify(l => l.Trace("Entering: {0}", mock.Object));
            logger.Verify(l => l.Trace(It.IsAny<InvocationFormatProvider>(), "Exiting: {0:R}", mock.Object));
        }

        private class TraceLoggingInterceptorForTest : TraceLoggingInterceptor
        {
            private readonly IInvocationContext mockContext;

            public TraceLoggingInterceptorForTest(IInvocationContext mockContext)
            {
                this.mockContext = mockContext;
            }

            protected override IInvocationContext GetInvocationContext(IInvocation invocation)
            {
                return mockContext;
            }
        }
    }
}