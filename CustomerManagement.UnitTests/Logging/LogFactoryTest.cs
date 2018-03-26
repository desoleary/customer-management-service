using CustomerManagement.Infrastructure.Container;
using CustomerManagement.Logging;
using Moq;
using NBehave.Spec.NUnit;
using NUnit.Framework;

namespace CustomerManagement.UnitTests.Logging
{
    [TestFixture]
    public class LogFactoryTest
    {
        [Test]
        public void ShouldReturnInitializedLoggerInstance()
        {
            var resolver = new Mock<IDependencyResolver>();
            var loggerAdapter = new Mock<ILogAdapter>();
            var logger = new Mock<ILogger>();

            IoC.Initialize(resolver.Object);
            loggerAdapter.Setup(x => x.CreateLogger(It.IsAny<string>())).Returns(logger.Object);
            resolver.Setup(x => x.GetInstanceOf<ILogAdapter>()).Returns(loggerAdapter.Object);
            

            var factory = new LogFactory(loggerAdapter.Object);
            factory.CreateFor(typeof(LogFactoryTest)).ShouldEqual(logger.Object);
        }
    }
}