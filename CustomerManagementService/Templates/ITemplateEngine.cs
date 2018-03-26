using System.Collections.Generic;

namespace CustomerManagement.Templates
{
    public interface ITemplateEngine
    {
        string Merge(string templateName, IDictionary<string, object> dictionary);
    }
}