using CustomerManagementService;
using NUnit.Framework;

namespace Tests.Unit.Service
{
    [TestFixture]
    public class CustomerManagementServiceTest
    {
        [Test]
        public void ShouldReturnExpectedXml()
        {
            const string customerId = "12345";
            const string expectedXml = "<customer>" + customerId + "</customer>";

            var service = new CustomerManagementRestService();

            var responseXml = service.Get(customerId);
            
            responseXml.ToString().ShouldEqual(expectedXml);
        }
    }
}


