using System.Text;
using NBehave.Spec.NUnit;

namespace Tests.Unit
{
    public static class StringExtensions
    {
        public static string RemoveFormatting(this string inputString)
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

        public static void ShouldEqual(this string actualString, string expectedString)
        {
            Extensions.ShouldEqual(actualString.RemoveFormatting(), expectedString.RemoveFormatting());
        }
    }
}
