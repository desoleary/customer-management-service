using System.Text;
using NBehave.Spec.NUnit;

namespace CustomerManagement.Common.Extensions
{
    public static class StringExtensions
    {
        private static string RemoveFormatting(this string inputString)
        {
            var charsToRemove = new[] { '\r', '\t', '\n', ' ' };
            var results = inputString.Split(charsToRemove);
            var transformedString = new StringBuilder();

            foreach (var s in results)
            {
                transformedString.Append(s);
            }

            return transformedString.ToString();
        }

        public static void WithoutFormattingShouldEqual(this string actualString, string expectedString)
        {
            actualString.RemoveFormatting().ShouldEqual(expectedString.RemoveFormatting());
        }
    }
}