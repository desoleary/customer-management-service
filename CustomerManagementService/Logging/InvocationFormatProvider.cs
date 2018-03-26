using System;

namespace CustomerManagement.Logging
{
    public class InvocationFormatProvider : IFormatProvider, ICustomFormatter
    {
        public object GetFormat(Type formatType)
        {
            return formatType == typeof(ICustomFormatter) ? this : null;
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            var context = arg as IInvocationContext;
            if (context == null) throw new ArgumentException(string.Format("Argument to be formatted must be of type {0}", typeof(IInvocationContext).Name), "arg");
            
            return string.Format("{0}{1}", context, format == "R" ? Environment.NewLine + context.ReturnValue : string.Empty);
        }
    }
}