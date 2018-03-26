using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Castle.Core;
using Castle.Core.Interceptor;
using Castle.Windsor;
using System.Linq;
using CustomerManagement.Exceptions;
using CustomerManagement.Handlers;
using CustomerManagement.Handlers.Chain;
using CustomerManagement.Infrastructure.Container.Interceptors;
using Castle.MicroKernel.Registration;
using CustomerManagement.Model.Students;
using CustomerManagement.View;
using CustomerManagement.Web;
using CustomerManagement.Web.Adapters;
using CustomerManagement.Properties;

namespace CustomerManagement.Infrastructure.Container
{
    [ExcludeFromContainer]
    public sealed class WindsorBootstrapper
    {
        private readonly IWindsorContainer container;

        private WindsorBootstrapper(IWindsorContainer container)
        {
            this.container = container;

            RegisterInterceptors();
            RegisterDependencies();
            RegisterComponents();
        }

        public static void Initialize(IWindsorContainer container)
        {
            new WindsorBootstrapper(container);

            var dependencyResolver = new WindsorDependencyResolver(container);
            IoC.Initialize(dependencyResolver);
        }

        private void RegisterInterceptors()
        {
            container.Register(
                Component.For<IInterceptor>()
                    .Named("SecurityLogging")
                    .ImplementedBy<SecurityLoggingInterceptor>()
                    .LifeStyle.Transient,

                Component.For<IInterceptor>()
                    .Named("TraceLogging")
                    .ImplementedBy<TraceLoggingInterceptor>()
                    .LifeStyle.Transient,

                Component.For<IInterceptor>()
                    .Named("ExceptionSuppressionAndLogging")
                    .ImplementedBy<ExceptionSuppressionAndLoggingInterceptor>()
                    .LifeStyle.Transient,

                Component.For<IInterceptor>()
                    .Named("RestException")
                    .ImplementedBy<RestExceptionInterceptor>()
                    .LifeStyle.Transient);
        }

        private void RegisterDependencies()
        {
            container.Register(
                Component.For<IWebRequest>()
                    .ImplementedBy<WebRequest>()
                    .DependsOn(new {timeout = Settings.Default.WebRequestTimeoutInMilliseconds})
                    .LifeStyle.Transient,

                Component.For<IViewEngine>()
                    .ImplementedBy<ViewEngine>()
                    .DependsOn(new
                    {
                        templates = new Dictionary<string, string> { { "GET-StudentList", "CustomerManagement.Resources.SearchStudentsByName.vm" }, 
                                                                     { "GET-Student", "CustomerManagement.Resources.GetStudent.vm" },
                                                                     { "POST-Student", "CustomerManagement.Resources.CreateStudent.vm" } }
                    })
                    .LifeStyle.Transient,
                
                Component.For<ISearchRequestHandler>()
                    .Named("SearchRequestHandlerByName")
                    .ImplementedBy<SearchRequestHandlerByName>()
                    .LifeStyle.Transient);
        }


        private void RegisterComponents()
        {
            container.Register(Component.For<CustomerManagementRestService>()
                                   .Named("service")
                                   .ServiceOverrides(ServiceOverride.ForKey("searchRequestHandler").Eq("SearchRequestHandlerByName"))                                   
                                   .Interceptors(InterceptorReference.ForKey("RestException"),
                                                 InterceptorReference.ForKey("ExceptionSuppressionAndLogging"),
                                                 InterceptorReference.ForKey("TraceLogging"),
                                                 InterceptorReference.ForKey("SecurityLogging"))
                                   .First
                                   .LifeStyle.Transient);

            container.Register(AllTypes.Pick().FromAssembly(Assembly.GetExecutingAssembly())
                                   .Unless(x => x.HasBeenExcluded())
                                   .WithService
                                   .FirstInterface()
                                   .Configure(x => x.LifeStyle.Transient));
        }
    }

    public static class WindsorBootstrapperExtensions
    {
        public static bool HasBeenExcluded(this Type type)
        {
            return type.GetCustomAttributes(true).Contains(new ExcludeFromContainerAttribute());
        }
    }
}