using System;
using System.Net;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CustomerManagement.Infrastructure.Container;
using CustomerManagement.Logging;
using CustomerManagement.Web;
using NUnit.Framework;
using NBehave.Spec.NUnit;
using WebRequest=CustomerManagement.Web.Adapters.WebRequest;

namespace CustomerManagement.IntegrationTests.Service
{
    [TestFixture]
    public class CustomerManagementRestServiceTest
    {
        private readonly string baseUri = Properties.Settings.Default.CustomerManagementBaseUri + "/students";
        private const string SearchUriTemplate = "{0}?firstName={1}&lastName={2}";
        private const string GetUriTemplate = "{0}/{1}";

        [SetUp]
        public void SetUp()
        {
            RegisterNullLogFactoryWithWindsorContainer();
        }

        [Test]
        public void Search_ShouldReturnStudentsXml()
        {
            var response = GetWebResponseFrom(string.Format(SearchUriTemplate, baseUri, "Joe", "Blog"));
            response.StatusCode.ShouldEqual(HttpStatusCode.OK);
            response.Body.ShouldNotBeNull();
        }

        [Test]
        public void Get_ShouldReturnStudentsXml()
        {
            var response = GetWebResponseFrom(string.Format(GetUriTemplate, baseUri, "1"));
            response.StatusCode.ShouldEqual(HttpStatusCode.OK);
            response.Body.ShouldNotBeNull();
        }

        [Test]
        public void Create_ShouldReturnStudentsXml()
        {
            const string studentXml = @"<Student>
          <FirstName>firstName</FirstName>
          <LastName>lastName</LastName>
          <DateOfBirth>2010-02-06T12:43:25.2267795-08:00</DateOfBirth>
          <PhoneNumber>phoneNumber</PhoneNumber>
          <AddressLine1>addressLine1</AddressLine1>
          <AddressLine2>addressLine2</AddressLine2>
          <Grade>grade</Grade>
        </Student>";

            var response = PostWebResponseFrom(baseUri, studentXml);
            response.StatusCode.ShouldEqual(HttpStatusCode.OK);
            response.Body.ShouldNotBeNull();
        }

        private static IWebResponse GetWebResponseFrom(string uri)
        {
            var webRequest = new WebRequest(Properties.Settings.Default.WebRequestTimeoutInMilliseconds);
            return webRequest.GetResponseFor(HttpMethod.GET, uri);
        }

        private static IWebResponse PostWebResponseFrom(string uri, string xmlBody)
        {
            var webRequest = new WebRequest(Properties.Settings.Default.WebRequestTimeoutInMilliseconds);
            return webRequest.GetResponseFor(HttpMethod.POST, uri, xmlBody);
        }

        private static void RegisterNullLogFactoryWithWindsorContainer()
        {
            var container = new WindsorContainer();
            container.Register(Component.For<ILogFactory>()
                                   .ImplementedBy<NullLogFactory>()
                                   .LifeStyle.Transient);
            var dependencyResolver = new WindsorDependencyResolver(container);
            
            IoC.Initialize(dependencyResolver);
        }
    }
}