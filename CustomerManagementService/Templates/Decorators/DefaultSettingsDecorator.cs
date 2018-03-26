using System.Collections.Generic;
using CustomerManagement.Infrastructure.Container;
using CustomerManagement.Properties;

namespace CustomerManagement.Templates.Decorators
{
    [ExcludeFromContainer]
    public class DefaultSettingsDecorator : ITemplateEngine
    {
        private readonly ITemplateEngine underlyingEngine;
        private readonly Settings settings;

        public DefaultSettingsDecorator(ITemplateEngine underlyingEngine, Settings settings)
        {
            this.underlyingEngine = underlyingEngine;
            this.settings = settings;
        }

        public string Merge(string templateName, IDictionary<string, object> dictionary)
        {
            dictionary.Add("Settings", settings);
            return underlyingEngine.Merge(templateName, dictionary);
        }
    }
}