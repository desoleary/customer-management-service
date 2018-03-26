using System;
using System.Collections;
using System.Collections.Generic;
using CustomerManagement.Exceptions;
using CustomerManagement.Model.Students;
using CustomerManagement.Templates;
using CustomerManagement.View;
using CustomerManagement.Web;
using Moq;
using NUnit.Framework;
using Assert=CustomerManagement.Common.TestUtilities.Assert;

namespace CustomerManagement.UnitTests.View
{
    [TestFixture]
    public class ViewEngineTest
    {
        private Dictionary<string, string> registry;
        const string TemplateLocation = "some template path location";
        private ViewEngine engine;
        private Mock<ITemplateEngine> templateEngine;
        private Mock<IWebOperationContext> webOperationContext;

        [SetUp]
        public void SetUp()
        {
            registry = new Dictionary<string, string> { { "GET-" + typeof(object).Name, TemplateLocation } };
            templateEngine = new Mock<ITemplateEngine>();
            webOperationContext = new Mock<IWebOperationContext>();

            var incomingRequest = new Mock<IIncomingRequest>();
            incomingRequest.Setup(x => x.HttpMethod).Returns("GET");
            webOperationContext.Setup(x => x.IncomingRequest).Returns(incomingRequest.Object);

            engine = new ViewEngine(templateEngine.Object, registry, webOperationContext.Object);
        }

        [Test]
        public void ShouldCallUnderlyingTemplateEngineWhenInstanceTypePassedMatchesRegistry()
        {
            var objectToRender = new object();
            
            engine.Render(objectToRender);

            templateEngine.Verify(x => x.Merge(It.IsAny<string>(), It.IsAny<IDictionary<string, object>>()));
        }

        [Test]
        public void ShouldCallUnderlyingTemplateEngineWhenInstanceTypePassedMatchesRegistry_WithMatchedTemplateAndDictionaryContainingInstanceTypeNameAndInstance()
        {
            var objectToRender = new object();
            var expectedDictionaryToBeRendered = new Dictionary<string, object> { { objectToRender.GetType().Name, objectToRender } };

            engine.Render(objectToRender);

            templateEngine.Verify(x => x.Merge(TemplateLocation, expectedDictionaryToBeRendered));
        }

        [Test]
        public void ShouldThrowSomethingWhenInstanceTypePassedDoesNotMatchRegistry()
        {
            var unexpectedInstance = new Student(0, null, null, DateTime.Now,null,null,null, null);

            Assert.ThrowsExactly<TemplateResolutionException>(() => engine.Render(unexpectedInstance));
        }

        [Test]
        public void ShouldHandleGenericInstanceTypesBeingPassed_CallsUnderlyingTemplateEngineWithInnerTypeName()
        {
            IEnumerable<Student> students = new List<Student>
                                                {
                                                    new Student(0, "firstName", "lastName", DateTime.Now, "phoneNumber",
                                                                "addressLine1", "addressLine2", "grade")
                                                };

            registry = new Dictionary<string, string> { { "GET-StudentList", TemplateLocation } };
            templateEngine = new Mock<ITemplateEngine>();

            engine = new ViewEngine(templateEngine.Object, registry, webOperationContext.Object);
            
            engine.Render(students);

            templateEngine.Verify(x => x.Merge(TemplateLocation, new Dictionary<string, object> { { "StudentList", students } }));
        }
    }
}
