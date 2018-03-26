using System;
using System.Linq;
using Castle.Core;
using Castle.Windsor;
using CustomerManagement.Infrastructure.Container;
using CustomerManagement.Infrastructure.Container.Interceptors;
using CustomerManagement.Web;
using CustomerManagement.Web.Adapters;
using NBehave.Spec.NUnit;
using NUnit.Framework;

namespace CustomerManagement.UnitTests.Infrastructure.Container
{
    [TestFixture]
    public class WindsorBootstrapperTest
    {
        private WindsorContainer container;

        [SetUp]
        public void SetUp()
        {
            container = new WindsorContainer();
            WindsorBootstrapper.Initialize(container);
        }

        [Test]
        public void ShouldRegisterInterceptorForTraceLogger()
        {
            Assert.That(GetInterceptorsFor(typeof(CustomerManagementRestService)).Contains(new InterceptorReference("TraceLogging")));
            Assert.That(container.Resolve("TraceLogging"), Is.TypeOf(typeof(TraceLoggingInterceptor)));
        }

        [Test]
        public void ShouldRegisterInterceptorForSecurityLogger()
        {
            Assert.That(GetInterceptorsFor(typeof(CustomerManagementRestService)).Contains(new InterceptorReference("SecurityLogging")));
            Assert.That(container.Resolve("SecurityLogging"), Is.TypeOf(typeof(SecurityLoggingInterceptor)));
        }

        [Test]
        public void ShouldRegisterInterceptorForExceptionSuppressionAndLogging()
        {
            Assert.That(GetInterceptorsFor(typeof(CustomerManagementRestService)).Contains(new InterceptorReference("ExceptionSuppressionAndLogging")));
            Assert.That(container.Resolve("ExceptionSuppressionAndLogging"), Is.TypeOf(typeof(ExceptionSuppressionAndLoggingInterceptor)));
        }
        
        [Test]
        public void ShouldRegisterInterceptorForRestException()
        {
            Assert.That(GetInterceptorsFor(typeof(CustomerManagementRestService)).Contains(new InterceptorReference("RestException")));
            Assert.That(container.Resolve("RestException"), Is.TypeOf(typeof(RestExceptionInterceptor)));
        }

        [Test]
        public void ShouldReturnDefaultWebRequestWhenResolvingTypeIWebRequest()
        {
            container.Resolve<IWebRequest>().ShouldBeInstanceOfType<WebRequest>();
        }


        [Test]
        public void ShouldResolveAllRegisteredComponents()
        {
            var handlers = container.Kernel.GetAssignableHandlers(typeof(object));

            handlers
                .Where(x => !(x.ComponentModel.Implementation.IsAbstract || x.ComponentModel.Implementation.IsGenericTypeDefinition))
                .ForEach(x => container.Resolve(x.Service).ShouldNotBeNull());
        }

        private InterceptorReferenceCollection GetInterceptorsFor(Type service)
        {
            var handler = container.Kernel.GetHandler(service);
            return handler.ComponentModel.Interceptors;
        }
    }
}