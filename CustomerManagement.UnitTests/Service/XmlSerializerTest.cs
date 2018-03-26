using CustomerManagement.Model.Students;
using NUnit.Framework;
using NBehave.Spec.NUnit;

namespace CustomerManagement.UnitTests.Service
{
    [TestFixture]
    public class XmlSerializerTest
    {
        [Test]
        public void ShouldDoSomething()
        {
            var serializer = new XmlSerializerAdapter();
            var student = serializer.Deserialize<Student>("<Student/>");
            student.ShouldNotBeNull();
        }
    }
}
