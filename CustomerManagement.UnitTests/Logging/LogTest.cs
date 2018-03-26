using CustomerManagement.Infrastructure.Container;
using CustomerManagement.Logging;
using Moq;
using NUnit.Framework;
using NBehave.Spec.NUnit;

namespace CustomerManagement.UnitTests.Logging
{
    [TestFixture]
    public class LogTest
    {
        [Test]
        public void ShouldAskDependencyResolverForFactoryUsedToReturnLogger()
        {
            var resolver = new Mock<IDependencyResolver>();
            var factory = new Mock<ILogFactory>();
            var log = new Mock<ILogger>();

            IoC.Initialize(resolver.Object);

            resolver.Setup(x => x.GetInstanceOf<ILogFactory>()).Returns(factory.Object);
            factory.Setup(x => x.CreateFor(GetType())).Returns(log.Object);

            Log.For(this).ShouldEqual(log.Object);
        }
    }
}