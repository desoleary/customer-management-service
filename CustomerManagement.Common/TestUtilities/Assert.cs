using System;
using System.Net;
using System.Xml.Linq;
using CustomerManagement.Exceptions;
using NBehave.Spec.NUnit;

namespace CustomerManagement.Common.TestUtilities
{
    public static class Assert
    {
        public static TException ThrowsExactly<TException>(Action methodWhichThrows) where TException : Exception
        {
            TException caughtException = null;
            var expectedExceptionType = typeof(TException);
            try
            {
                methodWhichThrows();
                NUnit.Framework.Assert.Fail(string.Format("{0} was not thrown", expectedExceptionType.Name));
            }
            catch (TException e)
            {
                var actualCaughtExceptionType = e.GetType();

                if (actualCaughtExceptionType != expectedExceptionType)
                {
                    NUnit.Framework.Assert.Fail(string.Format("Exception thrown must be exactly of type {0} but was of type {1}", expectedExceptionType.FullName, actualCaughtExceptionType.FullName));
                }
                caughtException = e;
            }
            catch (Exception e)
            {
                var actualCaughtExceptionType = e.GetType();

                if (actualCaughtExceptionType != expectedExceptionType)
                {
                    NUnit.Framework.Assert.Fail(string.Format("Exception thrown must be exactly of type {0} but was of type {1}", expectedExceptionType.FullName, actualCaughtExceptionType.FullName));
                }
                caughtException = e as TException;
            }
            return caughtException;
        }

        public static void ThrowsNotFoundRestException(Action methodWhichThrows)
        {
            var expectedExceptionType = typeof (RestException);
            try
            {
                methodWhichThrows();
                NUnit.Framework.Assert.Fail(string.Format("{0} was not thrown", expectedExceptionType.Name));

            }
            catch (Exception e)
            {
                var restException = e as RestException;

                if (restException == null)
                {
                    NUnit.Framework.Assert.Fail(string.Format("Exception thrown must be exactly of type {0} but was of type {1}",
                                      expectedExceptionType.FullName, e.GetType().FullName));
                }

                restException.StatusCode.ShouldEqual(HttpStatusCode.NotFound);
            }
        }

        public static void ThrowsRestExceptionWithHttpStatusCodeOf(HttpStatusCode httpStatusCode, Action methodWhichThrows)
        {
            var expectedExceptionType = typeof(RestException);

            try
            {
                methodWhichThrows();
                NUnit.Framework.Assert.Fail(string.Format("{0} was not thrown", expectedExceptionType.Name));
            }
            catch (Exception e)
            {
                var restException = e as RestException;

                if (restException == null)
                {
                    NUnit.Framework.Assert.Fail(string.Format("Exception thrown must be exactly of type {0} but was of type {1}",
                                      expectedExceptionType.FullName, e.GetType().FullName));
                }

                restException.StatusCode.ShouldEqual(httpStatusCode);
            }
        }
    }
}