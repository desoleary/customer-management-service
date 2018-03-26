using System;
using Castle.Core.Interceptor;
using CustomerManagement.Logging;
using Moq;
using NBehave.Spec.NUnit;
using NUnit.Framework;
using Assert=CustomerManagement.Common.TestUtilities.Assert;

namespace CustomerManagement.UnitTests.Logging
{
    [TestFixture]
    public class InvocationFormatProviderTest
    {
        [Test]
        public void ShouldReturnTheDefaultStringWhenNoFormatIsSpecified()
        {
            var invocation = new Mock<IInvocation>();
            invocation.Setup(x => x.TargetType.UnderlyingSystemType.Name).Returns("ClassName");
            invocation.Setup(x => x.Method.Name).Returns("MethodName");

            var s = string.Format(new InvocationFormatProvider(), "{0}", new InvocationAdapter(invocation.Object));
            Extensions.ShouldEqual(s, "ClassName.MethodName");
        }

        [Test]
        public void ShouldIncludeTheReturnValueWhenAFormatIsSpecified()
        {
            var invocation = new Mock<IInvocation>();
            invocation.Setup(x => x.TargetType.UnderlyingSystemType.Name).Returns("ClassName");
            invocation.Setup(x => x.Method.Name).Returns("MethodName");
            invocation.Setup(x => x.ReturnValue).Returns("ReturnValue");

            var s = string.Format(new InvocationFormatProvider(), "{0:R}", new InvocationAdapter(invocation.Object));
            Extensions.ShouldEqual(s, string.Format("ClassName.MethodName{0}ReturnValue", Environment.NewLine));
        }

        [Test]
        public void ShouldReturnEmptyStringIfObjectToBeFormattedIsNull()
        {
            var invocation = new Mock<IInvocation>();
            invocation.Setup(x => x.TargetType.UnderlyingSystemType.Name).Returns("ClassName");
            invocation.Setup(x => x.Method.Name).Returns("MethodName");
            invocation.Setup(x => x.ReturnValue).Returns("ReturnValue");

            Assert.ThrowsExactly<ArgumentException>(() => string.Format(new InvocationFormatProvider(), "{0:R}", "unexpected object"));
        }
    }
}